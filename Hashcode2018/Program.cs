using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashcode2018 {
    class Program {
        static void Main(string[] args) {


            var rows = 0;
            var columns = 0;
            var vehicles = 0;
            var numRides = 0;
            var onTimeBonus = 0;
            long steps = 0;
            List<Ride> rides = new List<Ride>();
            List<Vehicle> vehiclesObjs = new List<Vehicle>();

            using (TextReader tr = new StreamReader("in.in")) {
                var line1 = tr.ReadLine();
                var values = line1.Split(' ');
                rows = int.Parse(values[0]);
                columns = int.Parse(values[1]);
                vehicles = int.Parse(values[2]);
                numRides = int.Parse(values[3]);
                onTimeBonus = int.Parse(values[4]);
                steps = long.Parse(values[5]);
                var rideCounter = 0;
                for (int i = 0; i < numRides; i++) {
                    var line = tr.ReadLine();
                    var vals = line.Split(' ');
                    rides.Add(new Ride() {
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

            for (int i = 0; i < vehicles; i++) {
                vehiclesObjs.Add(new Vehicle());
            }

            var vehicleindex = 0;
            // Do some magical algorithms

            foreach (var item in rides) {

                //var test = vehiclesObjs.Where(x => x.occTill+x.TimeToGetThere(item.startRow, item.startColumn) < item.startTick);                
                //if (test.Count() <= 0) {
                //    continue;
                //}
                //test.OrderBy(x => x.TimeToGetThere(item.startRow, item.startColumn));
                //vehicleindex = vehiclesObjs.IndexOf(test.First());
                var viableVehicles = new List<Vehicle>();
                foreach (var car in vehiclesObjs) {
                    if (car.occTill + car.TimeToGetThere(item.startRow, item.startColumn) < item.startTick) {
                        viableVehicles.Add(car);
                    }
                }
                viableVehicles.OrderBy(x => x.TimeToGetThere(item.startRow, item.startColumn)).ToList();
                if (viableVehicles.Count == 0) {
                    continue;
                }
                vehicleindex = vehiclesObjs.IndexOf(viableVehicles[0]);
                var vehc = vehiclesObjs[vehicleindex];
                vehiclesObjs[vehicleindex].scheduledRides.Add(item);
                var timeTaken = vehiclesObjs[vehicleindex].TimeToGetThere(item.startRow, item.startColumn, true, item.finx, item.finy);
                vehc.posX = item.finx;
                vehc.posY = item.finy;
                vehc.occTill = timeTaken;
                vehicleindex++;
                if (vehicleindex > vehicles - 1) {
                    vehicleindex = 0;
                }
            }



            using (TextWriter tw = new StreamWriter("out.txt")) {
                foreach (var item in vehiclesObjs) {
                    tw.WriteLine(item.ToString());
                }
            }

        }

        public class Ride {
            public int id { get; set; }
            public int startRow { get; set; }
            public int startColumn { get; set; }

            public int finx { get; set; }
            public int finy { get; set; }

            public int startTick { get; set; }
            public int endTick { get; set; }

            public Ride() {

            }
        }

        public class Vehicle {

            public List<Ride> scheduledRides { get; set; }
            public int TimeToGetThere(int x, int y, bool things = false, int x2 = 0 , int y2 = 0) {
                var time = (x - posX) + (y - posY);
                if (things) {
                    time += (x2 - x) + (y2 - y);
                }
                return time;
            }
            public int posX { get; set; }
            public int posY { get; set; }
            public int occTill { get; set; }
            public Vehicle() {
                scheduledRides = new List<Ride>();
                posX = 0;
                posY = 0;
                occTill = 0;
            }
            public override string ToString() {
                var s = scheduledRides.Count().ToString() + " ";
                foreach (var item in scheduledRides) {
                    s += item.id + " ";
                }
                return s;
            }

        }
    }
}
