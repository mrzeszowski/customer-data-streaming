package com.events.shipments;

import com.fasterxml.jackson.annotation.JsonProperty;

public class ShipmentStageRecord {

    @JsonProperty("Id")
    public String id;

    @JsonProperty("Type")
    public String type;

    @JsonProperty("Timestamp")
    public String timestamp;

    @JsonProperty("Note")
    public String note;
}

