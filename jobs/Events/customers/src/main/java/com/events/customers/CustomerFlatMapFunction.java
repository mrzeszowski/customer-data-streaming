package com.events.customers;

import com.events.customers.contracts.*;
import com.events.shared.Event;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.apache.flink.api.common.functions.FlatMapFunction;
import org.apache.flink.util.Collector;
import org.slf4j.Logger;

import java.util.List;
import java.util.stream.Collectors;

// Event transformer for parsing raw CDC events
public  class CustomerFlatMapFunction implements FlatMapFunction<String, Event> {

    private final ObjectMapper objectMapper;
    private final Logger LOG;

    public CustomerFlatMapFunction(Logger logger) {
        this.objectMapper = new ObjectMapper();
        objectMapper.configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false);

        LOG = logger;
    }

    @Override
    public void flatMap(String value, Collector<Event> out) throws Exception {

        JsonNode rootNode = objectMapper.readTree(value);
        String operation = rootNode.get("op").asText();
        JsonNode afterNode = rootNode.get("after");

        CustomerRecord customer = objectMapper.treeToValue(afterNode, CustomerRecord.class);
        AddressRecord address = objectMapper.readValue(afterNode.get("address").asText(), AddressRecord.class);
        List<CommunicationChannelRecord> communicationChannels = objectMapper.readValue(afterNode.get("communication_channels").asText(), new TypeReference<List<CommunicationChannelRecord>>() {});
        List<ConsentRecord> consents = objectMapper.readValue(afterNode.get("consents").asText(), new TypeReference<List<ConsentRecord>>() {});

        LOG.info("Operation: {}", operation);

        if ("c".equals(operation)) {
            CustomerCreated created = new CustomerCreated(
                    customer.id,
                    GetData(customer, address, communicationChannels, consents),
                    customer.version
            );
            out.collect(created);
        } else if ("u".equals(operation)) {
            CustomerUpdated updated = new CustomerUpdated(
                    customer.id,
                    GetData(customer, address, communicationChannels, consents),
                    customer.version
            );
            out.collect(updated);
        } else if ("d".equals(operation)) {
            CustomerDeleted deleted = new CustomerDeleted(customer.id);
            out.collect(deleted);
        }
    }

    private CustomerDto GetData(
            CustomerRecord after,
            AddressRecord address,
            List<CommunicationChannelRecord> communicationChannels,
            List<ConsentRecord> consents){
        return new CustomerDto(after.firstName,
                after.lastName,
                after.email,
                after.dateOfBirth,
                new AddressDto(address.city, address.state, address.street, address.country, address.postalCode),
                communicationChannels.stream()
                        .map(x -> new CommunicationChannelDto(x.type, x.value, x.isPrimary))
                        .collect(Collectors.toList()),
                consents.stream()
                        .map(x -> new ConsentDto(x.type, x.isGranted, x.timestamp))
                        .collect(Collectors.toList()),
                after.createdAt,
                after.updatedAt);
    }
}
