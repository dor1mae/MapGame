using System.Collections.Generic;

namespace AgentsSystem{
    public class Trait{
        public int id;
        public string name;
        public string description;
        public List<string> tags;
        public Dictionary<string, float> effects;

        public Trait(int _id, string _name, string _description, List<string> _tags, Dictionary<string,float> _effects) {
            id = _id;
            name = _name;
            description = _description;
            tags = _tags;
            effects = _effects;
         }
    }
}