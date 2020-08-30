using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour {
    public Tilemap tilemap;
    public Tile inactiveTile;
    public Tile activeTile;
    public Tile conqueredTile;
    public Tile enemyTile;
    HashSet<Vector3Int> combatTilesPositions;
    HashSet<Vector3Int> conqueredTilesPositions;
    HashSet<Vector3Int> adjacentTilesPositions;
    readonly Vector3Int startPosition = new Vector3Int(0, -2, 0);

    public static TilemapController Instance { get; private set; }

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.Log("Warning: multiple {} in scene!", this);
        }
    }

    void Start() {
        combatTilesPositions = new HashSet<Vector3Int>();
        conqueredTilesPositions = new HashSet<Vector3Int>();
        adjacentTilesPositions = new HashSet<Vector3Int>();

        for (int n = tilemap.cellBounds.xMin; n < tilemap.cellBounds.xMax; n++) {
            for (int p = tilemap.cellBounds.yMin; p < tilemap.cellBounds.yMax; p++) {
                Vector3Int cellPosition = (new Vector3Int(n, p, (int)tilemap.transform.position.y));
                if (tilemap.HasTile(cellPosition)) {
                    // Debug.Log("Active Tile Position: " + cellPosition);
                    combatTilesPositions.Add(cellPosition);
                } else {
                    // Debug.Log("Inactive Tile Position: " + cellPosition);
                }
            }
        }
        AddToAdjacentTiles(startPosition);
        AddToConqueredTiles(startPosition);

        BoundsInt boi = tilemap.cellBounds;
        Debug.Log("cellbounds: " + boi.ToString());
        BoundsInt bo2i = tilemap.cellBounds;
        Debug.Log("localBounds: " + bo2i.ToString());

    }

    // Update is called once per frame
    void Update() {

    }

    public void AddToAdjacentTiles(Vector3Int cellPosition) {
        // Add the Tile at 'cellPosition' to the adjacentTilesPositions
        adjacentTilesPositions.Add(cellPosition);
        // Change the sprite of the tile

        // Add the adjacent Tiles to adjacentTilesPositions, so they can be clicked now
        Debug.Log("cellPosition: " + cellPosition.ToString());
        updateAdjacentTile(cellPosition + new Vector3Int(1, 0, 0));
        updateAdjacentTile(cellPosition + new Vector3Int(-1, 0, 0));
        updateAdjacentTile(cellPosition + new Vector3Int(0, 1, 0));
        updateAdjacentTile(cellPosition + new Vector3Int(0, -1, 0));
        updateAdjacentTile(cellPosition + new Vector3Int(-1, -1, 0));
        // Vertical axis has offset, so neighbor coordinates change in dependancy of odd/even row
        if (isRowEven(cellPosition)) {
            updateAdjacentTile(cellPosition + new Vector3Int(-1, 1, 0));
        } else {
            updateAdjacentTile(cellPosition + new Vector3Int(1, 1, 0));
        }

    }

    private bool isRowEven(Vector3Int cellPosition) {
        if (cellPosition.y % 2 == 0) {
            return true;
        }
        return false;
    }

    public void AddToConqueredTiles(Vector3Int cellPosition) {
        // Add the Tile at 'cellPosition' to the conqueredTilesPositions
        conqueredTilesPositions.Add(cellPosition);
        // Change the sprite of the tile
        tilemap.SetTile(cellPosition, conqueredTile);
        // Add the adjacent Tiles to adjacentTilesPositions, so they can be clicked now
    }

    public bool isTileInAdjacentTiles(Vector3Int cellPosition) {
        if (adjacentTilesPositions.Contains(cellPosition)) {
            return true;
        }
        return false;
    }

    public void updateAdjacentTile(Vector3Int cellPosition){
         adjacentTilesPositions.Add(cellPosition);
         if (tilemap.HasTile(cellPosition)){
             tilemap.SetTile(cellPosition,activeTile);
         }
    }
}
