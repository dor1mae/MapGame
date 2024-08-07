using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TileMapFiller : MonoBehaviour
{
    private Tilemap _tileMap;
    private float[,] _heightMap;

    [SerializeField] private MapTile _grass;

    private void OnValidate()
    {
        if(!_tileMap) _tileMap = GetComponent<Tilemap>();
    }

    public void FillTilemap(float[,] heights)
    {
        _heightMap = heights;

        for(int i = 0; i < 64; i++)
        {
            for(int j = 0; j < 64; j++)
            {
                _tileMap.SetTile(new Vector3Int(i, j), _grass);
            }
        }

        _tileMap.RefreshAllTiles();
    }
}
