using System.Collections.Generic;

namespace AgentsSystem {
    public class Event {
        public int id;
        public string name;
        public string description;
        public int[] agentIDs;

        public Event(int _id, string _name, string _description, int[] _agentIDs)
        {
            id = _id;
            name = _name;
            description = _description;
            agentIDs = _agentIDs;
        }
    }
}