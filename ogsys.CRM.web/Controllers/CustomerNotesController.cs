using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ogsys.CRM.Data;
using ogsys.CRM.Models;
using Microsoft.AspNet.Identity;
using ogsys.CRM.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ogsys.CRM.Web.Controllers
{
    [Authorize]
    public class CustomerNotesController : Controller
    {
        private ICustomerDataService _customerService;

        public CustomerNotesController(ICustomerDataService service)
        {
            //_repository = genericRepository;
            this._customerService = service;
        }


        // GET: CustomerNotes
        public ActionResult Index(int id)
        {
            Customer customer = _customerService.GetById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // GET: CustomerNotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerNote customerNote = _customerService.GetCustomerNote(id.Value);
            if (customerNote == null)
            {
                return HttpNotFound();
            }
            return View(customerNote);
        }

        // GET: CustomerNotes/Create
        [HttpGet]
        public ActionResult Create(int customerId)
        {
            return View( new CustomerNote { CustomerId = customerId });
        }

        // POST: CustomerNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Body,CreateDate,CreatedBy,CustomerId")] CustomerNote customerNote)
        {
            if (ModelState.IsValid)
            {
                var test = User.Identity.Name;
                if (User.Identity.IsAuthenticated)
                {
                    using (var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
                    {
                        var currentUser = manager.FindById(User.Identity.GetUserId());
                        customerNote.CreatedBy = $"{currentUser.Firstname} {currentUser.Lastname}";
                    }
                }
                customerNote.CreateDate = DateTime.Now;
                _customerService.AddCustomerNote(customerNote);
                return RedirectToAction("Index", new { id = customerNote.CustomerId});
            }

            return View(customerNote);
        }

        // GET: CustomerNotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CustomerNote customerNote = _customerService.GetCustomerNote(id.Value);

            //CustomerNote customerNote = db.Notes.Find(id);
            if (customerNote == null)
            {
                return HttpNotFound();
            }
            return View(customerNote);
        }

        // POST: CustomerNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Body,CreateDate,CreatedBy,CustomerId")] CustomerNote customerNote)
        {
            if (ModelState.IsValid)
            {
                _customerService.UpdateCustomerNote(customerNote);
                return RedirectToAction("Index", new { id = customerNote.CustomerId });
            }
            return View(customerNote);
        }

        // GET: CustomerNotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerNote customerNote = _customerService.GetCustomerNote(id.Value);

//            CustomerNote customerNote = db.Notes.Find(id);
            if (customerNote == null)
            {
                return HttpNotFound();
            }
            return View(customerNote);
        }

        // POST: CustomerNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerNote customerNote = _customerService.GetCustomerNote(id);
            if (customerNote == null)
            {
                return HttpNotFound();
            }

            _customerService.DeleteCustomerNote(id);

            //CustomerNote customerNote = db.Notes.Find(id);
            //db.Notes.Remove(customerNote);
            //db.SaveChanges();
            return RedirectToAction("Index", new { id = customerNote.CustomerId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
            base.Dispose(disposing);
        }
    }
}
