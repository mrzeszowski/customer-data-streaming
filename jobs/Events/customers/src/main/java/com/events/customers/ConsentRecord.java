package com.events.customers;

import com.fasterxml.jackson.annotation.JsonProperty;

public class ConsentRecord {
    @JsonProperty("Type")
    public String type;
    @JsonProperty("IsGranted")
    public boolean isGranted;
    @JsonProperty("Timestamp")
    public String timestamp;
}
