using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexTilemapRenderer : MonoBehaviour
{

    public Tilemap tilemap;

    public List<PlayerTileset> playerTilesets = new List<PlayerTileset>();


    [SerializeField]
    public Dictionary<PlayerTag, Tileset> tilesetByPlayer = new Dictionary<PlayerTag, Tileset>();

    public Tile freeTile;
    public Tile selectedTile;


    void Awake()
    {
        foreach (var playerTileset in playerTilesets)
        {
            tilesetByPlayer[playerTileset.key] = playerTileset.val;
        }
    }

    public List<Vector3Int> getTilemapFieldPositions()
    {
        List<Vector3Int> fieldTiles = new List<Vector3Int>();
        for (int n = tilemap.cellBounds.xMin; n < tilemap.cellBounds.xMax; n++)
        {
            for (int p = tilemap.cellBounds.yMin; p < tilemap.cellBounds.yMax; p++)
            {
                Vector3Int cellPosition = new Vector3Int(n, p, (int)tilemap.transform.position.y);
                if (tilemap.HasTile(cellPosition))
                {
                    // Debug.Log("Active Tile Position: " + cellPosition);
                    fieldTiles.Add(cellPosition);
                }
                else
                {
                    // Debug.Log("Inactive Tile Position: " + cellPosition);
                }
            }
        }
        return fieldTiles;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void setTileConquered(Vector3Int cellPosition, PlayerTag playerTag)
    {
        if (tilesetByPlayer[playerTag].conqueredTile != null)
        {
            tilemap.SetTile(cellPosition, tilesetByPlayer[playerTag].conqueredTile);
        }
    }

    public void setTileFreed(Vector3Int cellPosition, PlayerTag playerTag)
    {
        tilemap.SetTile(cellPosition, freeTile);
    }

    public void setTileAdjacent(Vector3Int cellPosition, PlayerTag playerTag)
    {
        if (tilesetByPlayer[playerTag].adjacentTile != null)
        {
        tilemap.SetTile(cellPosition, tilesetByPlayer[playerTag].adjacentTile);
        }
    }

    public void setTileSelected(Vector3Int cellPosition, PlayerTag playerTag)
    {
        tilemap.SetTile(cellPosition, selectedTile);
    }
}

[Serializable]
public class Tileset
{

    public Tile adjacentTile;
    public Tile conqueredTile;
}

[Serializable]
public class PlayerTileset
{
    public PlayerTag key;
    public Tileset val;
}
