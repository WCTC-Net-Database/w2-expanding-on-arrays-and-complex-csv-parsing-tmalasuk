using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper.TypeConversion;


namespace My_Awesome_Program
{
    public class CharacterManager
    {
        private const string FilePath = "input.csv";

        // Returns a list of Character objects from the CSV file
        public List<Character> InitializeCharList()
        {
            using (var reader = new StreamReader(FilePath))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true
                };

                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<CharacterMap>();
                    return csv.GetRecords<Character>().ToList();
                }
            }
        }

        // Displays all characters in the provided list
        public void DisplayCharacters(List<Character> masterList)
        {
            foreach (var character in masterList)
            {
                Console.WriteLine($"Name: {character.Name}");
                Console.WriteLine($"Class: {character.Profession}");
                Console.WriteLine($"Level: {character.Level}");
                Console.WriteLine($"HP: {character.HP}");
                Console.WriteLine($"Equipment: {string.Join(", ", character.Equipment)}");
                Console.WriteLine("+---------------------+");
            }
            Console.WriteLine();
        }

        //HW requirement: Displays characters from an array of strings
        public void DisplayCharacterUsingArray(String[] lines)
        {
            //displays characters from the array
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

        // Adds a new character to the provided list
        public void AddCharacter(List<Character> masterList)
        {
            Console.Write("Enter character name: ");
            var name = Console.ReadLine()?.Trim();

            Console.Write("Enter character profession: ");
            var profession = Console.ReadLine()?.Trim();

            Console.Write("Enter character level: ");
            var levelInput = Console.ReadLine();
            int level;
            while (!int.TryParse(levelInput, out level) || level < 1)
            {
                Console.Write("Invalid input. Please enter a positive integer for level: ");
                levelInput = Console.ReadLine();
            }

            Console.Write("Enter character HP: ");
            var hpInput = Console.ReadLine();
            int hp;
            while (!int.TryParse(hpInput, out hp) || hp < 1)
            {
                Console.Write("Invalid input. Please enter a positive integer for HP: ");
                hpInput = Console.ReadLine();
            }

            var equipment = new List<string>();
            var item = "";
            while (equipment.Count == 0 || item != "done")
            {
                Console.Write("Enter one equipment item (or type 'done' to finish): ");
                item = Console.ReadLine()?.Trim();
                if (item?.ToLower() == "done")
                {
                    break;
                }
                
                equipment.Add(item);
                
            }

            var newCharacter = new Character(name, profession, level, hp, equipment);
            masterList.Add(newCharacter);

            Console.WriteLine($"Character {name} added successfully!");
            Console.WriteLine();
        }

        //HW requirement: Adds a new character to the provided array of strings
        public String[] AddCharacterUsingArray(ref string[] lines)
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

            return lines;

            
        }


        // Levels up a character in the provided list
        public void LevelUpCharacter(List<Character> masterList)
        {
            Console.Write("Enter the name of the character you want to level up: ");
            var desiredCharName = Console.ReadLine();

            var characterToLevelUp = masterList.FirstOrDefault(c => c.Name == desiredCharName);

            if (characterToLevelUp != null)
            {
                characterToLevelUp.Level++;
                Console.WriteLine($"Character {desiredCharName} has been leveled up! Current level: {characterToLevelUp.Level}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Character {desiredCharName} not found. Returning to main menu.");
            }
        }

        //HW requirement: Array leveling up
        public string[] LevelUpCharacterUsingArray(ref string[] lines)
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

                    if (name.Contains(','))
                    {
                        name = $"\"{name}\"";
                    }

                    // TODO: Update the line with the new level
                    lines[i] = $"{name},{characterClass},{level},{hitPoints},{equipment}";
                    break;

                }
                
            }
            return lines;
        }


        public void FindCharacter()
        {
            // Not implemented in this version
        }
    }
}
