package com.events.shipments.contracts;

public class ShipmentStageDto {

    public String id;
    public String type;
    public String timestamp;
    public String note;

    public ShipmentStageDto(String id, String type, String timestamp, String note) {

        this.id = id;
        this.type = type;
        this.timestamp = timestamp;
        this.note = note;
    }
}
