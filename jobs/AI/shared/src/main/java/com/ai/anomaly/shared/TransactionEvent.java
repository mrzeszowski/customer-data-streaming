package com.ai.anomaly.shared;

public class TransactionEvent {

    public Amount amount;

    public TransactionEvent(Amount amount) {

        this.amount = amount;
    }

    public static TransactionEvent empty() {
        return new TransactionEvent(null);
    }
}

