using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deckel_Shop.Services
{


    public class CustomerService
    {
        private readonly DeckelShopContext deckelShopContext;
        public CustomerService()
        {
            deckelShopContext = new DeckelShopContext();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return deckelShopContext.Customers.AsEnumerable();
        }
        public IEnumerable< Customer> GetCustomer(int id)
        {
            return deckelShopContext.Customers.Where(c => c.Id == id).AsEnumerable();
        }

        public void  AddCustomer(Customer customer)
        {
            deckelShopContext.Customers.Add(customer);
            deckelShopContext.SaveChanges(); 
        }

        public void RemoveCustomer(Customer customer)
        {
            deckelShopContext.Customers.Remove(customer);
            deckelShopContext.SaveChanges();
        }
        
        public void EditCustomer(Customer customer)
        {
            var currentCustomer = deckelShopContext.Customers.FirstOrDefault(x => x.Id == customer.Id);
            if (currentCustomer != null)
            {
                currentCustomer = customer;
            }
            deckelShopContext.SaveChanges(); 
        }

        
        
    }



}
