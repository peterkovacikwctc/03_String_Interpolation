using System;
using System.IO;

namespace SleepData
{
    class Program
    {
        static void Main(string[] args)
        {
            // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();
            var file = "data.txt";

            if (resp == "1")
            {
                // create data file

                 // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                int weeks = int.Parse(Console.ReadLine());

                 // determine start and end date
                DateTime today = DateTime.Now;
                // we want full weeks sunday - saturday
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                // subtract # of weeks from endDate to get startDate
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
                
                // random number generator
                Random rnd = new Random();

                // create file
                StreamWriter sw = new StreamWriter(file);
                // loop for the desired # of weeks
                while (dataDate < dataEndDate)
                {
                    // 7 days in a week
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++)
                    {
                        // generate random number of hours slept between 4-12 (inclusive)
                        hours[i] = rnd.Next(4, 13);
                    }
                    // M/d/yyyy,#|#|#|#|#|#|#
                    //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                    // add 1 week to date
                    dataDate = dataDate.AddDays(7);
                }
                sw.Close();
            }
            else if (resp == "2")
            {
                if (File.Exists(file))
                {
                    StreamReader sr = new StreamReader(file);
                    Console.WriteLine("");
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] arr = line.Split(',');

                        // date information (before splitting with comma)
                        string date = arr[0];
                        DateTime thisDate = Convert.ToDateTime(date);

                        // weekly sleep hours (after splitting with comma)
                        string weeklyHours = arr[1];
                        
                        // array of hours slept each day
                        string[] sleepEachDay = weeklyHours.Split('|');

                        // compute total hours of sleep
                        int totalHours = 0;
                        for (int i = 0; i < 7; i++) {
                            totalHours += int.Parse(sleepEachDay[i]);
                        }

                        // compute average number of hours
                        double averageHours = totalHours / 7.0;
                        
                        //display chart
                        Console.WriteLine("Week of " + thisDate.ToString("MMM, dd, yyyy"));
                        Console.WriteLine($"{"Su",3}{"Mo",3}{"Tu",3}{"We",3}{"Th",3}{"Fr",3}{"Sa",3}{"Tot",4}{"Avg",4}");
                        Console.WriteLine($"{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}{"---",4}{"---",4}");
                        Console.WriteLine($"{sleepEachDay[0],3}{sleepEachDay[1],3}{sleepEachDay[2],3}{sleepEachDay[3],3}{sleepEachDay[4],3}{sleepEachDay[5],3}{sleepEachDay[6],3}{totalHours,4}{averageHours,4:n1}");
                        Console.WriteLine("");
                    }
                    sr.Close();
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("The file '" + file + "' does not exist.");
                }
            }
        }
    }
}
