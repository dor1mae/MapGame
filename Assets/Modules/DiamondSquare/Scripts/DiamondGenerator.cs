using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondGenerator : MonoBehaviour
{
    [SerializeField] private int _size;
    [SerializeField] private Image _image;

    [SerializeField] private float _outsideHeight;
    [SerializeField] private float _roughness;
    [SerializeField] private float _standartHeight;
    [SerializeField] private float _angleHeight;

    [SerializeField] private TileMapFiller _tileMapFiller;

    [SerializeField] private Button _button;

    [SerializeField] private Color _waterColor;
    [SerializeField] private Color _sandColor;
    [SerializeField] private Color _earthColor;
    [SerializeField] private Color _mountainColor;
    [SerializeField] private Color _forestColor;
    [SerializeField] private Color _deepWaterColor;
    [SerializeField] private Color _highMountainColor;

    private void OnValidate()
    {
        if(_standartHeight == _outsideHeight)
        {
            _outsideHeight++;
        }
    }

    private void Start()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
            {
                var generator = new DiamondHeightMap(_size, _outsideHeight, _roughness, _standartHeight, _angleHeight);
                var map = generator.GenerateMap();
                var texture = new Texture2D((int)Mathf.Pow(2, _size) + 1, (int)Mathf.Pow(2, _size) + 1);

                for(int i = 0; i < texture.width; i++)
                {
                    for(int j = 0; j < texture.height; j++)
                    {
                        texture.SetPixel(i, j, CalcColor(map[i, j]));
                    }
                }
                texture.Apply();

                var sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0, 0), 100f);
                sprite.texture.anisoLevel = 0;
                sprite.texture.filterMode = FilterMode.Point;
                sprite.texture.Apply();

                _image.sprite = sprite;

                _tileMapFiller.FillTilemap(map);

                Debug.Log($"Изображение имеет размер {texture.width}x{texture.height}");
            }
            );
    }

    private Color CalcColor(float height)
    {
        if (height < 0.02f) return _deepWaterColor;
        else if (height < 0.05f) return _waterColor;
        else if (height < 0.075f) return _sandColor;
        else if (height < 0.2f) return _earthColor;
        else if (height < 0.35f) return _forestColor;
        else if (height < 0.6f) return _mountainColor;
        else return _highMountainColor;
    }
}
