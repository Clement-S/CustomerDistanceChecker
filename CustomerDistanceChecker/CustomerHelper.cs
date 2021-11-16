using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CustomerDistanceChecker
{
    public class CustomerHelper
    {
        /// <summary>
        /// The earth radius in km
        /// </summary>
        public const double Earth_Radius_Km = 6371;

        /// <summary>
        /// Converts to radians.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns></returns>
        public static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        /// <summary>
        /// Calculates the Customer's distance to Dublin office.
        /// </summary>
        /// <param name="customerLatitude">The customer latitude.</param>
        /// <param name="customerLongitude">The customer longitude.</param>
        /// <returns>The distance to the Dublin office</returns>
        public double CalculateCustomerDistanceToDublinOffice(Customer customer)
        {
            var customerLatitudeInRadians = ConvertToRadians(customer.Latitude);
            var customerLongitudeInRadians = ConvertToRadians(customer.Longitude);

            var sinOfOfficeLatitude = Math.Sin(DublinOfficeLocation.Latitude_In_Radians);
            var cosOfOfficeLatitude = Math.Cos(DublinOfficeLocation.Latitude_In_Radians);

            var sinOfcustomerLatitude = Math.Sin(customerLatitudeInRadians);
            var cosOfcustomerLatitude = Math.Cos(customerLatitudeInRadians);

            var absoluteDifference = Math.Abs((customerLongitudeInRadians - DublinOfficeLocation.Longitude_In_Radians));

            var centralAngle = Math.Acos((sinOfOfficeLatitude * sinOfcustomerLatitude) + (cosOfOfficeLatitude * cosOfcustomerLatitude * Math.Cos(absoluteDifference)));

            var distanceToOffice = CustomerHelper.Earth_Radius_Km * centralAngle;

            return distanceToOffice;
        }

        /// <summary>
        /// Checks the Customer's distance to office.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns>A flag signifying if the customer is within range.</returns>
        public bool CheckCustomerDistanceToOffice(Customer customer)
        {
            var customerDistanceToDublinOffice = CalculateCustomerDistanceToDublinOffice(customer);

            if (customerDistanceToDublinOffice < 0)
            {
                throw new ArgumentOutOfRangeException($"Incorrect Details for Customer: {customer.Name} with UserId {customer.User_Id} Entered");
            }

            if (customerDistanceToDublinOffice <= DublinOfficeLocation.AcceptedDistanceToOffice_In_Km)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deserialises the file data.
        /// </summary>
        /// <param name="jsonFileData">The file data in JSON string format.</param>
        /// <returns>A list of customer objects after deserialising file data.</returns>
        public IEnumerable<Customer> DeserialiseFileDataToCustomerObjects(string[] jsonFileData)
        {           
            if (jsonFileData == null)
            {
                throw new ArgumentNullException("Empty data passed in");
            }

            List<Customer> customers = new List<Customer>();

            for (int i = 0; i < jsonFileData.Length; i++)
            {
                var customer = JsonConvert.DeserializeObject<Customer>(jsonFileData[i]);

                customers.Add(customer);
            }

            return customers;
        }

        /// <summary>
        /// Outputs the customer details from a list of customers.
        /// </summary>
        /// <param name="customers">The customers.</param>
        public void OutputCustomerDetails(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine("User Id: {0}     Customer Name:  {1}", customer.User_Id, customer.Name);
            }
        }

        /// <summary>
        /// Reads customer data from file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns> String array holding each line of json from file.</returns>
        public string[] ReadCustomerDataFromFile(string filePath)
        {
            using (var streamReader = File.OpenText(filePath))
            {
                var lines = streamReader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                return lines;
            }
        }
    }
}
