using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ogsys.CRM.Models;

namespace ogsys.CRM.Data
{
    public interface ICustomerData
    {
        List<Customer> GetAll();

        Customer GetById(int id);

        void Insert(Customer entity);

        void Update(Customer entity);

        void Delete(int id);

        void AddCustomerNote(CustomerNote note);

        CustomerNote GetCustomerNote(int id);

        void UpdateCustomerNote(CustomerNote note);

        void DeleteCustomerNote(int customerNoteId);
    }
}
