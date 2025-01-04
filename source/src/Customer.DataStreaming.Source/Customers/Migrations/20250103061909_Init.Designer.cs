﻿// <auto-generated />
using System;
using Customer.DataStreaming.Source.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Customers.Migrations
{
    [DbContext(typeof(CustomerContext))]
    [Migration("20250103061909_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("customers")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Customer.DataStreaming.Source.Customers.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("last_name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("bigint")
                        .HasColumnName("version");

                    b.HasKey("Id")
                        .HasName("pk_customer");

                    b.ToTable("customer", "customers");
                });

            modelBuilder.Entity("Customer.DataStreaming.Source.Customers.Customer", b =>
                {
                    b.OwnsOne("Customer.DataStreaming.Source.Customers.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("CustomerId");

                            b1.ToTable("customer", "customers");

                            b1.ToJson("address");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId")
                                .HasConstraintName("fk_customer_customer_id");
                        });

                    b.OwnsMany("Customer.DataStreaming.Source.Customers.CommunicationChannel", "CommunicationChannels", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uuid");

                            b1.Property<int>("__synthesizedOrdinal")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<bool>("IsPrimary")
                                .HasColumnType("boolean");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("CustomerId", "__synthesizedOrdinal");

                            b1.ToTable("customer", "customers");

                            b1.ToJson("communication_channels");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId")
                                .HasConstraintName("fk_customer_customer_customer_id");
                        });

                    b.OwnsMany("Customer.DataStreaming.Source.Customers.Consent", "Consents", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uuid");

                            b1.Property<int>("__synthesizedOrdinal")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<bool>("IsGranted")
                                .HasColumnType("boolean");

                            b1.Property<DateTime>("Timestamp")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("CustomerId", "__synthesizedOrdinal");

                            b1.ToTable("customer", "customers");

                            b1.ToJson("consents");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId")
                                .HasConstraintName("fk_customer_customer_customer_id");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("CommunicationChannels");

                    b.Navigation("Consents");
                });
#pragma warning restore 612, 618
        }
    }
}
