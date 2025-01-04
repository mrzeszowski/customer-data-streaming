package com.events.customers.contracts;

public class ConsentDto {

    public String type;
    public boolean isGranted;
    public String timestamp;

    public ConsentDto(String type, boolean isGranted, String timestamp) {

        this.type = type;
        this.isGranted = isGranted;
        this.timestamp = timestamp;
    }
}
