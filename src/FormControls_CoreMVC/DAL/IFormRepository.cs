using FormControls_CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FormControls_CoreMVC.DAL
{
    public interface IFormRepository : IDisposable
    {
        User AddUser(FormControlsViewModel model);
        List<User> GetAllUsers();
        User GetUser(Expression<Func<User, bool>> linqWhere);
        void RemoveUser(string userId);
        void UpdateUser(User user, FormControlsViewModel model);

        void AddUserCountry(string countryName, string countryId, User user);
        List<Country> GetAllCountries();
        Country GetCountry(Expression<Func<Country, bool>> linqWhereCountry);
        Country GetCountry(Expression<Func<UserCountry, bool>> linqWhereUserCountry);
        UserCountry GetUserCountry(Expression<Func<UserCountry, bool>> linqWhere);
        void RemoveUserCountry(string userid);
        void RemoveUserCountry(UserCountry userCountry);

        void AddUserCourses(List<Course> courses, string userId);
        List<Course> GetAllCourses();
        Course GetCourse(Expression<Func<Course, bool>> linqWhere);
        List<string> GetUserCourseIds(string userId);
        void RemovUserCourses(string userId);
        bool CheckNewCourseList(User user, List<Course> newCourseList);
        bool IsCourseChecked(string userId, string courseId);
        bool HasAnyCourse(string userId);

        void AddUserDescription(string userId, string description);
        UserDescription GetUserDescription(Expression<Func<UserDescription, bool>> linqWhere);
        void RemoveUserDescription(string userId);
        void UpdateUserDescription(UserDescription userDescription, FormControlsViewModel model);

        void Save();
    }
}

