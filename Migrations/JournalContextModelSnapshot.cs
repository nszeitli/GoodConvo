﻿// <auto-generated />
using System;
using GoodConvo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GoodConvo.Migrations
{
    [DbContext(typeof(JournalContext))]
    partial class JournalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GoodConvo.Models.EntityModels.Coach", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author");

                    b.Property<bool>("IsPrivate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Coaches");
                });

            modelBuilder.Entity("GoodConvo.Models.EntityModels.Conversation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CoachId");

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Document");

                    b.Property<string>("SessionTag");

                    b.Property<int?>("UserDataId");

                    b.Property<bool>("inProgress");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.HasIndex("UserDataId");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("GoodConvo.Models.EntityModels.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CoachId");

                    b.Property<int?>("ConversationId");

                    b.Property<int>("Index");

                    b.Property<string>("QuestionText");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.HasIndex("ConversationId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("GoodConvo.Models.EntityModels.Response", b =>
                {
                    b.Property<string>("GUID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ConversationId");

                    b.Property<int>("Index");

                    b.Property<bool>("IsTextResponse");

                    b.Property<int>("NumResponse");

                    b.Property<string>("TextResponse");

                    b.HasKey("GUID");

                    b.HasIndex("ConversationId");

                    b.ToTable("Response");
                });

            modelBuilder.Entity("GoodConvo.Models.EntityModels.UserData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.HasKey("Id");

                    b.ToTable("UserData");
                });

            modelBuilder.Entity("GoodConvo.Models.EntityModels.Conversation", b =>
                {
                    b.HasOne("GoodConvo.Models.EntityModels.Coach", "Coach")
                        .WithMany()
                        .HasForeignKey("CoachId");

                    b.HasOne("GoodConvo.Models.EntityModels.UserData", "UserData")
                        .WithMany("PreviousConversations")
                        .HasForeignKey("UserDataId");
                });

            modelBuilder.Entity("GoodConvo.Models.EntityModels.Question", b =>
                {
                    b.HasOne("GoodConvo.Models.EntityModels.Coach")
                        .WithMany("QuestionList")
                        .HasForeignKey("CoachId");

                    b.HasOne("GoodConvo.Models.EntityModels.Conversation")
                        .WithMany("QuestionsAsked")
                        .HasForeignKey("ConversationId");
                });

            modelBuilder.Entity("GoodConvo.Models.EntityModels.Response", b =>
                {
                    b.HasOne("GoodConvo.Models.EntityModels.Conversation")
                        .WithMany("ResponseList")
                        .HasForeignKey("ConversationId");
                });
#pragma warning restore 612, 618
        }
    }
}
