using System;
using CustomerDistanceChecker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CustomerDistanceCheckerTests
{
    [TestClass]
    public class CustomerDistanceCheckerTests
    {
        /// <summary>
        /// The CustomerHelper class
        /// </summary>
        CustomerHelper customerHelper = new CustomerHelper();

        /// <summary>
        /// The Test Data file location
        /// </summary>
        public const string TestDataFileLocation = "C:\\Users\\Seyi07\\Documents\\CustomerDistanceChecker\\TestData.txt";

        /// <summary>
        /// The Invalid Test Data file location
        /// </summary>
        public const string InvalidTestDataFileLocation = "C:\\Users\\Seyi07\\Documents\\CustomerDistanceChecker\\SampleInvalidData.txt";

        [TestMethod]
        public void GivenADistanceBeyond100Km_CustomerShouldntBeInvited()
        {
            // Arrange
            Customer customer_Stacy = new Customer();
            customer_Stacy.Latitude = 53.521111;
            customer_Stacy.Longitude = -17.831111;

            // Act
            var isCloseBy = customerHelper.CheckCustomerDistanceToOffice(customer_Stacy);

            // Assert
            Assert.IsFalse(isCloseBy);
        }

        [TestMethod]
        public void GivenADistanceBelow100Km_CustomerShouldBeInvited()
        {
            // Arrange
            Customer customer_Clement = new Customer();
            customer_Clement.Latitude = 52.983675;
            customer_Clement.Longitude = -6.043701;

            // Act
            var isCloseBy = customerHelper.CheckCustomerDistanceToOffice(customer_Clement);

            // Assert
            Assert.IsTrue(isCloseBy);
        }

        [TestMethod]
        public void GivenAValidCustomerInstance_ThenDistanceToDublinOffice_ShouldBeCalculated()
        {
            // Arrange
            var inputData = customerHelper.ReadCustomerDataFromFile(TestDataFileLocation);

            var customers = customerHelper.DeserialiseFileDataToCustomerObjects(inputData);

            // Act
            var distance = customerHelper.CalculateCustomerDistanceToDublinOffice(customers.First());

            Assert.IsTrue(distance > 0);            
        }

        [TestMethod]
        public void GivenValidData_ThenDataShouldBeDeserialised_ToCustomerObjects()
        {
            // Arrange
            var inputData = customerHelper.ReadCustomerDataFromFile(TestDataFileLocation);

            // Act
            var customers = (customerHelper.DeserialiseFileDataToCustomerObjects(inputData)).ToList();

            var expectedNumberOfCustomerInstances = 2;

            // Assert
            Assert.IsNotNull(customers);
            Assert.AreEqual(expectedNumberOfCustomerInstances, customers.Count);
            Assert.AreEqual(typeof(List<Customer>), customers.GetType());
        }

        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void GivenInvalidInputData_ThenShouldThrowException()
        {
            // Arrange
            var inputData = customerHelper.ReadCustomerDataFromFile(InvalidTestDataFileLocation);

            // Act
            var customers = (customerHelper.DeserialiseFileDataToCustomerObjects(inputData)).ToList();             
        }
    }
}

