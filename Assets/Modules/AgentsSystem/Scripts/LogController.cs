using UnityEngine;
using UnityEngine.UIElements;

namespace AgentsSystem
{
    public class LogController : MonoBehaviour
    {
        public VisualTreeAsset eventListItemTemplate;
        private ListView eventsListView;
        public EventSystem eventSystem;
        
        void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            eventsListView = root.Q<ListView>("eventsList");

            SetupListView();
            this.gameObject.SetActive(false);
        }

        private void SetupListView()
        {
            EventSystem eventSystem = GetComponentInParent<EventSystem>();
            eventsListView.makeItem = () =>
            {
                return eventListItemTemplate.CloneTree();
            };

            eventsListView.bindItem = (element, index) =>
            {
                var eventItem = eventSystem.eventsLogged[index];
                var nameLabel = element.Q<Label>("nameLabel");
                var descriptionLabel = element.Q<TextElement>("descriptionLabel");

                nameLabel.text = eventItem.name;
                descriptionLabel.text = eventItem.description;
            };

            eventsListView.itemsSource = eventSystem.eventsLogged;
            eventsListView.selectionType = SelectionType.Single;
        }

        /*void Update()
        {
            
        }*/
    }
}

