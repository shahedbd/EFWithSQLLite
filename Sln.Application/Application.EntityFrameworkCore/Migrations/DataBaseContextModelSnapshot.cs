﻿// <auto-generated />
using System;
using Application.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Application.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("Application.Core.PersonalInfo", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreationUser");

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<DateTime?>("LastModifiedDate");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("LastUpdateUser");

                    b.Property<string>("MobileNo");

                    b.Property<string>("NID");

                    b.Property<byte>("Status");

                    b.HasKey("ID");

                    b.ToTable("PersonalInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
