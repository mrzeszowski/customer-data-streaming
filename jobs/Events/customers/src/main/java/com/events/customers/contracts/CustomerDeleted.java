package com.events.customers.contracts;

import com.events.shared.Event;

public class CustomerDeleted extends Event {

    public CustomerDeleted(String id) {
        super(id);
    }

    @Override
    public String getEventType() {
        return "com.events.customers.contracts.CustomerDeleted";
    }
}
