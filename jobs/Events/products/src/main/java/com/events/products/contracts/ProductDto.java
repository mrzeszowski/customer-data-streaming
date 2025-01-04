package com.events.products.contracts;

import java.math.BigDecimal;

public class ProductDto {

    public String name;
    public String description;
    public BigDecimal price;
    public String category;

    public ProductDto(String name, String description, BigDecimal price, String category) {

        this.name = name;
        this.description = description;
        this.price = price;
        this.category = category;
    }
}
