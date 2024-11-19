using System.Collections.Generic;

namespace AgentsSystem {
    public class Agent {
        public int id;
        public string name;
        public float health;
        public int age;
        public List<Trait> traits;
        public Dictionary<int, string> relationships;

        public Agent(int _id, string _name) {
            id = _id;
            name = _name;
        }

        public void changeRelationship(int _id, string _relationshipType){
            if (relationships.ContainsKey(_id)){
                relationships[_id] = _relationshipType;
            } else {
                relationships.Add(_id, _relationshipType);
            }
        }

        public void changeHealth(float healthChange){
            if (health + healthChange > 0){ health += healthChange;}
            else { health = 0;}
        }
    }
}