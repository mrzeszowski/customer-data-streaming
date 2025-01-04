package com.events.transactions.contracts;

import java.math.BigDecimal;

public class PaymentDetailsDto {

    public String type;
    public BigDecimal amount;
    public boolean isSuccessful;

    public PaymentDetailsDto(String type, BigDecimal amount, boolean isSuccessful) {
        this.type = type;
        this.amount = amount;
        this.isSuccessful = isSuccessful;
    }
}

