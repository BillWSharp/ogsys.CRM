using ogsys.CRM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ogsys.CRM.Data
{
    public class CustomerData: ICustomerData, IDisposable
    {

        private CustomersContext _customerDbContext;
        public CustomerData(CustomersContext customerDbContext)
        {
            this._customerDbContext = customerDbContext;
        }

        public List<Customer> GetAll()
        {
            return _customerDbContext
                    .Customers
                    .AsNoTracking()
                    .ToList();
        }

        public Customer GetById(int id)
        {
            return _customerDbContext
                    .Customers
                    .Find(id);
        }

        public void Insert(Customer entity)
        {
            _customerDbContext.Customers.Add(entity);
            _customerDbContext.SaveChanges();
        }

        public void Update(Customer entity)
        {
            _customerDbContext.Customers.Attach(entity);
            _customerDbContext.Entry(entity).State = EntityState.Modified;
            _customerDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entityToDelete = _customerDbContext.Customers.Find(id);
            if (entityToDelete == null)
                return;
            _customerDbContext.Customers.Remove(entityToDelete);
            _customerDbContext.SaveChanges();
        }

        public void AddCustomerNote(CustomerNote note)
        {
            _customerDbContext.Notes.Add(note);
            _customerDbContext.SaveChanges();
        }

        public CustomerNote GetCustomerNote(int id)
        {
            return _customerDbContext.Notes.Find(id);
        }

        public void UpdateCustomerNote(CustomerNote note)
        {
            _customerDbContext.Notes.Attach(note);
            _customerDbContext.Entry(note).State = EntityState.Modified;
            _customerDbContext.SaveChanges();
        }

        public void DeleteCustomerNote(int customerNoteId)
        {
            CustomerNote customerNote = _customerDbContext.Notes.Find(customerNoteId);
            _customerDbContext.Notes.Remove(customerNote);
            _customerDbContext.SaveChanges();
        }


        public void Dispose()
        {
            if (_customerDbContext  != null) _customerDbContext.Dispose();
        }

    }
}
