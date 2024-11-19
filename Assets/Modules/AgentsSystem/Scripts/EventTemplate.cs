using System;
using System.Collections.Generic;

namespace AgentsSystem {
    public class EventTemplate {
        public int id;
        public string name;
        public string description;
        public int[] agentIDs;
        public List<IRequrement> requirements;
        //public List<Action<Agent>> results; //пока не знаю, как оно будет

        public EventTemplate(int _id, string _name, string _description)
        {
            id = _id;
            name = _name;
            description = _description;
        }
        public EventTemplate(int _id, string _name, string _description, int[] _agentIDs)
        {
            id = _id;
            name = _name;
            description = _description;
            agentIDs = _agentIDs;
        }
    }
}