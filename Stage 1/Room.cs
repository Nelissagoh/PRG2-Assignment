using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_1
{
    abstract class Room
    {
        public int RoomNumber { get; set; }
        public string BedConfiguration { get; set; }
        public double DailyRate { get; set; }
        public bool IsAvail { get; set; }
        public Room() { }
        public Room(int rn, string bc, double dr, bool ia)
        {
            RoomNumber = rn;
            BedConfiguration = bc;
            DailyRate = dr;
            IsAvail = ia;
        }
        public double CalculateCharges() 
        {
            double rate;
            if (IsAvail == true)
            {
                rate = DailyRate;
            }
            else
            {
                rate = 0.0;
            }
            return rate;
        }

        public override string ToString()
        {
            return "BedConfiguration: " + BedConfiguration;
        }
    }
}
