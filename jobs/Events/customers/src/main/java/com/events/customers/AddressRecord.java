package com.events.customers;

import com.fasterxml.jackson.annotation.JsonProperty;

public class AddressRecord {
    @JsonProperty("City")
    public String city;
    @JsonProperty("State")
    public String state;
    @JsonProperty("Street")
    public String street;
    @JsonProperty("Country")
    public String country;
    @JsonProperty("PostalCode")
    public String postalCode;
}