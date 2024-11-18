using UnityEngine.UIElements;

public class EventInfoVisualElement : VisualElement
{
    public EventInfoVisualElement() {
        var nameLabel = new Label() {name = "nameLabel"};
        var eventDescription = new TextElement() {name = "eventDescription"};
    }
    
    private void OnEnable(){
        
    }
}
