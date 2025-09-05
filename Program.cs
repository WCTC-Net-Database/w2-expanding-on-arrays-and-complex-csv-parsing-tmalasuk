using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper.TypeConversion;


namespace My_Awesome_Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var charManager = new CharacterManager();
            var dataManager = new DataManager();

            //returns an initial list of Character objects from the input.csv file
            //var masterList = dataManager.InitializeCharList();
            var arrayOfCharacters = dataManager.InitializeArray(); //used to satisfy homework requirement
            Console.WriteLine($"Welcome! {arrayOfCharacters.Length -1} characters have been loaded from save file.");

            

            var exit = false;
            while (!exit)
            {
                Console.WriteLine("1. Display Characters\n2. Add Character\n3. Level Up Character\n4. Find Character\n5. Exit");
                Console.Write("> ");
                var userInput = Console.ReadLine()?.Trim();

                switch (userInput)
                {
                    case "1":
                        //charManager.DisplayCharacters(masterList);
                        charManager.DisplayCharacterUsingArray(arrayOfCharacters); //used to satisfy homework requirement
                        break;

                    case "2":
                        //charManager.AddCharacter(masterList);
                        charManager.AddCharacterUsingArray(ref arrayOfCharacters); //used to satisfy homework requirement
                        break;

                    case "3":
                        //charManager.LevelUpCharacter(masterList);
                        charManager.LevelUpCharacterUsingArray(ref arrayOfCharacters); //used to satisfy homework requirement
                        break;
                    case "4":
                        charManager.FindCharacter(); //not implemented yet
                        break;
                    case "5":
                        //this version will only write to file when exiting
                        //dataManager.RewriteFile(masterList);
                        dataManager.RewriteFileUsingArray(arrayOfCharacters); //used to satisfy homework requirement
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }

        }

    }

}






