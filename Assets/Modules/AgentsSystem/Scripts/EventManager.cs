using UnityEngine;
using System.Collections.Generic;

namespace AgentsSystem {
    public class EventSystem : MonoBehaviour
    {
        public List<Agent> agentsLogged = new List<Agent>();
        public List<EventTemplate> eventsLogged = new List<EventTemplate>();
        public List<EventTemplate> eventsStorage = new List<EventTemplate>();
        EventTemplate Event1;
        EventTemplate Event2;
        EventTemplate Event3;
        int counter = 0;
        void Awake()
        {
            eventsStorage.Add(new EventTemplate(0, "Heroic event", "Hero saved the day"));
            eventsStorage[0].requirements.Add(new AgentHasTraitRequirement(0)); //айдишник-то есть, но хранить пока негде
            
        }

        void Update()
        {
            
        }

        void AddAndDebug(EventTemplate eventToAdd){
            eventsLogged.Add(eventToAdd);
            Debug.Log("Added: " + eventToAdd.name + ", id: " + eventToAdd.id);
        }

        void RemoveAndDebug(int _id){
            Debug.Log("Removed: " + eventsLogged[_id].name);
            eventsLogged.RemoveAt(_id);
        }
    }
}

