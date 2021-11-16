using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace CustomerDistanceChecker
{
    class Program
    {
        /// <summary>
        /// The file location
        /// </summary>
        public const string FileLocation = "C:\\Users\\coade\\OneDrive\\Documents\\CustomerDistanceChecker\\Customers.txt";
        
        /// <summary>
        /// The list to hold customers in Range
        /// </summary>
        public static List<Customer> MatchingCustomers = new List<Customer>();

        /// <summary>
        /// The entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            var customerHelper = new CustomerHelper();

            var fileData = customerHelper.ReadCustomerDataFromFile(FileLocation);

            // get list of customer objects
            var customers = customerHelper.DeserialiseFileDataToCustomerObjects(fileData);

            foreach (var customer in customers)
            {
                var customerIsWithinDistance = customerHelper.CheckCustomerDistanceToOffice(customer);

                if (customerIsWithinDistance)
                {
                    MatchingCustomers.Add(customer);
                }
            }

            // Order List of Matched customers
            MatchingCustomers = MatchingCustomers.OrderBy(c => int.Parse(c.User_Id)).ToList();

            customerHelper.OutputCustomerDetails(MatchingCustomers);

            Console.ReadLine();
        }      
        
    }
}

