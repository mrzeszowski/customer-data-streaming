package com.events.transactions;

import com.fasterxml.jackson.annotation.JsonProperty;

import java.math.BigDecimal;

public class TransactionProductRecord {
    @JsonProperty("ProductId")
    public String productId;
    @JsonProperty("Name")
    public String name;
    @JsonProperty("Price")
    public BigDecimal price;
    @JsonProperty("Quantity")
    public Integer quantity;
}