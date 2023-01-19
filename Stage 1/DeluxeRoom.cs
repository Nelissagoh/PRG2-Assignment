using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_1
{
    class DeluxeRoom : Room
    {
        public bool AdditionalBed { get; set; }
        public DeluxeRoom() : base() { }
        public DeluxeRoom(int rn, string bc, double dr, bool ia, bool ab)
            : base(rn, bc, dr, ia)
        {
            AdditionalBed = ab;
        }
        public override double CalculateCharges()
        {
            return 1;
        }
        public override string ToString()
        {
            return base.ToString() + $"\tAdditionalBed: {AdditionalBed}";
        }
    }
}