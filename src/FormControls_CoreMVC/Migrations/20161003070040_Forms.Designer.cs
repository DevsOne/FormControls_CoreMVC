using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FormControls_CoreMVC.Models;

namespace FormControls_CoreMVC.Migrations
{
    [DbContext(typeof(FormsDbContext))]
    [Migration("20161003070040_Forms")]
    partial class Forms
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FormControls_CoreMVC.Models.Country", b =>
                {
                    b.Property<string>("ID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("FormControls_CoreMVC.Models.Course", b =>
                {
                    b.Property<string>("ID");

                    b.Property<bool>("Checked");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("FormControls_CoreMVC.Models.User", b =>
                {
                    b.Property<string>("ID");

                    b.Property<string>("Email");

                    b.Property<string>("Gender");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FormControls_CoreMVC.Models.UserCountry", b =>
                {
                    b.Property<string>("UserCountryID");

                    b.Property<string>("CountryID")
                        .IsRequired();

                    b.Property<string>("UserID")
                        .IsRequired();

                    b.HasKey("UserCountryID");

                    b.HasIndex("CountryID");

                    b.HasIndex("UserID");

                    b.ToTable("UserCountries");
                });

            modelBuilder.Entity("FormControls_CoreMVC.Models.UserCourse", b =>
                {
                    b.Property<string>("UserCourseID");

                    b.Property<bool>("Checked");

                    b.Property<string>("CourseID")
                        .IsRequired();

                    b.Property<string>("UserID")
                        .IsRequired();

                    b.HasKey("UserCourseID");

                    b.HasIndex("CourseID");

                    b.HasIndex("UserID");

                    b.ToTable("UserCourses");
                });

            modelBuilder.Entity("FormControls_CoreMVC.Models.UserDescription", b =>
                {
                    b.Property<string>("UserID");

                    b.Property<string>("Description");

                    b.HasKey("UserID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("UserDescriptions");
                });

            modelBuilder.Entity("FormControls_CoreMVC.Models.UserCountry", b =>
                {
                    b.HasOne("FormControls_CoreMVC.Models.Country", "Country")
                        .WithMany("UserCountries")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FormControls_CoreMVC.Models.User", "User")
                        .WithMany("UserCountries")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FormControls_CoreMVC.Models.UserCourse", b =>
                {
                    b.HasOne("FormControls_CoreMVC.Models.Course", "Course")
                        .WithMany("UserCourses")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FormControls_CoreMVC.Models.User", "User")
                        .WithMany("UserCourses")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FormControls_CoreMVC.Models.UserDescription", b =>
                {
                    b.HasOne("FormControls_CoreMVC.Models.User", "User")
                        .WithOne("UserDescription")
                        .HasForeignKey("FormControls_CoreMVC.Models.UserDescription", "UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
