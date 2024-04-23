using L111;
using System;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace L111
{
    public partial class Form1 : Form
    {
        private string File1; 
        // file name of first initial data file
        private string File2; 
        // file name of second initial data file
        private string resultFile;
        // file name of result data file
        private string adress1; 
        //adress of first park
        private string adress2; 
        // adress of second park
        public Form1()
        {
            InitializeComponent();
            ToogleControls();
        }
        DriverContainer Park1 = new DriverContainer();
        // container object of park 1
        DriverContainer Park2 = new DriverContainer();
        // container object of park 2
        DriverContainer Result = new DriverContainer();
        // container object of rent buses

        /// <summary>
        /// method for open button in menu strip
        /// opens 2 initial data files 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            openFileDialog1.Title = "Open initial " +
                "data file for first park";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Text Files|*.txt" +
                "|Word Documents|*.doc";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File1 = openFileDialog1.FileName;
                ReadDrivers(File1, Park1, out adress1);
                DisplayToListBox(Park1, listDrivers1,
                    String.Format("Drivers from " +
                    "Park1 adress: {0}", adress1));
            }

            openFileDialog1.Title = "Open initial data " +
                "file for second park";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File2 = openFileDialog1.FileName;
                ReadDrivers(File2, Park2, out adress2);
                DisplayToListBox(Park2, listDrivers1,
                    String.Format("Drivers from " +
                    "Park2 adress: {0}", adress2));
                ToogleControls(true);
            }
        }

        /// <summary>
        /// method for turning buttons on or false
        /// </summary>
        /// <param name="tf">bool variable 
        /// for turning buttons on or off</param>
        private void ToogleControls(bool tf = false)
        {
            saveToolStripMenuItem.Enabled = tf;
            processToolStripMenuItem.Enabled = tf;
        }

        /// <summary>
        /// method for reading initial data files
        /// </summary>
        /// <param name="filename">name of initial data file</param>
        /// <param name="result">name of result container</param>
        /// <param name="adress">adress of park</param>
        private void ReadDrivers(string filename, 
            DriverContainer result, out string adress)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string line = reader.ReadLine();
                adress = line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    DateTime date = new DateTime(Convert.ToInt32(parts[3]), 1, 1);
                    Driver temp = new Driver(parts[0].Trim(),
                        Convert.ToInt32(parts[1].Trim()), 
                        parts[2].Trim(), date, 
                        Convert.ToInt32(parts[4].Trim()), 
                        Convert.ToInt32(parts[5]));
                    result.AddDriver(temp);
                }
            }
        }

        /// <summary>
        /// method for displaying container object to list box
        /// </summary>
        /// <param name="driverContainer">container object</param>
        /// <param name="LB">name of listbox</param>
        /// <param name="header">header</param>
        private void DisplayToListBox(DriverContainer driverContainer, 
            ListBox LB, string header)
        {
            LB.Items.Add("\n");
            LB.Items.Add(header);
            LB.Items.Add("----------------------------------------" +
                "-----------------------------------");
            LB.Items.Add(" Name and Surname    Experience " +
                "Registration Number   Date  Seats Kilometers");
            LB.Items.Add("----------------------------------" +
                "----------------------------------------");
            for (int i = 0; i < driverContainer.Count; i++)
            {
                LB.Items.Add(driverContainer.GetDriver(i).ToString());
            }
            LB.Items.Add("---------------------------------" +
                "------------------------------------------");

        }


        /// <summary>
        /// method for closing button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// method for button save in menu strip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Open initial data " +
                "file for first park";
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Text Files|*.txt|" +
                "Word Documents|*.doc";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                resultFile = saveFileDialog1.FileName;
                Print(resultFile);
            }
        }

        /// <summary>
        /// print method to txt file
        /// </summary>
        /// <param name="filename">name of result txt file</param>
        private void Print(string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.WriteLine(resultBox.Text);
            }
        }

        /// <summary>
        /// method for experience button in menu strip
        /// (finds the most experienced driver from both parks)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void experienceToolStripMenuItem_Click(object sender,
            EventArgs e)
        {
            resultBox.Items.Add("The most experienced driver is:");
            Driver mostExperienced = MostExperiencedDriver(Park1, Park2);
            resultBox.Items.Add(mostExperienced.ToString());
            if (Park1.Contains(mostExperienced))
            {
                resultBox.Items.Add("He is in Park1");
            }
            else
            {
                resultBox.Items.Add("He is in Park2");
            }

        }

        /// <summary>
        /// method to find the most experienced driver 
        /// from both parks
        /// </summary>
        /// <param name="dc1">container object of
        /// first park</param>
        /// <param name="dc2">container object of 
        /// second park</param>
        /// <returns>class object of the most 
        /// experienced driver</returns>
        private Driver MostExperiencedDriver(DriverContainer dc1, 
            DriverContainer dc2)
        {
            Driver result = new Driver();
            if (dc1.GetMostExperienced().experience >
                dc2.GetMostExperienced().experience)
            {
                result = dc1.GetMostExperienced();
            }
            else
            {
                result = dc2.GetMostExperienced();
            }
            return result;
        }

        /// <summary>
        /// method for writing only digits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        /// <summary>
        /// method for button rentBuses in menu strip 
        /// finds buses which are younger than K(entered from keyboard)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rentBusesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int k;
            if (textBox1.Text != "" 
                && Convert.ToInt32(textBox1.Text) != 0 
                && (k = Convert.ToInt32(textBox1.Text)) <= 
                Convert.ToInt32(DateTime.Now.Year))
            {
                    DateTime date = new DateTime(k, 1, 1);
                    Result = new DriverContainer();
                    Create(Park1.CreateListRentBuses(date), ref Result);
                    Create(Park2.CreateListRentBuses(date), ref Result);
                    if (Result[0] != null)
                    {
                        DisplayToListBox(Result, resultBox, String.Format("Buses that are " +
                            "not older than {0}", k));
                    }
                    else
                    {
                        resultBox.Items.Add("There are no available buses");
                    }
            }
            else
            {
                MessageBox.Show("Enter the correct value k");
            }

        }


        /// <summary>
        /// method for fillind result container object from another container object
        /// </summary>
        /// <param name="dc1">initial container object</param>
        /// <param name="dc2">result container object</param>
        private void Create(DriverContainer dc1, ref DriverContainer dc2)
        {
            for (int i = 0; i < dc1.Count; i++)
            {
                Driver temp = new Driver(dc1[i].nameS,
                        dc1[i].experience,
                        dc1[i].regNumber,
                        dc1[i].date,
                        dc1[i].seats,
                        dc1[i].km);
                dc2.AddDriver(temp);
            }
        }

        /// <summary>
        /// method for button sort in menu strip
        /// sorts result container object alphabeticly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Result[0] != null)
            {
                Result.BubbleSort();
                DisplayToListBox(Result, resultBox, "Sorted container");
            }
            else
            {
                MessageBox.Show("Your container is empty");
            }
        }

        /// <summary>
        /// method for button in menustip
        /// which fills new object container 
        /// with buses which have travelled less than k km
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void amountOfKmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                int k = Convert.ToInt32(textBox1.Text);
                FilterByKm(k, Result);
                if (Result[0] != null)
                {
                    DisplayToListBox(Result, resultBox, String.Format("Buses that has " +
                        "less km than {0}", k));
                }
                else
                {
                    resultBox.Items.Add("There are no " +
                        "available buses");
                }

            }
            else
            {
                MessageBox.Show("Enter the value k");
            }
        }

        /// <summary>
        /// methods for removing
        /// buses which travelled more than k km
        /// </summary>
        /// <param name="k">amount of km</param>
        private void FilterByKm(int k, DriverContainer A)
        {
            for (int i = 0; i < A.Count; i++)
            {
                if (A[i].km > k)
                {
                    A.Remove(A[i]);
                    i--;
                }
            }

        }
    }
}
