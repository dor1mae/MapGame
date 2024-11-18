using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace AgentsSystem
{
    public class LogController : MonoBehaviour
    {

        public List<AgentsSystem.Agent> agentsLogged = new List<Agent>();
        public List<AgentsSystem.Event> eventsLogged = new List<Event>();
        AgentsSystem.Event Event1;
        AgentsSystem.Event Event2;
        AgentsSystem.Event Event3;
        AgentsSystem.Agent Agent3 = new Agent(3, "Jenna Yakowski");
        int counter = 0;

        public VisualTreeAsset eventListItemTemplate;
        private ListView eventsListView;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            agentsLogged.Add(new Agent(1, "Jack Black"));
            agentsLogged.Add(new Agent(2, "John Jakowski"));
            
            int[] tempArray1 = {0};
            Event1 = new Event(1, "The Beginning", "Once upon a time, there was something", tempArray1);
            int[] tempArray2 = {0,1};
            Event2 = new Event(1, "The Second Choice", "Once upon a time, there was something. But the something didn't exist for long enough for history to form", tempArray2);
            int[] tempArray3 = {1};
            Event3 = new Event(1, "The End", "how\nshould\nthings\nbe?", tempArray3);

            var root = GetComponent<UIDocument>().rootVisualElement;
            eventsListView = root.Q<ListView>("eventsList");

            SetupListView();
        }

        private void SetupListView()
        {
            eventsListView.makeItem = () =>
            {
                return eventListItemTemplate.CloneTree();
            };

            eventsListView.bindItem = (element, index) =>
            {
                var eventItem = eventsLogged[index];
                var nameLabel = element.Q<Label>("nameLabel");
                var descriptionLabel = element.Q<TextElement>("eventDescription");

                nameLabel.text = eventItem.name;
                descriptionLabel.text = eventItem.description;
            };

            eventsListView.itemsSource = eventsLogged;
            eventsListView.selectionType = SelectionType.Single;
        }

        // Update is called once per frame
        void Update()
        {
            if (counter == 500) eventsLogged.Add(Event1);
            if (counter == 2500) eventsLogged.Add(Event2);
            //if (counter == 157) agentsLogged.Add(Agent3);
            if (counter == 3500) eventsLogged.Add(Event3);
            counter++;
            if (counter >=5000) counter = 0;
        }
    }
}

