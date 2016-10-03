using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormControls_CoreMVC.Models
{
    public class User
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }

        public IList<UserCourse> UserCourses { get; set; }
        public UserDescription UserDescription { get; set; }
        public IList<UserCountry> UserCountries { get; set; }
    }
    public class Country
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public IList<UserCountry> UserCountries { get; set; }
    }
    public class UserCountry
    {
        [Key]
        public string UserCountryID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public string CountryID { get; set; }

        public User User { get; set; }
        public Country Country { get; set; }
    }
    public class Course
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }

        public IList<UserCourse> UserCourses { get; set; }
    }
    public class UserCourse
    {
        [Key]
        public string UserCourseID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public string CourseID { get; set; }
        public bool Checked { get; set; }

        public Course Course { get; set; }
        public User User { get; set; }
    }
    public class UserDescription
    {
        [Key, ForeignKey("User")]
        public string UserID { get; set; }
        public string Description { get; set; }

        public User User { get; set; }
    }
    public class FormControlsViewModel
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public List<Course> Courses { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public string Description { get; set; }
    }
}
