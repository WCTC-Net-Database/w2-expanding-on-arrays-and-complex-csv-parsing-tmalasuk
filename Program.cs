using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static string[] lines;

    static void Main()
    {
        
        string filePath = "input.csv";
        lines = File.ReadAllLines(filePath);
      

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Display Characters");
            Console.WriteLine("2. Add Character");
            Console.WriteLine("3. Level Up Character");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllCharacters(lines);
                    break;
                case "2":
                    AddCharacter(ref lines);
                    break;
                case "3":
                    LevelUpCharacter(lines);
                    break;
                case "4":
                    File.WriteAllLines(filePath, lines);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void DisplayAllCharacters(string[] lines)
    {
        // Skip the header row
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

            string name;
            int commaIndex;

            // Check if the name is quoted
            if (line.StartsWith("\""))
            {
                commaIndex = line.IndexOf("\",");
                name = line.Substring(1, commaIndex - 1);
                line = line.Substring(commaIndex + 2);

            }
            else
            {
                commaIndex = line.IndexOf(',');
                name = line.Substring(0, commaIndex);
                line = line.Substring(commaIndex + 1);
            }

            var columns = line.Split(",");

            string characterClass = columns[0];
            int level = int.Parse(columns[1]);
            int hitPoints = int.Parse(columns[2]);
            string[] equipment = columns[3].Split("|");

            // Display character information
            Console.WriteLine($"------------------\nName: {name}\nClass: {characterClass}\nLevel: {level}\nHP: {hitPoints}\nEquipment: {string.Join(", ", equipment)}\n------------------");
        }
    }

    static void AddCharacter(ref string[] lines)
    {
        
        // Prompt for character details
        Console.Write("Enter character name: ");
        string? name = Console.ReadLine();

        Console.Write("Enter character class: ");
        string? characterClass = Console.ReadLine();

        Console.Write("Enter level: ");
        int level = int.Parse(Console.ReadLine());

        Console.Write("Enter hit points: ");
        int hitPoints = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter equipment (one item per line, press Enter on an empty line to finish):");
        var equipmentList = new List<string>();
        while (true)
        {
            string equipmentItem = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(equipmentItem))
            {
                break;
            }
            equipmentList.Add(equipmentItem);
        }
        string equipment = string.Join("|", equipmentList);

        // Enclose the name in quotes if it contains a comma
        if (name.Contains(','))
        {
            name = $"\"{name}\"";
        }

        string newCharacterLine = $"{name},{characterClass},{level},{hitPoints},{equipment}";

        // Append the new character to the lines array
        // Increase the size of the array by 1
        Array.Resize(ref lines, lines.Length + 1);

        // Add the new element to the last position
        lines[lines.Length - 1] = newCharacterLine;

        Console.WriteLine("Character added successfully!");
    }

    static void LevelUpCharacter(string[] lines)
    {
        Console.Write("Enter the name of the character to level up: ");
        string nameToLevelUp = Console.ReadLine();

        // Loop through characters to find the one to level up
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

            if (line.Contains(nameToLevelUp))
            {

                // TODO: Split the rest of the fields locating the level field
                string name;
                int commaIndex;

                // Check if the name is quoted
                if (line.StartsWith("\""))
                {
                    commaIndex = line.IndexOf("\",");
                    name = line.Substring(0, commaIndex);
                    line = line.Substring(commaIndex + 2);

                }
                else
                {
                    commaIndex = line.IndexOf(',');
                    name = line.Substring(0, commaIndex);
                    line = line.Substring(commaIndex + 1);
                }

                var columns = line.Split(",");

                string characterClass = columns[0];
                int level = int.Parse(columns[1]);
                int hitPoints = int.Parse(columns[2]);
                string equipment = columns[3];


                // TODO: Level up the character
                level++;
                Console.WriteLine($"Character {name} leveled up to level {level}!");

                // TODO: Update the line with the new level
                lines[i] = $"{name},{characterClass},{level},{hitPoints},{equipment}";
                break;
            }
        }
    }

}