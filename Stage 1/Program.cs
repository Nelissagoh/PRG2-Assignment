// See https://aka.ms/new-console-template for more information

using Stage_1;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;




//Creating a guestlist
List<Guest> guestList = new List<Guest>();
//Creating a staylist
List<Stay> stayList = new List<Stay>();
//Creating a roomlist
List<Room> roomList = new List<Room>();



void SetRoom(Room r, bool rw, bool rb, bool ab)
{
    if (r is StandardRoom sr)
    {
    sr.RequireWifi = rw;
    sr.RequireBreakfast = rb;
    }
    else if (r is DeluxeRoom dr)
    {
    dr.AdditionalBed = ab;
    }
}
void InitData(List<Guest> guestList, List<Room> roomList, List<Stay> stayList)
{
    string[] csvLines = File.ReadAllLines("Guests.csv");
    for (int i = 1; i < csvLines.Length; i++)
    {
        string[] data = csvLines[i].Split(',');
        Guest g = new Guest(data[0], data[1], null, new Membership(data[2], Convert.ToInt32(data[3])));
        guestList.Add(g);
    }
    csvLines = File.ReadAllLines("Rooms.csv");
    for (int i = 1; i < csvLines.Length; i++)
    {
        string[] data = csvLines[i].Split(',');
        Room r;
        if (data[0] == "Standard")
        {
            r = new StandardRoom(Convert.ToInt32(data[1]), data[2], Convert.ToDouble(data[3]), true);
        }
        else
        {
            r = new DeluxeRoom(Convert.ToInt32(data[1]), data[2], Convert.ToDouble(data[3]), true);
        }
        roomList.Add(r);
    }
    csvLines = File.ReadAllLines("Stays.csv");
    for (int i = 1; i < csvLines.Length; i++)
    {
        string[] data = csvLines[i].Split(',');
        var guest = guestList.Find(x => data[1].Equals(x.PassportNum));
        var room1 = roomList.Find(x => Convert.ToInt32(data[5]) == x.RoomNumber);
        Stay stay = new Stay(Convert.ToDateTime(data[3]), Convert.ToDateTime(data[4]));
        guest.HotelStay = stay;
        guest.IsCheckedin = Convert.ToBoolean(data[2]);
        SetRoom(room1, Convert.ToBoolean(data[6]), Convert.ToBoolean(data[7]), Convert.ToBoolean(data[8]));
        stay.AddRoom(room1);
        if (data[2] == "TRUE")
        {
            room1.IsAvail = false;
            //Console.WriteLine(room1.RoomNumber);
        }
        if (data[9] != "")
        {
            var room2 = roomList.Find(x => Convert.ToInt32(data[9]) == x.RoomNumber);
            SetRoom(room2, Convert.ToBoolean(data[10]), Convert.ToBoolean(data[11]), Convert.ToBoolean(data[12]));
            stay.AddRoom(room2);
            if (data[2] == "TRUE")
            {
                room2.IsAvail = false;
                //Console.WriteLine(room2.RoomNumber);
            }
        }
        stayList.Add(stay);
    }

}



InitData(guestList, roomList, stayList);




//search method for feature  4
Guest? Searchguest(List<Guest> guestList, string? n)
    {
        foreach (Guest i in guestList)
        {
            if (i.Name == n)
            {
                return i;
            }
        }
        return null;
    }

    //search method for feature 5
    Guest? Search(List<Guest> guestList, string? name)
    {
        foreach (Guest g in guestList)
        {
            if (g.Name == name)
            {
                return g;
            }
        }
        return null;
    }

foreach(Room r in roomList)
{
    Console.WriteLine(r);
}




