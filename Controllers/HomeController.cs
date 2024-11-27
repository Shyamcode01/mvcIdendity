using dummyIdentity.Areas.Identity.Data;
using dummyIdentity.Models;
using dummyIdentity.Repository;
using dummyIdentity.Repository.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace dummyIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly dbContextsimple _dbContext;
        private readonly EmailService _emailService;

        public HomeController(ILogger<HomeController> logger,
            dbContextsimple contextsimple,EmailService email)
        {
            _logger = logger;
            _dbContext = contextsimple;
            _emailService = email;
        }

        public IActionResult Index()
        {
            return View();
        }

        // contact view
        public IActionResult Contact()
        {
            return View();
        }

        // contact send data email
        [HttpPost]
        public IActionResult Contact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string subject = "New Contact Form";
            string body = $@"
                    <h3>Contact Details</h3>
                    <p><strong>Name:</strong> {contact.Name}</p>
                    <p><strong>Email:</strong> {contact.Email}</p>
                    <p><strong>Mobile:</strong> {contact.Mobile}</p>
                    <p><strong>Message:</strong> {contact.TextMessage}</p>";

                // email send 

            _emailService.SendEmailAsync("shyamyadav121240@gmail.com",subject,body);
           
            return RedirectToAction("ThanksPage");
        }


        // thanks page after submit contact detail
        public IActionResult ThanksPage()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

       


        [HttpPost]
   
        public IActionResult Create(Employees emp)
        {

           
            if(!ModelState.IsValid)
            {
                return View("data null");
            }
           _dbContext.employees.Add(emp);
            _dbContext.SaveChanges();
            return RedirectToAction ("Employees");
           
        }

        [HttpGet]
        [Authorize]
        
        public IActionResult Employees(string SearchString)
        {
           

           
            var data=_dbContext.employees.ToList();
            if (!string.IsNullOrEmpty(SearchString))
            {
                data=data.Where(x=>x.FirstName.ToLower().Contains(SearchString.ToLower())).ToList();
               
            }
 
            if (data==null || !data.Any())
            {
                ViewBag.Message = "No employee found";
                return View(new List<Employees>());
            }
           
            return View(data);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data=_dbContext.employees.Find(id);
            return View(data);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConform(int id)
        {
            var data = _dbContext.employees.FirstOrDefault(x=>x.id==id);
            if (data == null)
            {
                return View();
            }
            _dbContext.employees.Remove(data);
            _dbContext.SaveChanges();
            return RedirectToAction("Employees");
           
        }
         
    }
}
