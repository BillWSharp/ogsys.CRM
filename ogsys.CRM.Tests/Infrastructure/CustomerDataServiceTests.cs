using System;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using ogsys.CRM.Models;
using System.Linq;
using System.Data.Entity;
using ogsys.CRM.Data;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Collections;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Threading;

namespace ogsys.CRM.Tests.Infrastructure
{

    [TestFixture]
    public class CustomerDataServiceTests
    {

        [Test]
        public void Customer_GetAll()
        {
            // arrange
            TestContext testContext = SetupTestData();

            var service = new CustomerDataService(testContext);
            var expectedCount = 2;
            var resultType = typeof(Customer);

            //act
            var customerViewModelList = service.GetAll();

            //assert
            Assert.AreEqual(expectedCount, testContext.Customers.Count());
            Assert.AreEqual(expectedCount, customerViewModelList.Count());
            Assert.IsInstanceOf(resultType, customerViewModelList.FirstOrDefault());
        }

        [Test]
        public void Customer_GetAllCustomersViewModel_NoFilter()
        {
            // arrange
            TestContext testContext = SetupTestData();

            var service = new CustomerDataService(testContext);
            var expectedCount = 2;
            var resultType = typeof(CustomerListViewModel);
            //act
            var customerViewModelList = service.GetAllCustomersViewModel(null);

            //assert
            Assert.AreEqual(expectedCount, testContext.Customers.Count());
            Assert.AreEqual(expectedCount, customerViewModelList.Count());
            Assert.IsInstanceOf(resultType, customerViewModelList.FirstOrDefault());
        }



        [Test]
        public void Customer_GetAllCustomersViewModel_Filtered_ExpectedReturn()
        {
            // arrange
            TestContext testContext = SetupTestData();
            var service = new CustomerDataService(testContext);
            var filterText = "Jack";
            var submittedCount = 2;
            var expectedReturnCount = 1;
            var resultType = typeof(CustomerListViewModel);

            //act
            var customerViewModelList = service.GetAllCustomersViewModel(filterText);

            //assert
            Assert.AreEqual(submittedCount, testContext.Customers.Count());
            Assert.AreEqual(expectedReturnCount, customerViewModelList.Count());
            Assert.IsInstanceOf(resultType, customerViewModelList.FirstOrDefault());
        }

        [Test]
        public void Customer_GetAllCustomersViewModel_Filtered_ExpectedNoReturn()
        {
            // arrange
           TestContext testContext = SetupTestData();
            var service = new CustomerDataService(testContext);
            var filterText = "JackAndDiana";
            var submittedCount = 2;
            var expectedReturnCount = 0;
            var resultType = typeof(CustomerListViewModel);

            //act
            var customerViewModelList = service.GetAllCustomersViewModel(filterText);

            //assert
            Assert.AreEqual(submittedCount, testContext.Customers.Count());
            Assert.AreEqual(expectedReturnCount, customerViewModelList.Count());
        }



        [Test]
        public void Customer_Delete()
        {
            // arrange
            TestContext testContext = SetupTestData();

            var service = new CustomerDataService(testContext);
            var idToDelete = 1;
            var submittedCount = 2;
            var expectedReturnCount = 1;
            var resultType = typeof(CustomerListViewModel);
            var priorCount = testContext.Customers.Count();

            //act
            service.Delete(idToDelete);

            //assert
            Assert.AreEqual(submittedCount, priorCount);
            Assert.AreEqual(expectedReturnCount, testContext.Customers.Count());
        }


        [Test]
        public void Customer_Update()
        {
            // arrange
            TestContext testContext = SetupTestData();

            Customer customerToUpdate= new Customer
            {
                Firstname = "Customer2",
                Lastname = "NameChange",
                Email = "xyz@airmail.com",
                Phone = "1231231234",
                Address = "1234 First St.",
                Company = "N/A",
                Id = 2,
                Notes = new List<CustomerNote>
                    {
                        new CustomerNote {Body = "Great Customer!", CreatedBy="User1", CreateDate= DateTime.Now }
                        ,new CustomerNote {Body = "Repeat Customer!", CreatedBy="User1", CreateDate= DateTime.Now.AddDays(-1) }
                    }
            };

            var service = new CustomerDataService(testContext);

            //act
            service.Update(customerToUpdate);
            Customer updatedCustomer = service.GetById(2);

            //assert
            Assert.AreEqual("NameChange", updatedCustomer.Lastname);
        }

        [Test]
        public void Customer_InsertNew()
        {
            // arrange
            TestContext testContext = SetupTestData();

            Customer customerToInsert= new Customer
            {
                Firstname = "Customer3",
                Lastname = "Testcustomer3",
                Email = "xyz@airmail.com",
                Phone = "1231231234",
                Address = "1234 First St.",
                Company = "N/A",
                Id = 3,
                Notes = new List<CustomerNote>
                    {
                        new CustomerNote {Body = "Great Customer!", CreatedBy="User1", CreateDate= DateTime.Now }
                        ,new CustomerNote {Body = "Repeat Customer!", CreatedBy="User1", CreateDate= DateTime.Now.AddDays(-1) }
                    }
            };

            var service = new CustomerDataService(testContext);
            var priorCount = testContext.Customers.Count();
            var expectedCount = priorCount + 1;

            //act
            service.Insert(customerToInsert);
            var newCount = testContext.Customers.Count();


            //assert
            Assert.AreEqual(newCount, expectedCount);
        }


