﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
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


    public class TestContext : ICustomerData
    {
        public TestContext()
        {
            this.Customers = new TestDbSet<Customer>();
            this.CustomerNotes = new TestDbSet<CustomerNote>();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerNote> CustomerNotes { get; set; }
        public int SaveChangesCount { get; private set; }
        public int SaveChanges()
        {
            this.SaveChangesCount++;
            return 1;
        }

        public List<Customer> GetAll()
        {
            return Customers.ToList();
        }

        public Customer GetById(int id)
        {
            var customer = Customers.SingleOrDefault(x => x.Id == id);
            return customer;
        }

        public void Insert(Customer entity)
        {
            Customers.Add(entity);
        }

        public void Update(Customer entity)
        {
            var customer = Customers.SingleOrDefault(x => x.Id == entity.Id);
            Customers.Remove(customer);
            Customers.Add(entity);
        }

        public void Delete(int id)
        {
            var customer = Customers.SingleOrDefault(x => x.Id == id);
            Customers.Remove(customer);
        }

        public void AddCustomerNote(CustomerNote note)
        {

            CustomerNotes.Add(note);
        }

        public CustomerNote GetCustomerNote(int id)
        {
            var note = CustomerNotes.SingleOrDefault(x => x.Id == id);
            return note;
        }

        public void UpdateCustomerNote(CustomerNote note)
        {
            var customerNote = CustomerNotes.SingleOrDefault(x => x.Id == note.Id);
            CustomerNotes.Remove(customerNote);
            CustomerNotes.Add(note);
        }

        public void DeleteCustomerNote(int customerNoteId)
        {
            var note = CustomerNotes.SingleOrDefault(x => x.Id == customerNoteId);
            CustomerNotes.Remove(note);
        }
    }

    public class TestDbSet<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity>
    where TEntity : class
    {
        ObservableCollection<TEntity> _data;
        IQueryable _query;

        public TestDbSet()
        {
            _data = new ObservableCollection<TEntity>();
            _query = _data.AsQueryable();
        }

        public override TEntity Add(TEntity item)
        {
            _data.Add(item);
            return item;
        }

        public override TEntity Remove(TEntity item)
        {
            _data.Remove(item);
            return item;
        }

        public override TEntity Attach(TEntity item)
        {
            _data.Add(item);
            return item;
        }

        public override TEntity Create()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public override ObservableCollection<TEntity> Local
        {
            get { return _data; }
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestDbAsyncQueryProvider<TEntity>(_query.Provider); }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
        {
            private readonly IQueryProvider _inner;

            internal TestDbAsyncQueryProvider(IQueryProvider inner)
            {
                _inner = inner;
            }

            public IQueryable CreateQuery(Expression expression)
            {
                return new TestDbAsyncEnumerable<TEntity>(expression);
            }

            public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
            {
                return new TestDbAsyncEnumerable<TElement>(expression);
            }

            public object Execute(Expression expression)
            {
                return _inner.Execute(expression);
            }

            public TResult Execute<TResult>(Expression expression)
            {
                return _inner.Execute<TResult>(expression);
            }

            public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
            {
                return Task.FromResult(Execute(expression));
            }

            public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
            {
                return Task.FromResult(Execute<TResult>(expression));
            }
        }

        internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
        {
            public TestDbAsyncEnumerable(IEnumerable<T> enumerable)
                : base(enumerable)
            { }

            public TestDbAsyncEnumerable(Expression expression)
                : base(expression)
            { }

            public IDbAsyncEnumerator<T> GetAsyncEnumerator()
            {
                return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
            }

            IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
            {
                return GetAsyncEnumerator();
            }

            IQueryProvider IQueryable.Provider
            {
                get { return new TestDbAsyncQueryProvider<T>(this); }
            }
        }

        internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;

            public TestDbAsyncEnumerator(IEnumerator<T> inner)
            {
                _inner = inner;
            }

            public void Dispose()
            {
                _inner.Dispose();
            }

            public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(_inner.MoveNext());
            }

            public T Current
            {
                get { return _inner.Current; }
            }

            object IDbAsyncEnumerator.Current
            {
                get { return Current; }
            }
        }


    }


}
