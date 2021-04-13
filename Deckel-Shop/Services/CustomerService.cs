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
        public void RemoveCustomer(int id)
        {
            const string deleted = "Deleted";
            OrderService os = new OrderService();
            var customer = deckelShopContext.Customers.SingleOrDefault(c => c.Id == id);
            var customerList = os.GetAllOrdersBySelectedCustomer(id);
            if (customerList != null)
            {
                customer.FirstName = deleted;
                customer.LastName = deleted;
                customer.Phone = deleted;
                customer.City = deleted;
                customer.Email = deleted;
                customer.Street = deleted;
                customer.ZipCode = deleted;
            }
            else
            {
                deckelShopContext.Customers.Remove(customer);
            }

            deckelShopContext.SaveChanges();
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

        public async Task <int> EditCustomer(Customer customer)
        {
            
            deckelShopContext.Update(customer);
           
            return await deckelShopContext.SaveChangesAsync();
        }

        
        
    }



}
