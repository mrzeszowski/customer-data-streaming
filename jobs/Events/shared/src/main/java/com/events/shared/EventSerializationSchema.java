package com.events.shared;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.apache.flink.connector.kafka.sink.KafkaRecordSerializationSchema;
import org.apache.kafka.clients.producer.ProducerRecord;

import java.nio.charset.StandardCharsets;
import java.util.UUID;

public class EventSerializationSchema implements KafkaRecordSerializationSchema<Event> {
    private final String topic;

    public EventSerializationSchema(String topic) {
        this.topic = topic;
    }

    @Override
    public ProducerRecord<byte[], byte[]> serialize(Event element, KafkaRecordSerializationSchema.KafkaSinkContext context, Long timestamp) {
        String eventType = element.getEventType();
        ObjectMapper objectMapper = new ObjectMapper();

        String serializedValue = null;
        try {
            serializedValue = objectMapper.writeValueAsString(element);
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }

        ProducerRecord<byte[], byte[]> record = new ProducerRecord<>(
                topic,
                element.getId().getBytes(StandardCharsets.UTF_8),
                serializedValue.getBytes(StandardCharsets.UTF_8)
        );

        // Add event type as a Kafka header
        record.headers().add("message.type", eventType.getBytes(StandardCharsets.UTF_8));
        record.headers().add("message.id", UUID.randomUUID().toString().getBytes(StandardCharsets.UTF_8));

        return record;
    }
}