using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_1
{
    class Membership:Guest
    {
        public string Status { get; set; }
        public int Points { get; set; }
        public Membership() { }
        public Membership(string s, int p)
        {
            Status = s;
            Points = p;
        }
        public void EarnPoints(double amt)
        {
            double Points = amt / 10;
        }
        public bool RedeemPoints(int p)
        {
            if(Points >= 100 && Points >= p)
            {
                Points -= p;
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return base.ToString() + "\tStatus: "+ Status;
        }




    }
}
