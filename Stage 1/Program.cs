// See https://aka.ms/new-console-template for more information

using Stage_1;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

void DisplayAllGuest()
{
    string[] lines = File.ReadAllLines("Guests.csv");
    for (int i = 0; i < lines.Length; i++)
    {
        string[] info = lines[i].Split(',');
        Console.WriteLine("{0,-10}{1,-20}{2,-20}{3,-20}", info[0], info[1], info[2], info[3]);
    }
    
}


List<Guest> guestList = new List<Guest>();


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
    Console.WriteLine("Enter your choice: ");

    int choice=Convert.ToInt32(Console.ReadLine());

    if (choice == 1)
    {
        DisplayAllGuest();
    }

    if(choice == 2)
    {
        DisplayAllRoom();
    }

    if (choice == 3)
    {
        Console.WriteLine("Enter your name: ");
        string name = Console.ReadLine();
        Console.WriteLine("Enter your passport number: ");
        string passnum = Console.ReadLine();

        Guest guest1 = new Guest(name,passnum,null,null);
        Membership m1 = new Membership("Ordinary", 0);

        guest1.Member=m1;
       
        guestList.Add(guest1);

        using (StreamWriter sw=new StreamWriter("Guests.csv", true))
        {
            sw.WriteLine(guest1.Name+","+ guest1.PassportNum+","+guest1.Member.Status+","+guest1.Member.Points);
        }

        Console.WriteLine("Registration Successful");


        


    }

    if (choice == 4)
    {

    }

    if (choice == 5)
    {

    }

    if (choice == 6)
    {

    }
}



