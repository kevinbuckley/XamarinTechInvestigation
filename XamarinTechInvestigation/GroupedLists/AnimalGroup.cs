using System.Collections.Generic;

namespace XamarinTechInvestigation.GroupedLists
{
    public class AnimalGroup : List<Animal>
    {
        public string Name { get; private set; }
        public List<Animal> Animals { get; set; } = new List<Animal>();
        public AnimalGroup(string name, List<Animal> animals) 
        {
            Animals = animals;
            Name = name;
        }
    }
}

