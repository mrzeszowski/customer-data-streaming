package com.events.customers.contracts;

import com.events.shared.Event;

public class CustomerUpdated extends Event{

    public CustomerDto data;
    public Long version;

    public CustomerUpdated(String id,
                           CustomerDto data,
                           Long version) {
        super(id);
        this.data = data;
        this.version = version;
    }


    public String getEventType() {
        return "com.events.customers.contracts.CustomerUpdated";
    }
}
