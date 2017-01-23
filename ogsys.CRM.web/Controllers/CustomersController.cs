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
using ogsys.CRM.Web.Models;

namespace ogsys.CRM.Web.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {

        private ICustomerDataService _service;
        public CustomersController(ICustomerDataService service)
        {
            this._service = service;
        }

        // GET: Customers
        public ActionResult Index(string nameSearch = null)
        {

            var entireList = _service.GetAllCustomersViewModel(nameSearch);

            return View(entireList);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = _service.GetById(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Firstname,Lastname,Company,Email,Address,Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _service.Insert(customer);
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = _service.GetById(id.Value);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Firstname,Lastname,Company,Email,Address,Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _service.Update(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = _service.GetById(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = _service.GetById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            _service.Delete(id);
            return RedirectToAction("Index");

        }

        

    }
}
