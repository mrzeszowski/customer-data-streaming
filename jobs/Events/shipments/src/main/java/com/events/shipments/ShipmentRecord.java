package com.events.shipments;

import com.fasterxml.jackson.annotation.JsonProperty;

public class ShipmentRecord {

    @JsonProperty("id")
    public String id;

    @JsonProperty("transaction_id")
    public String transactionId;

    @JsonProperty("is_completed")
    public Boolean isCompleted;

    @JsonProperty("created_at")
    public String createdAt;

    @JsonProperty("updated_at")
    public String updatedAt;

    @JsonProperty("version")
    public Long version;
}