        [Test]
        public void CustomerNote_AddNew()
        {
            // arrange
            TestContext testContext = SetupTestData();

            var noteToInsert = new CustomerNote { Body = "Fabulous Customer!", CreatedBy = "User1", CreateDate = DateTime.Now, CustomerId = 1 };
            var service = new CustomerDataService(testContext);

            var priorCount = testContext.CustomerNotes.Where(x => x.CustomerId == 1).Count();
            var expectedCount = priorCount + 1;

            //act
            service.AddCustomerNote(noteToInsert);
            var newCount = testContext.CustomerNotes.Where(x => x.CustomerId == 1).Count();


            //assert
            Assert.AreEqual(newCount, expectedCount);
        }


        [Test]
        public void CustomerNote_GetById()
        {
            // arrange
            TestContext testContext = SetupTestData();
            var service = new CustomerDataService(testContext);

            var expectedText = "Great Customer!";
            var customerNoteID = 3;

            //act
            CustomerNote note = service.GetCustomerNote(customerNoteID);

            //assert
            Assert.AreEqual(note.Body, expectedText);
        }


        [Test]
        public void CustomerNote_DeleteById()
        {
            // arrange
            TestContext testContext = SetupTestData();

            var service = new CustomerDataService(testContext);
            var idToDelete = 3;
            var customerIdBeingDeleted = 2;
            var priorCount = testContext.CustomerNotes.Where(x => x.CustomerId == customerIdBeingDeleted).Count();
            var expectedReturnCount = priorCount - 1;

            //act
            service.DeleteCustomerNote(idToDelete);
            var remainingCount = testContext.CustomerNotes.Where(x => x.CustomerId == customerIdBeingDeleted).Count();

            //assert
            Assert.AreEqual(expectedReturnCount, remainingCount);
        }


        [Test]
        public void CustomerNote_Update()
        {
            // arrange
            TestContext testContext = SetupTestData();
            var noteToUpdate= new CustomerNote { Body = "Fabulous Customer!", CreatedBy = "User1", CreateDate = DateTime.Now, CustomerId = 1, Id = 1 };
            var updatedNoteId = 1;
            var service = new CustomerDataService(testContext);
            var priorText = "Great Customer!";
            var ExpectedText = "Fabulous Customer!";

            //act
            service.UpdateCustomerNote(noteToUpdate);
            CustomerNote note = service.GetCustomerNote(updatedNoteId);

            //assert
            Assert.AreEqual(ExpectedText, note.Body);
        }


        //public void UpdateCustomerNote(CustomerNote note)
        //{
        //    _CustomerData.UpdateCustomerNote(note);
        //}



        private static TestContext SetupTestData()
        {
            var testContext = new TestContext();
            testContext.Customers.Add(
                new Customer
                {
                    Firstname = "Jack",
                    Lastname = "LastCustomer1",
                    Email = "xyz@airmail.com",
                    Phone = "1231231234",
                    Address = "1234 First St.",
                    Company = "N/A",
                    Id = 1,
                    Notes = new List<CustomerNote>
                    {
                        new CustomerNote {Body = "Great Customer!", CreatedBy="User1", CreateDate= DateTime.Now }
                        ,new CustomerNote {Body = "Repeat Customer!", CreatedBy="User1", CreateDate= DateTime.Now.AddDays(-1) }
                    }
                });
            testContext.Customers.Add(
                new Customer
                {
                    Firstname = "Customer2",
                    Lastname = "LastCustomer2",
                    Email = "xyz@airmail.com",
                    Phone = "1231231234",
                    Address = "1234 First St.",
                    Company = "N/A",
                    Id = 2,
                    Notes = new List<CustomerNote>
                    {
                        new CustomerNote {Body = "Great Customer!", CreatedBy="User1", CreateDate= DateTime.Now }
                        ,new CustomerNote {Body = "Repeat Customer!", CreatedBy="User1", CreateDate= DateTime.Now.AddDays(-1) }
                    }
                });

            testContext.CustomerNotes.Add(new CustomerNote { Body = "Great Customer!", CreatedBy = "User1", CreateDate = DateTime.Now, Id = 1, CustomerId = 1 });
            testContext.CustomerNotes.Add(new CustomerNote { Body = "Repeat Customer!", CreatedBy = "User1", CreateDate = DateTime.Now.AddDays(-1), Id = 2, CustomerId = 1 });
            testContext.CustomerNotes.Add(new CustomerNote { Body = "Great Customer!", CreatedBy = "User1", CreateDate = DateTime.Now, Id = 3, CustomerId = 2 });
            testContext.CustomerNotes.Add(new CustomerNote { Body = "Repeat Customer!", CreatedBy = "User1", CreateDate = DateTime.Now.AddDays(-1), Id = 4, CustomerId = 2 });






            return testContext;
        }
    }







}

