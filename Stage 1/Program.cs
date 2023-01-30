// See https://aka.ms/new-console-template for more information

using Stage_1;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


//function to display all guest
void DisplayAllGuest()
{
    string[] lines = File.ReadAllLines("Guests.csv");
    for (int i = 0; i < lines.Length; i++)
    {
        string[] info = lines[i].Split(',');
        Console.WriteLine("{0,-10}{1,-20}{2,-20}{3,-20}", info[0], info[1], info[2], info[3]);
    }
    
}

//Adding guest csv info into guestList
void InitData(List<Guest> guestList, List<Guest> stayList)
{
    string[] Lines = File.ReadAllLines("Guests.csv");
    for (int i = 1; i < Lines.Length; i++)
    {
        string[] data = Lines[i].Split(',');
        Guest g = new Guest(data[0], data[1], null, new Membership(data[2], Convert.ToInt32(data[3])));
        guestList.Add(g);

        /*
        Lines = File.ReadAllLines("Stays.csv");

        for (int a = 1; a < Lines.Length; a++)
        {
            string[] y = Lines[a].Split(',');
            var guest = guestList.Find(x => y[0].Equals(x.Name));
            Stay s = new Stay(Convert.ToDateTime(y[3]), Convert.ToDateTime(y[4]));
            g.HotelStay = s;
        }*/

    }




    foreach (Guest guest in guestList)
    {
        Console.WriteLine(guestList);
    }



}

//Creating a guestlist
List<Guest> guestList = new List<Guest>();


//Adding guest csv info into guestList and stay csv info into stay list
InitData(guestList, stayList);


//search method
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



//functiom to display all name in stay file
void DisplayStayName()
{
    string[] information = File.ReadAllLines("Stays.csv");
    for (int l = 1; l < information.Length; l++)
    {
        string[] name = information[l].Split(',');
        Console.WriteLine("[" + l + "]" +name[0]);
    }
}

List<Stay> stayList = new List<Stay>();


void DisplayAllRoom()
{
    string[] csv = File.ReadAllLines("Rooms.csv");
    for (int i = 0; i < csv.Length; i++)
    {
        string[] rooms = csv[i].Split(',');
        Console.WriteLine("{0,-20}{1,-20}{2,-30}{3,-20}", rooms[0], rooms[1], rooms[2], rooms[3]);
    }
}

List<Room> roomList = new List<Room>();



while (true)
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

    int choice=Convert.ToInt32(Console.ReadLine());

    if (choice == 1)
    {
        //call for the function
        DisplayAllGuest();
    }

    if(choice == 2)
    {
        DisplayAllRoom();
    }

    if (choice == 3)
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
        }
        catch (FormatException ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    if (choice == 4)
    {
        //Display guest name in stay file
        DisplayStayName();           

        //prompt user to select a guest and retrieved the selected guest
        Console.Write("Enter guest name to retrieve: ");
        string opt=Console.ReadLine();

        // prompt user to enter check in date
        Console.Write("Enter check in date:");
        int checkin = Convert.ToInt32(Console.ReadLine());

        // prompt user to enter check out date
        Console.Write("Enter check out date:");
        int checkout = Convert.ToInt32(Console.ReadLine());

        // create stay object with the information given

        //List all available rooms
        DisplayAllRoom();

        // prompt user to select a room and retrieve the selected room
        Console.Write("Select a room to stay:");
        string room = Console.ReadLine();  //To list out all the available rooms
                                          // Standard Room: require wifi [Y/N] & require breakfast [Y/N]
                                         //  Deluxe Room: additional bed [Y/N]

        // update the user response to the room object

        // update the availability of the room

        //add the room object to the room list of the stay

        // prompt user whether want to select another room, if yes, repeat the above steps from list 
        Console.Write("Do you want to select another room: ");
        string option = Console.ReadLine();
        if (option == "yes")
        {
            //Do something
        }
        else if (option == "no")
        {
            //Do something
        }
        else
        {
            Console.WriteLine("Invalid Option");
        }

        // assign the stay object to the guest


        // update check in status of the guest

        // display a message to indicate check in successfully
        Console.WriteLine("Check in successfully");
    }

    if (choice == 5)
    {
        //Display guest name in stay file
        DisplayAllGuest();

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
                    Console.WriteLine("Name: {0,-15} PassportNumber: " +
                        "{1,-15}{2,-20}", g.Name, g.PassportNum, g.HotelStay);

                    if (g.HotelStay == null)
                    {
                        Console.WriteLine("Guest not found. Please try agian.");
                        continue;
                    }

                    else
                    {
                        Console.WriteLine("CheckInDate: {0,-15} CheckOutDate: " +
                        "{1,-15}", g.HotelStay.CheckinDate, g.HotelStay.CheckoutDate);

                        /*
                        Console.WriteLine("---------------------Details---------------------");
                        Console.WriteLine("CheckInDate: {0,-15} CheckOutDate: {1,-15} RoomNumber: " +
                            "{2,-15} BedConfiguration: {3,-15} DailyRate: {4,-15} IsAvail: {5,-15}"
                            , g.HotelStay.CheckinDate, g.HotelStay.CheckoutDate);
                        */
                    }


                }

            }

        }

    }

    if (choice == 6)
    {
        // List the guests
        DisplayAllGuest();

        // prompt user to select a guest and retrieve the selected guest
        Console.Write("Enter guest name to retrieve: ");
        string opt = Console.ReadLine();

        // check if the guest is checked in
        if (something)
        {
            //do something
        }
        else
        {
            //do something
        }
        // prompt user for the number of day to extend
        Console.Write("Enter number of days to extend: ");
        int days = Convert.ToInt32(Console.ReadLine());

        // retrieve the stay object of the guest

        // compute and update the check out date of the stay
    }



    else
    {
        Console.WriteLine("Invalid Options");
    }
}

//Must remember to do validation!!! For example (if there 6 option, then user enter option 7, should prompt error

