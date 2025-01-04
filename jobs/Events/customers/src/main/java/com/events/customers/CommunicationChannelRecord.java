package com.events.customers;

import com.fasterxml.jackson.annotation.JsonProperty;

public class CommunicationChannelRecord {

    @JsonProperty("Type")
    public String type;
    @JsonProperty("Value")
    public String value;
    @JsonProperty("IsPrimary")
    public boolean isPrimary;
}
