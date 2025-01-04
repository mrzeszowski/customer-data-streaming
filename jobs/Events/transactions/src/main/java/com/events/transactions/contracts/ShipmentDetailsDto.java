package com.events.transactions.contracts;

public class ShipmentDetailsDto {

    public String address;
    public String provider;

    public ShipmentDetailsDto(String address, String provider) {
        this.address = address;
        this.provider = provider;
    }
}