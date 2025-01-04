package com.events.transactions.contracts;

import com.events.shared.Event;

public class TransactionCreated extends Event {

    public TransactionDto data;
    public Long version;

    public TransactionCreated(String id, TransactionDto data, Long version) {
        super(id);
        this.data = data;
        this.version = version;
    }

    @Override
    public String getEventType() {
        return "com.events.transactions.contracts.TransactionCreated";
    }
}
