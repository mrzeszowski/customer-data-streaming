package com.events.products.contracts;

import com.events.shared.Event;

public class ProductUpserted extends Event {
    
    public ProductDto data;
    public Long version;

    public ProductUpserted(String id, ProductDto data, Long version) {
        super(id);
        this.data = data;
        this.version = version;
    }

    @Override
    public String getEventType() {
        return "com.events.products.contracts.ProductUpserted";
    }
}
