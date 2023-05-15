using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMap : MonoBehaviour
{
    private Tilemap tilemap;
    public position = transform.position;
    public Vector3[] availablePath = [];

    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        foreach (Vector3Int pos in tilemap.cellBounds.allPosition)
        {
            if (!tilemap.HasTile(pos))
            {
                // save the position of the tile
                availablePath.push(pos);
            }
        }
    }
}
