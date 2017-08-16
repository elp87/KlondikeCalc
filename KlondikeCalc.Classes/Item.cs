using System.Collections.Generic;

namespace KlondikeCalc.Classes
{
    public class Item
    {
        public Item(int id, string name, string description)
        {
            ID = id;
            Name = name;
            Description = description;
        }

        public Item()
        {
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
    }
}
