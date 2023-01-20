// See https://aka.ms/new-console-template for more information

using Stage_1;
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

DisplayAllGuest();


