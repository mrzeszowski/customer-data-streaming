package com.events.transactions;

import com.fasterxml.jackson.annotation.JsonProperty;

import java.math.BigDecimal;

public class PaymentDetailsRecord {
    @JsonProperty("Type")
    public String type;
    @JsonProperty("Amount")
    public BigDecimal amount;
    @JsonProperty("IsSuccessful")
    public Boolean isSuccessful;
}

