package com.events.customers.contracts;

import com.events.customers.ConsentRecord;

import java.util.List;

public class CustomerDto {

    public String firstName;
    public String lastName;
    public String email;
    public String dateOfBirth;
    public AddressDto address;
    public List<CommunicationChannelDto> communicationChannels;
    public List<ConsentDto> consents;
    public String createdAt;
    public String updatedAt;

    public CustomerDto(String firstName,
                       String lastName,
                       String email,
                       String dateOfBirth,
                       AddressDto address,
                       List<CommunicationChannelDto> communicationChannels,
                       List<ConsentDto> consents,
                       String createdAt,
                       String updatedAt) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.dateOfBirth = dateOfBirth;
        this.address = address;
        this.communicationChannels = communicationChannels;
        this.consents = consents;
        this.createdAt = createdAt;
        this.updatedAt = updatedAt;
    }

}
