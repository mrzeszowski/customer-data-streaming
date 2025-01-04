package com.events.shipments.contracts;

import com.events.shared.Event;

public class ShipmentCreated extends Event {

    public String transactionId;
    public String note;
    public Long version;

    public ShipmentCreated(String id, String transactionId, String note, Long version) {
        super(id);
        this.transactionId = transactionId;
        this.note = note;
        this.version = version;
    }

    @Override
    public String getEventType() {
        return "com.events.shipments.contracts.ShipmentCreated";
    }
}
