package com.events.customers.contracts;

public class AddressDto {

    public String city;
    public String state;
    public String street;
    public String country;
    public String postalCode;

    public AddressDto(String city, String state, String street, String country, String postalCode) {

        this.city = city;
        this.state = state;
        this.street = street;
        this.country = country;
        this.postalCode = postalCode;
    }
}