using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DiamondGenerator : MonoBehaviour
{
    [SerializeField] private int _size;
    [SerializeField] private Image _image;

    [SerializeField] private float _basicHeight;

    [SerializeField] private Button _button;

    [SerializeField] private Color _waterColor;
    [SerializeField] private Color _sandColor;
    [SerializeField] private Color _earthColor;
    [SerializeField] private Color _mountainColor;

    private void Start()
    {
        var generator = new DiamondHeightMap(_size, _basicHeight);

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
            {
                var map = generator.GenerateMap();
                var texture = new Texture2D((int)Mathf.Pow(2, _size) + 1, (int)Mathf.Pow(2, _size) + 1);
                var col = new Color[texture.width * texture.height];

                for(int i = 0; i < texture.width; i++)
                {
                    for(int j = 0; j < texture.height; j++)
                    {
                        texture.SetPixel(i, j, CalcColor(map[i, j]));
                    }
                }
                texture.Apply();

                _image.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
            }
            );
    }

    private Color CalcColor(float height)
    {
        if (height < 0.1f) return _waterColor;
        else if (height < 0.3f) return _sandColor;
        else if (height < 0.6f) return _earthColor;
        else return _mountainColor;
    }
}
