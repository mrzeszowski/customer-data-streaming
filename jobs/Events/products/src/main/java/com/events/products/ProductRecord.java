package com.events.products;

import com.fasterxml.jackson.annotation.JsonProperty;

import java.math.BigDecimal;

public class ProductRecord {

    @JsonProperty("id")
    public String id;

    @JsonProperty("name")
    public String name;

    @JsonProperty("description")
    public String description;

    @JsonProperty("price")
    public BigDecimal price;

    @JsonProperty("category")
    public String category;

    @JsonProperty("version")
    public Long version;
}
