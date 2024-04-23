using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L111
{
    class Driver
    {
        public string nameS { get; set; }
        //variable to store name and surname of driver
        public int experience { get; set; }
        //variable to store experience of driver
        public string regNumber { get; set; }
        //variable to store registration number
        public DateTime date { get; set; }
        //variable to store date of manufacture
        public int seats { get; set; }
        //variable to store amount of seats in the bus
        public int km { get; set; }
        //variable to store amount of km

        /// <summary>
        /// constructor without parameters
        /// </summary>
        public Driver()
        {

        }

        /// <summary>
        /// constructor with parameters
        /// </summary>
        /// <param name="nameS">variable to store 
        /// name and surname of driver</param>
        /// <param name="experience">variable to store 
        /// experience of driver</param>
        /// <param name="regNumber">variable to store 
        /// registration number</param>
        /// <param name="date">variable to store 
        /// date of manufacture</param>
        /// <param name="seats">variable to store 
        /// amount of seats in the bus</param>
        /// <param name="km">variable to store 
        /// amount of km</param>
        public Driver(string nameS, 
            int experience,
            string regNumber, 
            DateTime date, 
            int seats, 
            int km)
        {
            this.nameS = nameS;
            this.experience = experience;
            this.regNumber = regNumber;
            this.date = date;
            this.seats = seats;
            this.km = km;
        }

        /// <summary>
        /// override method ToString
        /// </summary>
        /// <returns>line with all information 
        /// about driver and bus</returns>
        public override string ToString()
        {
            return String.Format(" {0,-20} {1,-10} {2,-20} {3,-5} {4,-5} {5,-10}",
                nameS, 
                experience, 
                regNumber, 
                date.ToString("yyyy"), 
                seats,
                km);
        }

        /// <summary>
        /// overload operator to compare drivers alphabeticly
        /// </summary>
        /// <param name="dv1">first class object</param>
        /// <param name="dv2">second class object</param>
        /// <returns>true if first class object 
        /// is alphabeticly first(false otherwise)</returns>
        public static bool operator >(Driver dv1, Driver dv2)
        {
            if (String.Compare(dv1.nameS, dv2.nameS) == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// overload operator to compare drivers alphabeticly
        /// </summary>
        /// <param name="dv1">first class object</param>
        /// <param name="dv2">second class object</param>
        /// <returns>false if first class object 
        /// is alphabeticly first(true otherwise)</returns>
        public static bool operator <(Driver dv1, Driver dv2)
        {
            if (String.Compare(dv1.nameS, dv2.nameS) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
