package com.events.shipments;

import com.events.shared.Event;
import com.events.shipments.contracts.*;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.apache.flink.api.common.functions.FlatMapFunction;
import org.apache.flink.util.Collector;
import org.slf4j.Logger;

import java.util.List;
import java.util.Objects;

public class ShipmentFlatMapFunction implements FlatMapFunction<String, Event> {

    private final ObjectMapper objectMapper;
    private final Logger LOG;

    public ShipmentFlatMapFunction(Logger logger) {
        this.objectMapper = new ObjectMapper();
        objectMapper.configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false);

        LOG = logger;
    }

    @Override
    public void flatMap(String value, Collector<Event> out) throws Exception {

        JsonNode rootNode = objectMapper.readTree(value);
        String operation = rootNode.get("op").asText();
        JsonNode afterNode = rootNode.get("after");

        ShipmentRecord shipment = objectMapper.treeToValue(afterNode, ShipmentRecord.class);
        List<ShipmentStageRecord> afterStages = objectMapper.readValue(afterNode.get("stages").asText(), new TypeReference<List<ShipmentStageRecord>>() {});

        LOG.info("Operation: {}", operation);

        if ("c".equals(operation)) {
            ShipmentCreated created = new ShipmentCreated(
                    shipment.id,
                    shipment.transactionId,
                    afterStages.get(afterStages.size() - 1).note,
                    shipment.version
            );
            out.collect(created);
        } else if ("u".equals(operation)) {

            ShipmentStageRecord lastStage = afterStages.get(afterStages.size() - 1);

            if (lastStage.type.equals("Processed"))
                out.collect(new ShipmentProcessed(shipment.id,
                        shipment.transactionId,
                        lastStage.note,
                        shipment.version));

            if (lastStage.type.equals("Shipped"))
                out.collect(new ShipmentShipped(shipment.id,
                        shipment.transactionId,
                        lastStage.note,
                        shipment.version));

            if (lastStage.type.equals("TransitStarted"))
                out.collect(new ShipmentTransitStarted(shipment.id,
                        shipment.transactionId,
                        lastStage.note,
                        shipment.version));

            if (lastStage.type.equals("Delivered"))
                out.collect(new ShipmentDelivered(shipment.id,
                        shipment.transactionId,
                        lastStage.note,
                        shipment.version));

        }
    }
}
