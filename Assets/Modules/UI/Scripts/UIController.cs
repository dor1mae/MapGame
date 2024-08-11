using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    private VisualElement _root;

    private Button _generateButton;

    private VisualElement _minimap;
    private Image image;

    [SerializeField] DiamondGenerator _generator;

    // Start is called before the first frame update
    void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;

        _minimap = _root.Q<VisualElement>("mini_map");

        image = new Image();
        _minimap.Add(image);
        image.StretchToParentSize();

        _generateButton = _root.Q<Button>("Generate_Button");
        _generateButton.RegisterCallback<ClickEvent>(OnGenerateButton);
    }

    private void OnGenerateButton(ClickEvent clickEvent)
    {
        _generator.Generate(image);
    }
}
