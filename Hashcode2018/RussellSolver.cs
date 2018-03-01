using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashcode2018
{
    public class RussellSolver
    {
        public RussellSolver()
        {

        }

        public void Solve()
        {

        }
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
            var s = scheduledRides.Count().ToString() + " ";
            foreach (var item in scheduledRides)
            {
                s += item.id + " ";
            }
            return s;
        }

    }
}
