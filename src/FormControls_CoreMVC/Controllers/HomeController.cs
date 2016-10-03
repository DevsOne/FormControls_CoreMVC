using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FormControls_CoreMVC.DAL;
using FormControls_CoreMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormControls_CoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFormRepository _fRepo;
        public HomeController(IFormRepository fRepo)
        {
            _fRepo = fRepo;
        }
        public IActionResult Index()
        {
            List<FormControlsViewModel> userList = new List<FormControlsViewModel>();
            var users = _fRepo.GetAllUsers();

            foreach (var user in users)
            {
                List<Course> courseNames = new List<Course>();
                var countryName = _fRepo.GetCountry(linqWhereUserCountry: x => x.UserID == user.ID).Name;
                var description = _fRepo.GetUserDescription(x => x.UserID == user.ID).Description;
                if (_fRepo.HasAnyCourse(user.ID))
                {
                    var courseIds = _fRepo.GetUserCourseIds(user.ID);
                    foreach (var courseId in courseIds) { courseNames.Add(new Course { Name = _fRepo.GetCourse(x => x.ID == courseId).Name }); }
                }
                courseNames = courseNames.OrderBy(x => x.Name).ToList();
                userList.Add(new FormControlsViewModel { Name = user.Name, Email = user.Email, Gender = user.Gender, Country = countryName, UserID = user.ID, Description = description, Courses = courseNames });
            }
            return View(userList);
        }

        public IActionResult Create()
        {
            var model = new FormControlsViewModel();
            model.Courses = CreateCourseList();
            model.Countries = CreateCountryList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(FormControlsViewModel model)
        {
            var user = _fRepo.AddUser(model);
            _fRepo.AddUserCountry(model.Country, null, user);
            _fRepo.AddUserDescription(user.ID, model.Description);
            _fRepo.AddUserCourses(model.Courses, user.ID);
            _fRepo.Save();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(string userId, FormControlsViewModel model)
        {
            var user = _fRepo.GetUser(x => x.ID == userId);
            model.UserID = userId;
            model.Name = user.Name;
            model.Email = user.Email;
            model.Gender = user.Gender;
            model.Description = _fRepo.GetUserDescription(x => x.UserID == userId).Description;
            model.Country = _fRepo.GetCountry(linqWhereUserCountry: x => x.UserID == userId).Name;
            model.Courses = CreateCourseList(user);
            model.Countries = CreateCountryList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(FormControlsViewModel model)
        {
            var user = _fRepo.GetUser(x => x.ID == model.UserID);
            var userDescription = _fRepo.GetUserDescription(x => x.UserID == user.ID);
            var userCountry = _fRepo.GetUserCountry(x => x.UserID == user.ID);
            var selectedCountryId = _fRepo.GetCountry(linqWhereCountry: x => x.Name == model.Country).ID;

            _fRepo.UpdateUser(user, model);
            _fRepo.UpdateUserDescription(userDescription, model);
            
            if (userCountry.CountryID != selectedCountryId)
            {
                if (userCountry != null) { _fRepo.RemoveUserCountry(userCountry); _fRepo.Save(); }
                _fRepo.AddUserCountry(null, selectedCountryId, user);
            }

            if (!_fRepo.CheckNewCourseList(user, model.Courses))
            {
                _fRepo.RemovUserCourses(user.ID);
                _fRepo.Save();
                if (model.Courses != null) { _fRepo.AddUserCourses(model.Courses, user.ID); }
            }

            _fRepo.Save();

            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult Delete(string userId)
        {
            _fRepo.RemovUserCourses(userId);
            _fRepo.RemoveUserCountry(userId);
            _fRepo.RemoveUserDescription(userId);
            _fRepo.RemoveUser(userId);
            _fRepo.Save();
            return RedirectToAction("index", "Home");
        }

        public IEnumerable<SelectListItem> CreateCountryList()
        {
            List<SelectListItem> countryNames = new List<SelectListItem>();
            var countries = _fRepo.GetAllCountries();
            foreach (var country in countries)
            {
                countryNames.Add(new SelectListItem { Text = country.Name, Value = country.Name });
            }
            return countryNames.GroupBy(x => x.Text).Select(x => x.FirstOrDefault()).ToList().OrderBy(x => x.Text);
        }
        public List<Course> CreateCourseList()
        {
            var courses = new List<Course>();
            var allCourses = _fRepo.GetAllCourses();
            foreach (var course in allCourses)
            {
                courses.Add(new Course { Name = course.Name, ID = course.ID });
            }
            return courses;
        }
        public List<Course> CreateCourseList(User user)
        {
            var courses = new List<Course>();
            var allCourses = _fRepo.GetAllCourses();
            foreach (var course in allCourses)
            {
                var c = new Course() { Name = course.Name, ID = course.ID };
                if (_fRepo.IsCourseChecked(user.ID, course.ID)) { c.Checked = true; }
                else { c.Checked = false; }
                courses.Add(c);
            }
            return courses;
        }
    }
}
