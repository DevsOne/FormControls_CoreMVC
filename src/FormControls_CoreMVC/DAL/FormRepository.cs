using FormControls_CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FormControls_CoreMVC.DAL
{
    public class FormRepository : IFormRepository
    {
        private FormsDbContext _context;
        public FormRepository(FormsDbContext context)
        {
            _context = context;
        }

        public User AddUser(FormControlsViewModel model)
        {
            var user = new User { ID = Guid.NewGuid().ToString(), Name = model.Name, Email = model.Email, Gender = model.Gender };
            _context.Users.Add(user);
            return user;
        }
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        public User GetUser(Expression<Func<User, bool>> linqWhere)
        {
            return _context.Users.Where(linqWhere).FirstOrDefault();
        }
        public void RemoveUser(string userId)
        {
            var user = GetUser(x => x.ID == userId);
            _context.Users.Remove(user);
        }
        public void UpdateUser(User user,FormControlsViewModel model)
        {
            user.Email = model.Email;
            user.Gender = model.Gender;
            user.Name = model.Name;
            _context.Users.Update(user);
        }

        public void AddUserCountry(string countryName, string countryId, User user)
        {
            if (countryId == null) { countryId = GetCountry(linqWhereCountry: x => x.Name == countryName).ID; }
            var userCountry = new UserCountry { UserCountryID = Guid.NewGuid().ToString(), UserID = user.ID, CountryID = countryId };
            _context.UserCountries.Add(userCountry);
        }
        public List<Country> GetAllCountries()
        {
            return _context.Countries.ToList();
        }
        public Country GetCountry(Expression<Func<Country, bool>> linqWhereCountry)
        {
            return _context.Countries.Where(linqWhereCountry).FirstOrDefault();
        }
        public Country GetCountry(Expression<Func<UserCountry, bool>> linqWhereUserCountry)
        {
            var userCountry = _context.UserCountries.Where(linqWhereUserCountry).FirstOrDefault();
            return GetCountry(linqWhereCountry: x => x.ID == userCountry.CountryID);
        }
        public UserCountry GetUserCountry(Expression<Func<UserCountry, bool>> linqWhere)
        {
            return _context.UserCountries.Where(linqWhere).FirstOrDefault();
        }
        public void RemoveUserCountry(UserCountry userCountry)
        {
            _context.UserCountries.Remove(userCountry);
        }
        public void RemoveUserCountry(string userid)
        {
            var userCountry = GetUserCountry(x => x.UserID == userid);
            _context.UserCountries.Remove(userCountry);
        }

        public void AddUserCourses(List<Course> courses, string userId)
        {
            foreach (var course in courses)
            {
                if (course.Checked == true)
                {
                    var userCourse = new UserCourse { UserCourseID = Guid.NewGuid().ToString(), UserID = userId, CourseID = course.ID, Checked = true };
                    _context.UserCourses.Add(userCourse);
                }
            }
        }
        public List<Course> GetAllCourses()
        {
            return _context.Courses.ToList();
        }
        public Course GetCourse(Expression<Func<Course, bool>> linqWhere)
        {
            return _context.Courses.Where(linqWhere).FirstOrDefault();
        }
        public List<string> GetUserCourseIds(string userId)
        {
            return _context.UserCourses.Where(u => u.UserID == userId).Select(x => x.CourseID).ToList();
        }
        public void RemovUserCourses(string userId)
        {
            var userCourses = _context.UserCourses.Where(x => x.UserID == userId).ToList();
            foreach (var course in userCourses) { _context.UserCourses.Remove(course); }
        }
        public bool CheckNewCourseList(User user, List<Course> newCourseList)
        {
            var userCourseList = GetUserCourseIds(user.ID);
            if (userCourseList == null && newCourseList == null) { return true; }
            else if (userCourseList != null && newCourseList == null) { return false; }
            newCourseList = newCourseList.Where(x => x.Checked == true).ToList();
            List<string> newCourseIds = new List<string>();
            foreach (var course in newCourseList) { newCourseIds.Add(course.ID); }

            return userCourseList.SequenceEqual(newCourseIds);
        }
        public bool IsCourseChecked(string userId, string courseId)
        {
            return _context.UserCourses.Where(x => x.UserID == userId && x.CourseID == courseId).Select(x => x.Checked).FirstOrDefault();
        }
        public bool HasAnyCourse(string userId)
        {
            return _context.UserCourses.Where(x => x.UserID == userId).Any();
        }

        public void AddUserDescription(string userId, string description)
        {
            var userDescription = new UserDescription { UserID = userId, Description = description };
            _context.UserDescriptions.Add(userDescription);
        }
        public UserDescription GetUserDescription(Expression<Func<UserDescription, bool>> linqWhere)
        {
            return _context.UserDescriptions.Where(linqWhere).FirstOrDefault();
        }
        public void RemoveUserDescription(string userId)
        {
            var userDescription = GetUserDescription(x => x.UserID == userId);
            _context.UserDescriptions.Remove(userDescription);
        }
        public void UpdateUserDescription(UserDescription userDescription, FormControlsViewModel model)
        {
            userDescription.Description = model.Description;
            _context.UserDescriptions.Update(userDescription);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #region dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion dispose
    }
}
