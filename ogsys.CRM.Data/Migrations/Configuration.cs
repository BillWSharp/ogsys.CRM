namespace ogsys.CRM.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ogsys.CRM.Data.CustomersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ogsys.CRM.Data.CustomersContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Customers.AddOrUpdate(c => c.Firstname,
                new Models.Customer
                {
                    Firstname = "tester1",
                    Lastname = "test",
                    Company = "We Test",
                    Email = "tester1@wetest.com",
                    Address = "1234 New Town",
                    Phone = "1234561235"
                },
                new Models.Customer
                {
                    Firstname = "tester2",
                    Lastname = "test",
                    Company = "We Test",
                    Email = "tester1@wetest.com",
                    Address = "1234 New Town",
                    Phone = "1234561235"

                },
                new Models.Customer
                {
                    Firstname = "tester3",
                    Lastname = "test",
                    Company = "We Test",
                    Email = "tester1@wetest.com",
                    Address = "1234 New Town",
                    Phone = "1234561235",
                    Notes =
                        new List<Models.CustomerNote>
                        {
                            new Models.CustomerNote { Body="Always follows up!",  CreateDate=DateTime.Today, CreatedBy="User01" }
                        }

                });
        }
    }
}