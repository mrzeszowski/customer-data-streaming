package com.ai.anomaly.shared;

import com.fasterxml.jackson.annotation.JsonIgnore;

// Abstract base class for customer events
public abstract class Event {
    private final String id;

    protected Event(String id) {
        this.id = id;
    }

    public String getId() {
        return id;
    }

    @JsonIgnore
    public abstract String getEventType();
}