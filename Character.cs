using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Awesome_Program
{
    public class Character
    {
        public string Name { get; set; }
        public string Profession { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
        public List<string> Equipment { get; set; }

        public Character() { }

        public Character(string name, string profession, int level, int hp, List<string> equipment)
        {
            Name = name;
            Profession = profession;
            Level = level;
            HP = hp;
            Equipment = equipment;
        }
    }



    class CharacterMap : ClassMap<Character>
    {
        public CharacterMap()
        {
            Map(m => m.Name).Index(0);
            Map(m => m.Profession).Index(1);
            Map(m => m.Level).Index(2);
            Map(m => m.HP).Index(3);
            Map(m => m.Equipment).Index(4).TypeConverter<EquipmentConverter>();
        }
    }


    class EquipmentConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            var list = value as List<string>;
            return list != null ? string.Join("|", list) : string.Empty;
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return text.Split('|', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToList();
        }
    }
}
