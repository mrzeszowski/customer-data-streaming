package com.events.transactions;

import com.events.shared.Event;
import com.events.transactions.contracts.*;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.apache.flink.api.common.functions.FlatMapFunction;
import org.apache.flink.util.Collector;
import org.slf4j.Logger;

import java.util.List;
import java.util.stream.Collectors;

public  class TransactionFlatMapFunction implements FlatMapFunction<String, Event> {

    private final ObjectMapper objectMapper;
    private final Logger LOG;

    public TransactionFlatMapFunction(Logger logger) {
        this.objectMapper = new ObjectMapper();
        objectMapper.configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false);

        LOG = logger;
    }

    @Override
    public void flatMap(String value, Collector<Event> out) throws Exception {

        JsonNode rootNode = objectMapper.readTree(value);
        String operation = rootNode.get("op").asText();
        JsonNode afterNode = rootNode.get("after");

        TransactionRecord transaction = objectMapper.treeToValue(afterNode, TransactionRecord.class);

        PaymentDetailsRecord payment = objectMapper.readValue(afterNode.get("payment").asText(), PaymentDetailsRecord.class);
        ShipmentDetailsRecord shipment = objectMapper.readValue(afterNode.get("shipment").asText(), ShipmentDetailsRecord.class);
        List<TransactionProductRecord> products = objectMapper.readValue(afterNode.get("products").asText(), new TypeReference<List<TransactionProductRecord>>() {});

        LOG.info("Operation: {}", operation);

        if ("c".equals(operation)) {

            TransactionCreated created = new TransactionCreated(
                    transaction.id,
                    GetData(transaction, shipment, payment, products),
                    transaction.version
            );
            out.collect(created);
        } else if ("u".equals(operation)) {

            if (payment.isSuccessful) {
                out.collect(new TransactionPaymentConfirmed(transaction.id, transaction.version));
            }
        }
    }

    private TransactionDto GetData(
            TransactionRecord record,
            ShipmentDetailsRecord shipment,
            PaymentDetailsRecord payment,
            List<TransactionProductRecord> products){
        return new TransactionDto(record.customerId,
                products.stream().map(x -> new TransactionProductDto(x.productId, x.name, x.price, x.quantity)).collect(Collectors.toList()),
                new PaymentDetailsDto(payment.type, payment.amount, payment.isSuccessful),
                new ShipmentDetailsDto(shipment.address, shipment.provider),
                record.createdAt,
                record.updatedAt);
    }
}
