using System;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using ogsys.CRM.Models;
using System.Linq;
using System.Data.Entity;
using ogsys.CRM.Data;

namespace ogsys.CRM.Tests.Infrastructure
{
    [TestFixture]
    public class CustomerDataTests
    {
        [Test]
        public void GetAllCustomers_ListofCustomer_Count()
        {
            //arrange

            var data = new List<Customer>
            {
                new Customer { Firstname="Customer1", Lastname="LastCustomer1", Email="xyz@airmail.com", Phone="1231231234",  Address="1234 First St.", Company="N/A", Id=1,
                    Notes = new List<CustomerNote>
                    {
                        new CustomerNote {Body = "Great Customer!", CreatedBy="User1", CreateDate= DateTime.Now }
                        ,new CustomerNote {Body = "Repeat Customer!", CreatedBy="User1", CreateDate= DateTime.Now.AddDays(-1) }
                    }
                },
                new Customer { Firstname="Customer2", Lastname="LastCustomer2", Email="xyz@airmail.com", Phone="1231231234",  Address="1234 First St.", Company="N/A", Id=2,
                    Notes = new List<CustomerNote>
                    {
                        new CustomerNote {Body = "Great Customer!", CreatedBy="User1", CreateDate= DateTime.Now }
                        ,new CustomerNote {Body = "Repeat Customer!", CreatedBy="User1", CreateDate= DateTime.Now.AddDays(-1) }
                    }
                }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            // only needed if using AsNoTracking
            mockSet.Setup(x => x.AsNoTracking()).Returns(mockSet.Object);

            var mockContext = new Mock<CustomersContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);


            var customerData = new CustomerData(mockContext.Object);
            int expectedResult = 2;

            //act
            var returnedData = customerData.GetAll();
            var returnedCount = returnedData.Count;

            //assert
            Assert.AreEqual(expectedResult, returnedCount);
        }

        [Test]
        public void GetCustomer_FromListOfCustomer()
        {
            //arrange

            var data = new List<Customer>
            {
                new Customer { Firstname="Customer1", Lastname="LastCustomer1", Email="xyz@airmail.com", Phone="1231231234",  Address="1234 First St.", Company="N/A", Id=1,
                    Notes = new List<CustomerNote>
                    {
                        new CustomerNote {Body = "Great Customer!", CreatedBy="User1", CreateDate= DateTime.Now }
                        ,new CustomerNote {Body = "Repeat Customer!", CreatedBy="User1", CreateDate= DateTime.Now.AddDays(-1) }
                    }
                },
                new Customer { Firstname="Customer2", Lastname="LastCustomer2", Email="xyz@airmail.com", Phone="1231231234",  Address="1234 First St.", Company="N/A", Id=2,
                    Notes = new List<CustomerNote>
                    {
                        new CustomerNote {Body = "Great Customer!", CreatedBy="User1", CreateDate= DateTime.Now }
                        ,new CustomerNote {Body = "Repeat Customer!", CreatedBy="User1", CreateDate= DateTime.Now.AddDays(-1) }
                    }
                }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            // only needed if using AsNoTracking
            //mockSet.Setup(x => x.AsNoTracking()).Returns(mockSet.Object);
            mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));

            var mockContext = new Mock<CustomersContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);


            var customerData = new CustomerData(mockContext.Object);
            int expectedId = 2;

            //act
            var returnedData = customerData.GetById(expectedId);
            var returnedId = returnedData.Id;

