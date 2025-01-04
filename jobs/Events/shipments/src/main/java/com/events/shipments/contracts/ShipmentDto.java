package com.events.shipments.contracts;

import java.util.List;

public class ShipmentDto {

    public String id;
    public String transactionId;
    public List<ShipmentStageDto> stages;
    public Boolean isCompleted;
    public String createdAt;
    public String updatedAt;
    public Long version;

    public ShipmentDto(String id,
                       String transactionId,
                       List<ShipmentStageDto> stages,
                       Boolean isCompleted,
                       String createdAt,
                       String updatedAt,
                       Long version) {

        this.id = id;
        this.transactionId = transactionId;
        this.stages = stages;
        this.isCompleted = isCompleted;
        this.createdAt = createdAt;
        this.updatedAt = updatedAt;
        this.version = version;
    }
}
