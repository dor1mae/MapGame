using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TileMapFiller : MonoBehaviour
{
    private Tilemap _tileMap;
    private float[,] _heightMap;

    [SerializeField] private MapTile _grass;
    [SerializeField] private MapTile _tree;
    [SerializeField] private MapTile _water;
    [SerializeField] private MapTile _deepwater;
    [SerializeField] private MapTile _sand;
    [SerializeField] private MapTile _mountain;
    [SerializeField] private MapTile _highmountain;

    private void OnValidate()
    {
        if(!_tileMap) _tileMap = GetComponent<Tilemap>();
    }

    public void FillTilemap(float[,] heights, int size)
    {
        _heightMap = heights;

        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                CalcSprite(_heightMap[i, j], i, j);
            }
        }

        _tileMap.RefreshAllTiles();
    }

    private void CalcSprite(float height, int x, int y)
    {
        if (height < 0.02f)
        {
            _tileMap.SetTile(new Vector3Int(x, y), _deepwater);
        }
        else if(height < 0.05f)
        {
            _tileMap.SetTile(new Vector3Int(x, y), _water);
        }
        else if (height < 0.075f)
        {
            _tileMap.SetTile(new Vector3Int(x, y), _sand);
        }
        else if (height < 0.2f)
        {
            _tileMap.SetTile(new Vector3Int(x, y), _grass);
        }
        else if (height < 0.35f)
        {
            _tileMap.SetTile(new Vector3Int(x, y), _tree);
        }
        else if (height < 0.6f)
        {
            _tileMap.SetTile(new Vector3Int(x, y), _mountain);
        }
        else
        {
            _tileMap.SetTile(new Vector3Int(x, y), _highmountain);
        }
    }
}
