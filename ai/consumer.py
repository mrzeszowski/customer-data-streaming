from json import loads, dumps
from time import sleep
from kafka import KafkaConsumer
from river import anomaly, stats, metrics
from kafka import KafkaProducer

import logging
import os
import uuid

# Access an environment variable
kafka_server = os.environ.get('KAFKA')
consumer_group_id = os.environ.get('CONSUMER_GROUP_ID')
print(f"Kafka: {kafka_server}")
print(f"Consumer Group ID: {consumer_group_id}")

# Configure logging
logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

# Metric for evaluation
metric = metrics.ROCAUC()

# Initialize the anomaly detection model
model = anomaly.StandardAbsoluteDeviation(sub_stat=stats.Mean())

# create a kafka product that connects to Kafka on port 9092
producer = KafkaProducer(
    bootstrap_servers=[kafka_server],
    value_serializer=lambda x: dumps(x).encode("utf-8"),
    key_serializer=str.encode
)

# Kafka consumer setup
consumer = KafkaConsumer(
    "transactions.events",
    bootstrap_servers=[kafka_server],
    auto_offset_reset="earliest",
    enable_auto_commit=True,
    group_id=consumer_group_id,
    value_deserializer=lambda x: loads(x.decode("utf-8")),
)

# Process Kafka events
for event in consumer:
    event_data = event.value
    try:
        # Decode Kafka headers
        decoded_data = {}
        if event.headers:
            decoded_data = {key: value.decode('utf-8') for key, value in event.headers}

        message_type = decoded_data.get('message.type', '')
        if message_type == 'com.events.transactions.contracts.TransactionCreated':
            amount = event_data['data']['payment']['amount']
            
            # Update the model and score the event
            model.learn_one(None, amount)
            score = model.score_one(None, amount)
            
            # Detect anomalies
            is_anomaly = score > 1.0
            if is_anomaly:
                logger.info("transaction payment detected as anomaly: %s", amount)
                transaction_id = event_data['id']
                message_id = str(uuid.uuid4())
                data = {"transactionId": transaction_id, "amount": amount, "score": score}

                headers = [
                    ('message.type', b'ai.TransactionAnomalyDetected'),
                    ('message.id', message_id.encode('utf-8'))  # Convert string to bytes
                ]

                producer.send(topic="anomalies.events", 
                              value=data, 
                              key=message_id, 
                              headers=headers)

    except Exception as ex:
        raise