            //assert
            Assert.AreEqual(expectedId, returnedId);
        }

        [Test]
        public void InsertCustomer_IntoCustomers()
        {
            //arrange
            var insertCustomer = new Customer
            {
                Firstname = "Customer3",
                Lastname = "LastCustomer3",
                Email = "xyz@airmail.com",
                Phone = "9879879876",
                Address = "4444 First St.",
                Company = "N/A",
                Id = 3,
                Notes = new List<CustomerNote>
                    {
                        new CustomerNote {Body = "Great Customer!", CreatedBy="User1", CreateDate= DateTime.Now }
                        ,new CustomerNote {Body = "Repeat Customer!", CreatedBy="User1", CreateDate= DateTime.Now.AddDays(-1) }
                    }
            };

            var mockSet = new Mock<DbSet<Customer>>();
            var mockContext = new Mock<CustomersContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);
            var customerData = new CustomerData(mockContext.Object);

            //act
            customerData.Insert(insertCustomer);

            //assert
            mockSet.Verify(m => m.Add(It.IsAny<Customer>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }


        [Test]
        public void RemoveCustomer_FromCustomerList()
        {
            //arrange

            var data = new List<Customer>
            {
                new Customer { Firstname="Customer1", Lastname="LastCustomer1", Email="xyz@airmail.com", Phone="1231231234",  Address="1234 First St.", Company="N/A", Id=1,
                    Notes = new List<CustomerNote>
                    {
                        new CustomerNote {Body = "Great Customer!", CreatedBy="User1", CreateDate= DateTime.Now }
                        ,new CustomerNote {Body = "Repeat Customer!", CreatedBy="User1", CreateDate= DateTime.Now.AddDays(-1) }
                    }
                },
                new Customer { Firstname="Customer2", Lastname="LastCustomer2", Email="xyz@airmail.com", Phone="1231231234",  Address="1234 First St.", Company="N/A", Id=2,
                    Notes = new List<CustomerNote>
                    {
                        new CustomerNote {Body = "Great Customer!", CreatedBy="User1", CreateDate= DateTime.Now }
                        ,new CustomerNote {Body = "Repeat Customer!", CreatedBy="User1", CreateDate= DateTime.Now.AddDays(-1) }
                    }
                }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            // only needed if using AsNoTracking
            //mockSet.Setup(x => x.AsNoTracking()).Returns(mockSet.Object);
            mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));
            var mockContext = new Mock<CustomersContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);
            var customerData = new CustomerData(mockContext.Object);

            //act
            customerData.Delete(1);

            //assert
            mockSet.Verify(m => m.Remove(It.IsAny<Customer>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void UpdateCustomer_FromListOfCustomer()
        {
            //arrange

            //var data = new List<Customer>
            //{
            //    new Customer { Firstname="Customer1", Lastname="LastCustomer1", Email="xyz@airmail.com", Phone="1231231234",  Address="1234 First St.", Company="N/A", Id=1,
            //        Notes = new List<CustomerNote>
            //        {
            //            new CustomerNote {Body = "Great Customer!", CreatedBy="User1", CreateDate= DateTime.Now }
            //            ,new CustomerNote {Body = "Repeat Customer!", CreatedBy="User1", CreateDate= DateTime.Now.AddDays(-1) }
            //        }
            //    },
            //    new Customer { Firstname="Customer2", Lastname="LastCustomer2", Email="xyz@airmail.com", Phone="1231231234",  Address="1234 First St.", Company="N/A", Id=2,
            //        Notes = new List<CustomerNote>
            //        {
            //            new CustomerNote {Body = "Great Customer!", CreatedBy="User1", CreateDate= DateTime.Now }
            //            ,new CustomerNote {Body = "Repeat Customer!", CreatedBy="User1", CreateDate= DateTime.Now.AddDays(-1) }
            //        }
            //    }

            //}.AsQueryable();

            //var customerToUpdate = new Customer
            //{
            //    Firstname = "Customer2Updated",
            //    Lastname = "LastCustomer2",
            //    Email = "xyz@airmail.com",
            //    Phone = "1231231234",
            //    Address = "1234 First St.",
            //    Company = "N/A",
            //    Id = 2,
            //    Notes = new List<CustomerNote>
            //        {
            //            new CustomerNote {Body = "Great Customer!", CreatedBy="User1", CreateDate= DateTime.Now }
            //            ,new CustomerNote {Body = "Repeat Customer!", CreatedBy="User1", CreateDate= DateTime.Now.AddDays(-1) }
            //        }
            //};


            //var mockSet = new Mock<DbSet<Customer>>();
            //mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(data.Provider);
            //mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            //mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            //mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            //// only needed if using AsNoTracking
            //mockSet.Setup(x => x.AsNoTracking()).Returns(mockSet.Object);
            //mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
            //    .Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));


            //mockSet.Setup(x => x.Attach(Customer)).Returns(mockSet.Object);

            //var mockContext = new Mock<CustomersContext>();
            //mockContext.Setup(m => m.Customers).Returns(mockSet.Object);
            //mockSet.Setup( x =>
            //    x.ChangeState(EntityState.Modified)).DoInstead(() => Mock.Arrange(() => mockedEntry.State).Returns(EntityState.Modified)));


            //var customerData = new CustomerData(mockContext.Object);
            //int expectedId = 2;

            ////act
            //customerData.Update(customerToUpdate);

            //var returnedData = customerData.GetById(expectedId);
            //var returnedId = returnedData.Id;

            ////assert
            //Assert.AreEqual(expectedId, returnedId);
        }
    }
}
