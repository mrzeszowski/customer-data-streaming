package com.events.products;

import com.events.products.contracts.ProductDto;
import com.events.products.contracts.ProductUpserted;
import com.events.shared.Event;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.apache.flink.api.common.functions.FlatMapFunction;
import org.apache.flink.util.Collector;
import org.slf4j.Logger;

public class ProductFlatMapFunction implements FlatMapFunction<String, Event> {

    private final ObjectMapper objectMapper;
    private final Logger LOG;

    public ProductFlatMapFunction(Logger logger) {
        this.objectMapper = new ObjectMapper();
        objectMapper.configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false);

        LOG = logger;
    }

    @Override
    public void flatMap(String value, Collector<Event> out) throws Exception {

        JsonNode rootNode = objectMapper.readTree(value);
        String operation = rootNode.get("op").asText();
        JsonNode afterNode = rootNode.get("after");

        ProductRecord product = objectMapper.treeToValue(afterNode, ProductRecord.class);

        LOG.info("Operation: {}", operation);

        ProductUpserted upserted = new ProductUpserted(
                product.id,
                new ProductDto(product.name, product.description, product.price, product.category),
                product.version
        );

        out.collect(upserted);
    }
}
