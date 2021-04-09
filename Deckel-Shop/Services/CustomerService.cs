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

        public int GetCustomerId(string email)
        {
            var customer = deckelShopContext.Customers.FirstOrDefault(c => c.Email.Contains(email));

            if (customer == null)
            {
                return 0;
            }

            return customer.Id;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return deckelShopContext.Customers.AsEnumerable();
        }
        public Customer GetCustomer(int id)
        {
            return deckelShopContext.Customers.FirstOrDefault(c => c.Id == id);
        }

        public void  AddCustomer(Customer customer)
        {
            deckelShopContext.Customers.Add(customer);
            deckelShopContext.SaveChanges(); 
        }

        public void RemoveCustomer(int id)
        {
            var customer = deckelShopContext.Customers.SingleOrDefault(c => c.Id == id);
            deckelShopContext.Customers.Remove(customer);
            deckelShopContext.SaveChanges();
        }
        
        public async Task <int> EditCustomer(Customer customer)
        {
            
            deckelShopContext.Update(customer);
           
            return await deckelShopContext.SaveChangesAsync();
        }

        
        
    }



}
