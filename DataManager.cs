using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Awesome_Program
{
    public class DataManager
    {
        private const string FilePath = "input.csv";
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

        public void RewriteFile(List<Character> masterList)
        {
            using (var writer = new StreamWriter(FilePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<CharacterMap>();
                csv.WriteHeader<Character>();
                csv.NextRecord();
                csv.WriteRecords(masterList);
            }
        }

        public void RewriteFileUsingArray(String[] lines)
        {
            Console.WriteLine("Saving file...");
            File.WriteAllLines(FilePath, lines);
        }

        public String[] InitializeArray()
        {
            //used to satisfy homework requirement
            string[] lines = File.ReadAllLines(FilePath);

            return lines;
        }
    }
}
