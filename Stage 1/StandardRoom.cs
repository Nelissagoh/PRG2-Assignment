using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_1
{
    class StandardRoom:Room
    {
        public bool RequireWifi { get; set; }
        public bool RequireBreakfast { get; set; }
        public StandardRoom(){}
        public StandardRoom(int rn,string bc,double dr,bool ia)
            :base(rn,bc,dr,ia)
        {

        }
        public override double CalculateCharges()
        {
            return 1;
        }
        public override string ToString()
        {
            return base.ToString()+"\tRequireWifi: "+RequireWifi 
                + "\tRequireBreakfast: " + RequireBreakfast;
        }
    }
}
