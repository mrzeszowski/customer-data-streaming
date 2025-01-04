package com.events.customers.contracts;

public class CommunicationChannelDto {

    public String type;
    public String value;
    public Boolean isPrimary;

    public CommunicationChannelDto(String type, String value, Boolean isPrimary) {

        this.type = type;
        this.value = value;
        this.isPrimary = isPrimary;
    }
}
