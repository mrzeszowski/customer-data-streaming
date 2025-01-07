package com.ai.anomaly.shared;

import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.apache.flink.api.common.typeinfo.TypeInformation;
import org.apache.flink.connector.kafka.source.reader.deserializer.KafkaRecordDeserializationSchema;
import org.apache.flink.util.Collector;
import org.apache.kafka.clients.consumer.ConsumerRecord;
import org.apache.kafka.common.header.Header;

import java.io.IOException;
import java.math.BigDecimal;
import java.nio.charset.StandardCharsets;

public class TransactionDeserializationSchema implements KafkaRecordDeserializationSchema<TransactionEvent> {

    @Override
    public void deserialize(ConsumerRecord<byte[], byte[]> consumerRecord, Collector<TransactionEvent> collector) throws IOException {

        String type = "";
        for (Header header : consumerRecord.headers()) {

            if (header.key().equals("message.type"))
                type = new String(header.value(), StandardCharsets.UTF_8);
        }

        if (type.equals("com.events.transactions.contracts.TransactionCreated")) {
            String payload = new String(consumerRecord.value(), StandardCharsets.UTF_8);

            ObjectMapper objectMapper = new ObjectMapper();
            JsonNode node = objectMapper.readTree(payload).get("data").get("payment").get("amount");
            String amountString = node.asText();

            Double amount = objectMapper.readValue(amountString, Double.class);
            collector.collect(new TransactionEvent(new Amount(amount)));
        }
        else collector.collect(TransactionEvent.empty());
    }

    @Override
    public TypeInformation<TransactionEvent> getProducedType() {
        return TypeInformation.of(TransactionEvent.class);
    }
}