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

        public List<Customer> GetAllCustomers()
        {
            return deckelShopContext.Customers.ToList();
        }

        public List<Customer> GetGustomerByFirstName(string firstName)
        {
            return deckelShopContext.Customers.Where(x => x.FirstName.Contains(firstName)).ToList();
        }      

        public List<Customer> GetCustomerByLastName(string lastName)
        {
            return deckelShopContext.Customers.Where(x => x.LastName.Contains(lastName)).ToList();
        }
        
        public List< Customer> GetCustomerById(int id)
        {
            return deckelShopContext.Customers.Where(x => x.Id == id).ToList();
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
