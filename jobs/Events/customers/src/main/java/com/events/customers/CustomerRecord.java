package com.events.customers;

import com.fasterxml.jackson.annotation.JsonProperty;

public class CustomerRecord {
    public String id;
    @JsonProperty("first_name")
    public String firstName;
    @JsonProperty("last_name")
    public String lastName;
    public String email;
    @JsonProperty("date_of_birth")
    public String dateOfBirth;
    @JsonProperty("created_at")
    public String createdAt;
    @JsonProperty("updated_at")
    public String updatedAt;
    public Long version;
}
