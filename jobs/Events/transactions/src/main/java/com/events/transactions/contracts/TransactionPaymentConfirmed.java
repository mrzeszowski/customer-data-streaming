package com.events.transactions.contracts;

import com.events.shared.Event;

public class TransactionPaymentConfirmed extends Event {

    public Long version;

    public TransactionPaymentConfirmed(String id, Long version) {
        super(id);
        this.version = version;
    }

    @Override
    public String getEventType() {
        return "com.events.transactions.contracts.TransactionPaymentConfirmed";
    }
}
