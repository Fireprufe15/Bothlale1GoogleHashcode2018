﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashcode2018
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = { "a_example", "b_should_be_easy", "c_no_hurry", "d_metropolis", "e_high_bonus" };

            foreach (string fileName in files)
            {
                Console.WriteLine("Busy with: " + fileName);

                var rows = 0;
                var columns = 0;
                var vehicles = 0;
                var numRides = 0;
                var onTimeBonus = 0;
                long steps = 0;
                List<Ride> rides = new List<Ride>();
                List<Vehicle> vehiclesObjs = new List<Vehicle>();

                using (TextReader tr = new StreamReader("In Files/" + fileName + ".in"))
                {
                    var line1 = tr.ReadLine();
                    var values = line1.Split(' ');
                    rows = int.Parse(values[0]);
                    columns = int.Parse(values[1]);
                    vehicles = int.Parse(values[2]);
                    numRides = int.Parse(values[3]);
                    onTimeBonus = int.Parse(values[4]);
                    steps = long.Parse(values[5]);
                    var rideCounter = 0;
                    for (int i = 0; i < numRides; i++)
                    {
                        var line = tr.ReadLine();
                        var vals = line.Split(' ');
                        rides.Add(new Ride()
                        {
                            id = rideCounter,
                            startRow = int.Parse(vals[0]),
                            startColumn = int.Parse(vals[1]),
                            finx = int.Parse(vals[2]),
                            finy = int.Parse(vals[3]),
                            startTick = int.Parse(vals[4]),
                            endTick = int.Parse(vals[5])
                        });
                        rideCounter++;
                    }
                }

                for (int i = 0; i < vehicles; i++)
                {
                    vehiclesObjs.Add(new Vehicle());
                }

                rides = Sort(rides);
                
                var vehicleindex = 0;
                // Do some magical algorithms

                for (int i = 0; i < rides.Count; i++)
                {
                    Ride item = rides[i];
                    rides.RemoveAt(i);
                    vehiclesObjs[vehicleindex].scheduledRides.Add(item);
                    for (int j = 0; j < rides.Count; j++)
                    {
                        if (distanceBetweenRides(item, rides[j]) == MinDistanceBetweenRides(rides))
                        {
                            vehiclesObjs[vehicleindex].scheduledRides.Add(item);
                            rides.RemoveAt(j);
                            break;
                        }
                    }

                    vehicleindex++;
                    if (vehicleindex > vehicles - 1)
                    {
                        vehicleindex = 0;
                    }
                    i--;
                }

                using (TextWriter tw = new StreamWriter(fileName + ".out.txt"))
                {
                    foreach (var item in vehiclesObjs)
                    {
                        tw.WriteLine(item.ToString());
                    }
                }
            }



        }

        public static int MinDistanceBetweenRides(List<Ride> rides)
        {
            int iMinDistance = 0;

            for (int i = 0; i < rides.Count; i++)
            {
                for (int j = 0; j < rides.Count; j++)
                {
                    if (i!=j)
                    {
                        if (i == 0 && j == 1)
                        {
                            iMinDistance = distanceBetweenRides(rides[0], rides[1]);
                        }

                        if (distanceBetweenRides(rides[i], rides[j]) < iMinDistance)
                        {
                            iMinDistance = distanceBetweenRides(rides[i], rides[j]);
                        }
                    }
                }
            }

            return iMinDistance;
        }

        public static int distanceBetweenRides(Ride ride1, Ride ride2)
        {
            return (ride1.finx + ride2.startRow) + (ride1.finy + ride2.startColumn);
        }

        public static List<Ride> Sort(List<Ride> rides)
        {
            rides = rides.OrderBy(x => x.endTick).ToList();
            rides = rides.OrderBy(x => distanceTravelled(x)).ToList();
            rides = rides.OrderBy(x => x.startTick).ToList();
            
            return rides;
        }

        public static int distanceTravelled(Ride ride)
        {
            int startX = ride.startRow;
            int endX = ride.finx;
            int startY = ride.startColumn; ;
            int endY = ride.finy;
            return Math.Abs(startX - endX) + Math.Abs(startY - endY);
        }

        public class Ride
        {
            public int id { get; set; }
            public int startRow { get; set; }
            public int startColumn { get; set; }

            public int finx { get; set; }
            public int finy { get; set; }

            public int startTick { get; set; }
            public int endTick { get; set; }

            public Ride()
            {

            }
        }

        public class Vehicle
        {

            public List<Ride> scheduledRides { get; set; }
            public int posX { get; set; }
            public int posY { get; set; }
            public Vehicle()
            {
                scheduledRides = new List<Ride>();
                posX = 0;
                posY = 0;
            }
            public override string ToString()
            {
                var s = scheduledRides.Count.ToString() + " ";
                foreach (var item in scheduledRides)
                {
                    s += item.id + " ";
                }
                return s;
            }

        }
    }
}