while (true)
{
    try
    {
        Console.WriteLine("----------------M E N U --------------------");
        Console.WriteLine("[1] List all guest");
        Console.WriteLine("[2] List all available rooms");
        Console.WriteLine("[3] Register Guest");
        Console.WriteLine("[4] Check-in guest");
        Console.WriteLine("[5] Display stay details of a guest");
        Console.WriteLine("[6] Extends the stay by numbers of day");
        Console.WriteLine("[0] Exit");
        Console.WriteLine("---------------------------------------------");
        Console.Write("Enter your choice: ");

        int choice = Convert.ToInt32(Console.ReadLine());


        if (choice == 1)
        {
            //Display all guest info
            Console.WriteLine("List of guest");
            foreach (var guest in guestList)
            {
                Console.WriteLine("Name: {0,-10} Passportnum: {1,-10}", guest.Name, guest.PassportNum);
            }
        }

        else if (choice == 2)
        {
            Console.WriteLine("List of rooms");
            foreach (var room in roomList)
            {
                if (room.IsAvail)
                {
                    Console.WriteLine(room);
                }

            }
        }

        else if (choice == 3)
        {
            try
            {
                //prompt user for name and passport number
                Console.WriteLine("Enter your name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter your passport number: ");
                string passnum = Console.ReadLine();

                //create a guest object with the information given
                Guest guest1 = new Guest(name, passnum, null, null);

                //create membership object
                Membership m1 = new Membership("Ordinary", 0);

                //assign membership object to the guest
                guest1.Member = m1;

                //add the guest object to the guest list
                guestList.Add(guest1);

                //append the guests information to the guest.csv file
                using (StreamWriter sw = new StreamWriter("Guests.csv", true))
                {
                    sw.WriteLine(guest1.Name + "," + guest1.PassportNum + "," + guest1.Member.Status + "," + guest1.Member.Points);
                }

                //display a message to indicate registration status
                Console.WriteLine("Registration Successful");
                Console.WriteLine("Welcome " + name + " !");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        else if (choice == 4)
        {
            //Display guest name
            Console.WriteLine("List of guest");
            foreach (var guest in guestList)
            {
                Console.WriteLine(guest);
            }
            //prompt user to select a guest
            Console.WriteLine("Enter your choice of name: ");
            string? n = Console.ReadLine();
            n = n.ToLower();
            //Make first letter uppercase
            n = char.ToUpper(n[0]) + n.Substring(1);
            Guest? guestcheck = Searchguest(guestList, n);

            //exception
            bool issString = true;
            foreach (char c in n)
            {
                if (char.IsLetter(c))
                {
                    issString = false;
                    break;
                }
            }

            //ensure only char
            if (issString)
            {
                Console.WriteLine("Enter guest name(Only Character)");
                Console.WriteLine("Example: Tony");
                continue;
            }
            //show error if guest name not found

            else if (guestcheck == null)
            {

                Console.WriteLine("Guest not found. Please try agian. ");
                continue;
            }

            //if guest name is found
            else
            {
                foreach (Guest i in guestList)
                {
                    if (i.Name == n)
                    {
                        Console.WriteLine("Name: {0,-15} PassportNumber: " +
                            "{1,-15} Check in: {2,-20} ", i.Name, i.PassportNum, i.HotelStay);
                    }
                }
                //Prompt user for check in date
                Console.WriteLine("Enter Check in date[25/12/2022]: ");
                string checkin = Console.ReadLine();

                //Prompt user for check in date
                Console.WriteLine("Enter Check out date[25/12/2022]: ");
                string checkout = Console.ReadLine();

                //create a guest object with the information given
                Stay stay1 = new Stay(Convert.ToDateTime(checkin), Convert.ToDateTime(checkout));

                //List all available rooms


                //Prompt user to select a room and retrieve the selected room
                Console.WriteLine("Select a room: ");



            }
        }


        else if (choice == 5)
        {
            Console.WriteLine("------------Guest informations-----------");
            //Display guest name in stay file
            foreach (var guest in guestList)
            {
                Console.WriteLine("Name: {0,-10} PassportNum: {1,-10}", guest.Name, guest.PassportNum);
            }

            //prompt user to select a guest
            Console.WriteLine("Enter your choice of name: ");
            string? name = Console.ReadLine();
            name = name.ToLower();
            //Make first letter uppercase
            name = char.ToUpper(name[0]) + name.Substring(1);
            Guest? gcheck = Search(guestList, name);

            //exception
            bool isString = true;
            foreach (char c in name)
            {
                if (char.IsLetter(c))
                {
                    isString = false;
                    break;
                }
            }

            //ensure only char
            if (isString)
            {
                Console.WriteLine("Enter guest name(Only Character)");
                Console.WriteLine("Example: Tony");
                continue;
            }
            //show error if guest name not found


            else if (gcheck == null)
            {

                Console.WriteLine("Guest not found. Please try agian. ");
                continue;
            }

            //if guest name is found

            else
            {
                foreach (Guest g in guestList)
                {
                    if (g.Name == name)
                    {
                        Console.WriteLine("----------------------Guest informations----------------------");
                        Console.WriteLine("Name: {0,-5}\nPassportNumber: {1,-5} ", g.Name, g.PassportNum);

                        if (g.HotelStay == null)
                        {
                            Console.WriteLine("Guest not found. Please try agian.");
                            continue;
                        }


                        else
                        {

                            Console.WriteLine("-----------------------CheckIn/CheckOut--------------------");
                            Console.WriteLine("{0,-5}", g.HotelStay);
                            Console.WriteLine("------------------------Room Details-----------------------");

                            string[] lines = File.ReadAllLines("Stays.csv");
                            foreach (string line in lines)
                            {
                                string[] split = line.Split(',');

                                if (split[0] == name)
                                {
                                    if (split[9] == "")
                                    {
                                        Console.WriteLine("Room number: {0,-10} Require Wifi: {1,-10} \nRequire Breakfast: {2,-10}Extra Bed: {3,-10}", 
                                            split[5], split[6], split[7], split[8]);

                                    }

                                    else
                                    {
                                        Console.WriteLine("------------------------First Room-----------------------");
                                        Console.WriteLine("Room number: {0,-10} Require Wifi: {1,-10}\n Require Breakfast: {2,-10}Extra Bed: {3,-10}", 
                                            split[5], split[6], split[7], split[8]);
                                        Console.WriteLine("------------------------Second Room-----------------------");
                                        Console.WriteLine("Room number: {0,-10} Require Wifi: {1,-10}\n Require Breakfast: {2,-10}Extra Bed: {3,-10}", 
                                            split[9], split[10], split[11], split[12]);

                                    }

                                }
                            }



                        }



                    }
                }

            }
        }

        else if (choice == 6)
        {
            Console.WriteLine("");
        }


        else if (choice == 0)
        {
            Console.WriteLine("Thank you for staying with us!");
            Console.WriteLine("Hope to see you agian and have a nice days ahead!");
            break;
        }


        else
        {
            Console.WriteLine("Invalid Options");
        }
    }

    catch(FormatException ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine("Please try agian [ONLY NUMBERS FROM 0 to 1]");
        Console.WriteLine("For example: 1");
        continue;
    }

}





     




