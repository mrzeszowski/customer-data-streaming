package com.events.transactions;

import com.fasterxml.jackson.annotation.JsonProperty;

public class ShipmentDetailsRecord {
    @JsonProperty("Address")
    public String address;
    @JsonProperty("Provider")
    public String provider;
}