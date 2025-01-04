package com.events.transactions.contracts;

import java.math.BigDecimal;

public class TransactionProductDto {

    public String productId;
    public String name;
    public BigDecimal price;
    public Integer quantity;

    public TransactionProductDto(String productId, String name, BigDecimal price, Integer quantity) {
        this.productId = productId;
        this.name = name;
        this.price = price;
        this.quantity = quantity;
    }
}