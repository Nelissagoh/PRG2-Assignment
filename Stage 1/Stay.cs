using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_1
{
    internal class Stay
    {
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public List<Room> RoomList { get; set; } = new List<Room>();
        public Stay() { }
        public Stay(DateTime i,DateTime o)
        {
            CheckinDate = i;
            CheckoutDate = o;
        }
        public void AddRoom(Room r)
        {
            RoomList.Add(r);
        }
        public double CalculateTotal()
        {
            return 1;
        }
        public override string ToString()
        {
            return ToString();
        }


    }

}
