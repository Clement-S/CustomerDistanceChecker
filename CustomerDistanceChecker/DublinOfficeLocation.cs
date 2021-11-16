using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistanceChecker
{
    /// <summary>
    /// The Details of Dublin Office
    /// </summary>
    class DublinOfficeLocation
    {
        /// <summary>
        /// The accepted distance to office in km
        /// </summary>
        public const double AcceptedDistanceToOffice_In_Km = 100;

        /// <summary>
        /// The latitude of the office in Degrees
        /// </summary>
        public const double Latitude_In_Degrees = 53.339428;

        /// <summary>
        /// The longitude of the office in Degrees
        /// </summary>
        public const double Longitude_In_Degrees = -6.257664;

        /// <summary>
        /// The latitude of the office in radians
        /// </summary>
        public static double Latitude_In_Radians = CustomerHelper.ConvertToRadians(Latitude_In_Degrees);

        /// <summary>
        /// The longitude of the office in radians
        /// </summary>
        public static double Longitude_In_Radians = CustomerHelper.ConvertToRadians(Longitude_In_Degrees);
    }
}
