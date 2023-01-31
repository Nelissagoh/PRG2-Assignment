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
        public DeluxeRoom(){ }
        public DeluxeRoom(int rn, string bc, double dr, bool ia)
            
        {
            RoomNumber = rn;
            BedConfiguration = bc;
            DailyRate = dr;
            IsAvail = ia;
        }
    
        public double CalculateCharges() 
        {
            return 0;
        }
        public override string ToString()
        {
            return base.ToString() + $"\tAdditionalBed: {AdditionalBed}";
        }
    }
}