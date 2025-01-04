package com.events.transactions.contracts;

import java.util.List;

public class TransactionDto {


    public String customerId;
    public List<TransactionProductDto> products;
    public PaymentDetailsDto payment;
    public ShipmentDetailsDto shipment;
    public String createdAt;
    public String updatedAt;

    public TransactionDto(String customerId,
                          List<TransactionProductDto> products,
                          PaymentDetailsDto payment,
                          ShipmentDetailsDto shipment,
                          String createdAt,
                          String updatedAt) {
        this.customerId = customerId;
        this.products = products;
        this.payment = payment;
        this.shipment = shipment;
        this.createdAt = createdAt;
        this.updatedAt = updatedAt;
    }
}
