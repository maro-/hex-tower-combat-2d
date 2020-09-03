using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class HexTilemap : MonoBehaviour {
    private Tilemap tilemap;
    public Tile inactiveTile;
    public Tile activeTile;
    public Tile conqueredTile;
    private Vector3Int selectedPosition;
    public Vector3Int SelectedPosition{get;set;}
    public Tile enemyTile;
    Dictionary<Vector3Int, HexTile> combatTilesPositions;
    HashSet<Vector3Int> conqueredTilesPositions;
    HashSet<Vector3Int> adjacentTilesPositions;
    public TileBase previouslySelectedTile;

    public static HexTilemap Instance { get; private set; }

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.Log("Warning: multiple {} in scene!", this);
        }
        tilemap = GetComponent<Tilemap>();
        combatTilesPositions = new Dictionary<Vector3Int, HexTile>();
        conqueredTilesPositions = new HashSet<Vector3Int>();
        adjacentTilesPositions = new HashSet<Vector3Int>();
        if (tilemap == null) {
            Debug.LogError("No Tilemap on object found.");
        }

        for (int n = tilemap.cellBounds.xMin; n < tilemap.cellBounds.xMax; n++) {
            for (int p = tilemap.cellBounds.yMin; p < tilemap.cellBounds.yMax; p++) {
                Vector3Int cellPosition = (new Vector3Int(n, p, (int)tilemap.transform.position.y));
                if (tilemap.HasTile(cellPosition)) {
                    // Debug.Log("Active Tile Position: " + cellPosition);
                    combatTilesPositions.Add(cellPosition, new HexTile());
                } else {
                    // Debug.Log("Inactive Tile Position: " + cellPosition);
                }
            }
        }
    }

    public GameObject GetInstantiatedObject(Vector3Int cellPosition){
        return tilemap.GetInstantiatedObject(cellPosition);
    }

    public Vector3 GetTileWorldPosition(Vector3Int cellPosition){
        return tilemap.CellToWorld(cellPosition);
    }

    public void AddToAdjacentTiles(Vector3Int cellPosition) {
        // Add the Tile at 'cellPosition' to the adjacentTilesPositions
        adjacentTilesPositions.Add(cellPosition);

        // Add the adjacent Tiles to adjacentTilesPositions, so they can be clicked now
        Debug.Log("cellPosition: " + cellPosition.ToString());
        UpdateAdjacentTile(cellPosition + new Vector3Int(1, 0, 0));
        UpdateAdjacentTile(cellPosition + new Vector3Int(-1, 0, 0));
        UpdateAdjacentTile(cellPosition + new Vector3Int(0, 1, 0));
        UpdateAdjacentTile(cellPosition + new Vector3Int(0, -1, 0));
        UpdateAdjacentTile(cellPosition + new Vector3Int(-1, -1, 0));

        // Vertical axis has offset, so neighbor coordinates change in dependancy of odd/even row
        if (IsRowEven(cellPosition)) {
            UpdateAdjacentTile(cellPosition + new Vector3Int(-1, 1, 0));
        } else {
            UpdateAdjacentTile(cellPosition + new Vector3Int(1, 1, 0));
        }

    }

    public void AddToConqueredTiles(Vector3Int cellPosition) {
        // Add the Tile at 'cellPosition' to the conqueredTilesPositions
        conqueredTilesPositions.Add(cellPosition);
        // Change the sprite of the tile
        tilemap.SetTile(cellPosition, conqueredTile);
        // Add the adjacent Tiles to adjacentTilesPositions, so they can be clicked now
        combatTilesPositions[cellPosition].IsConquered = true;
        previouslySelectedTile = conqueredTile;
    }

    private void UpdateAdjacentTile(Vector3Int cellPosition) {
        if (tilemap.HasTile(cellPosition) && combatTilesPositions[cellPosition].IsConquered == false) {
            // TODO: Check here, if the tile on this position ist already conquered. If yes, don't check tile
            adjacentTilesPositions.Add(cellPosition);
            tilemap.SetTile(cellPosition, activeTile);
        }
    }

    private bool IsRowEven(Vector3Int cellPosition) {
        if (cellPosition.y % 2 == 0) {
            return true;
        }
        return false;
    }

    public bool IsTileInAdjacentTiles(Vector3Int cellPosition) {
        if (adjacentTilesPositions.Contains(cellPosition)) {
            return true;
        }
        return false;
    }

}
