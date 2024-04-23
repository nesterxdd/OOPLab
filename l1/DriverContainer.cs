using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L111
{
    class DriverContainer
    {
        public int Count { get; set; }
        //variable for storing amount of drivers

        Driver[] drivers;
        //array of driver class

        int Capacity;
        //capacity

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="capacity">specefied capacity
        /// (if not entered capacity is 16)</param>
        public DriverContainer(int capacity = 16)
        {
            drivers = new Driver[16];
            Count = 0;
        }

        /// <summary>
        /// method for adding a new driver 
        /// to container object
        /// </summary>
        /// <param name="driver">class object which will be added</param>
        public void AddDriver(Driver driver)
        {
            if(Capacity == Count + 1)
            {
                EnsureCapacity(Count * 2);
            }
            
            drivers[Count++] = driver;
        }

        /// <summary>
        /// indexer
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>class object of driver with index i</returns>
        public Driver this[int i]
        {
            get
            {
                return drivers[i];
            }
            set
            {
                drivers[i] = value;
            }
        }

        /// <summary>
        /// method for ensuring capacity
        /// </summary>
        /// <param name="minimumCapacity">required capacity</param>
        private void EnsureCapacity(int minimumCapacity)
        {
            if (minimumCapacity > this.Capacity)
            {
                Driver[] temp = new Driver[minimumCapacity];
                for (int i = 0; i < this.Count; i++)
                {
                    temp[i] = this.drivers[i];
                }
                this.Capacity = minimumCapacity;
                this.drivers = temp;
            }
        }

        /// <summary>
        /// method for getting driver with index i 
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>driver with index i</returns>
        public Driver GetDriver(int i)
        {
            return drivers[i];
        }

        /// <summary>
        /// method for getting the most experienced driver from container
        /// </summary>
        /// <returns>the most experienced driver</returns>
        public Driver GetMostExperienced()
        {
            int max = drivers[0].experience;
            Driver result = new Driver(drivers[0].nameS, 
                drivers[0].experience,
                drivers[0].regNumber,
                drivers[0].date,
                drivers[0].seats,
                drivers[0].km);
            for (int i = 1; i < Count; i++)
            {
                if (drivers[i].experience > max)
                {
                    max = drivers[i].experience;
                    result = new Driver(drivers[i].nameS,
                        drivers[i].experience, 
                        drivers[i].regNumber,
                        drivers[i].date, 
                        drivers[i].seats,
                        drivers[i].km);
                }
            }
            return result;
        }


        /// <summary>
        /// method to check does container 
        /// object stores driver
        /// </summary>
        /// <param name="driver">driver which will be checked</param>
        /// <returns>true if this driver in container(false otherwise)</returns>
        public bool Contains(Driver driver)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.drivers[i].Equals(driver))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// RemoveAt method which removes driver 
        /// with specefiet index
        /// </summary>
        /// <param name="location">index</param>
        public void RemoveAt(int location)
        {
            for (int i = location; i < Count; i++)
            {
                drivers[i] = drivers[i + 1];
            }
            Count--;
        }

        /// <summary>
        /// remove method(removes specefied driver)
        /// </summary>
        /// <param name="dv">specefied driver</param>
        public void Remove(Driver dv)
        {
            for (int i = 0; i < Count; i++)
            {
                if (drivers[i].Equals(dv))
                {
                    drivers[i] = null;

                    if (i + 1 != Count)
                    {


                        for (int j = i; j < Count; j++)
                        {
                            drivers[j] = drivers[j + 1];
                        }


                        Count--;
                        break;
                    }
                    Count--;
                }
            }
        }

        /// <summary>
        /// method for creating new container object 
        /// with buses that are younger than k
        /// </summary>
        /// <param name="k">year</param>
        /// <returns>container object 
        /// with suitable buses</returns>
        public DriverContainer CreateListRentBuses(DateTime date)
        {         
            DriverContainer result = new DriverContainer();
            for (int i = 0; i < Count; i++)
            {
                if (drivers[i].date >= date)
                {
                    Driver temp = new Driver(drivers[i].nameS,
                        drivers[i].experience,
                        drivers[i].regNumber,
                        drivers[i].date,
                        drivers[i].seats,
                        drivers[i].km);
                    result.AddDriver(temp);
                }
            }
            return result;
        }

        /// <summary>
        /// bubble sort method
        /// </summary>
        public void BubbleSort()
        {
            int i = 0;
            bool swap = true;
            while (swap)
            {
                swap = false;
                for (int j = Count - 1; j > i; j--)
                    if (drivers[j] > drivers[j - 1])
                    {
                        swap = true;
                        Driver dv = drivers[j];
                        drivers[j] = drivers[j - 1];
                        drivers[j - 1] = dv;
                    }
                i++;
            }
        }



    }
}
