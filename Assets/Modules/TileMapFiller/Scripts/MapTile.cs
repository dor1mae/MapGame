using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName ="MapTile", menuName ="Map/MapTiles", order =1)]
public class MapTile : TileBase
{
    [SerializeField] private Sprite sprite;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = sprite;
    }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        tilemap.RefreshTile(position);
    }
}
