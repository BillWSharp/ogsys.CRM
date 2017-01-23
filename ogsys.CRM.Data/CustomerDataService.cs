using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ogsys.CRM.Models;

namespace ogsys.CRM.Data
{
    public class CustomerDataService : ICustomerDataService, IDisposable
    {

        ICustomerData _CustomerData;

        public CustomerDataService(ICustomerData customerData)
        {
            _CustomerData = customerData;
        }

        public IEnumerable<CustomerListViewModel> GetAllCustomersViewModel(string nameSearch)
        {

            var entireList = _CustomerData
                .GetAll()
                .Where(x => nameSearch == null 
                            || x.Lastname.StartsWith(nameSearch, StringComparison.OrdinalIgnoreCase)
                            || x.Firstname.StartsWith(nameSearch, StringComparison.OrdinalIgnoreCase))
                  .Select(x =>
                     new CustomerListViewModel
                     {
                         Id = x.Id,
                         Firstname = x.Firstname,
                         Lastname = x.Lastname,
                         CountOfNotes = x.Notes == null
                                        ? 0
                                        :x.Notes.Count()
                     }
                    );
            return entireList.ToList()  ;
        }

        public void Delete(int id)
        {
            _CustomerData.Delete(id);
        }


        public IEnumerable<Customer> GetAll()
        {
            return _CustomerData.GetAll();
        }

        public Customer GetById(int id)
        {
            return _CustomerData.GetById(id);
        }

        public void Insert(Customer entity)
        {
            _CustomerData.Insert(entity);
        }

        public void Update(Customer entity)
        {
            _CustomerData.Update(entity);
        }

        public void AddCustomerNote(CustomerNote note)
        {
            _CustomerData.AddCustomerNote(note);
        }

        public CustomerNote GetCustomerNote(int id)
        {
            return _CustomerData.GetCustomerNote(id);
        }

        public void UpdateCustomerNote(CustomerNote note)
        {
            _CustomerData.UpdateCustomerNote(note);
        }

        public void DeleteCustomerNote(int customerNoteId)
        {
            _CustomerData.DeleteCustomerNote(customerNoteId);
        }


        public void Dispose()
        {
            {
                if (_CustomerData != null)
                {
                    (_CustomerData as IDisposable).Dispose();
                }
            }
        }
    }
}
