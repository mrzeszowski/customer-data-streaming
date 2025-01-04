package com.events.transactions;

import com.fasterxml.jackson.annotation.JsonProperty;

public class TransactionRecord {

    @JsonProperty("id")
    public String id;

    @JsonProperty("customer_id")
    public String customerId;

    @JsonProperty("created_at")
    public String createdAt;

    @JsonProperty("updated_at")
    public String updatedAt;

    @JsonProperty("version")
    public Long version;
}