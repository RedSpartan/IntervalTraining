﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RedSpartan.IntervalTraining.Internal.Repository.Access;

namespace RedSpartan.IntervalTraining.Repository.Internal.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200411100217_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("RedSpartan.IntervalTraining.Repository.Internal.Data.Entities.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Intervals")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Start")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Stop")
                        .HasColumnType("TEXT");

                    b.Property<int>("TemplateId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TimeActiveSeconds")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("RedSpartan.IntervalTraining.Repository.Internal.Data.Entities.IntervalTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Intervals")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Iterations")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("TimeSeconds")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Intervals");
                });

            modelBuilder.Entity("RedSpartan.IntervalTraining.Repository.Internal.Data.Entities.History", b =>
                {
                    b.HasOne("RedSpartan.IntervalTraining.Repository.Internal.Data.Entities.IntervalTemplate", "Template")
                        .WithMany("History")
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
