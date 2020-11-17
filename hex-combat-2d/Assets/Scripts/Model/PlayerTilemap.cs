using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerTilemap : MonoBehaviour {

    public static event Action<Vector3Int, PlayerTag> TileConquered;
    public static event Action<Vector3Int, PlayerTag> TileAdjacent;
    public static event Action<Vector3Int, PlayerTag> TileUnadjacent;
    public static event Action<Vector3Int, PlayerTag> TileFree;
    public List<Vector3Int> adjacentTilesPositions = new List<Vector3Int>();
    // HashSet<Vector3Int> enemyTilesPositions = new HashSet<Vector3Int>();
    public Tilemap tilemap;
    public Dictionary<Vector3Int, ArenaTile> arenaTilesPositions;
    private Vector3Int selectedPosition;
    public Vector3Int SelectedPosition { get; set; }
    private PlayerTag playerTag;
    public TileRenderer tileRenderer;

    void Awake() {
        tilemap = GameManager.Instance.tilemap;
        arenaTilesPositions = GameManager.Instance.arenaTilesPositions;
        playerTag = GetComponentInParent<Player>().playerTag;
        tileRenderer = GetComponentInParent<Player>().tileRenderer;
    }

    public void AddToConqueredTiles(Vector3Int cellPosition) {
        // Add the Tile at 'cellPosition' to the conqueredTilesPositions
        // conqueredTilesPositions.Add(cellPosition);
        // Change the sprite of the tile
        TileConquered?.Invoke(cellPosition, playerTag);
        // Add the adjacent Tiles to adjacentTilesPositions, so they can be clicked now
        arenaTilesPositions[cellPosition].IsConquered = true;
        arenaTilesPositions[cellPosition].ConqueredByPlayerTag = playerTag;
    }

    public void RemoveFromConqueredTiles(Vector3Int cellPosition, PlayerTag playerTagParameter) {
        // Add the Tile at 'cellPosition' to the conqueredTilesPositions
        // conqueredTilesPositions.Add(cellPosition);
        // Change the sprite of the tile
        if (playerTag == playerTagParameter) {
            TileFree?.Invoke(cellPosition, playerTag);
            // Add the adjacent Tiles to adjacentTilesPositions, so they can be clicked now
            arenaTilesPositions[cellPosition].IsConquered = false;
            arenaTilesPositions[cellPosition].ConqueredByPlayerTag = PlayerTag.None;
            RemoveFromAdjacentTiles(cellPosition);}
        // } else { tileRenderer.TileFree(cellPosition, playerTagParameter); }
    }

    // public void AddToEnemyTiles(Vector3Int cellPosition) {
    //     // Add the Tile at 'cellPosition' to the conqueredTilesPositions
    //     enemyTilesPositions.Add(cellPosition);
    //     // Change the sprite of the tile
    //     tilemap.SetTile(cellPosition, enemyTile);
    //     // Add the adjacent Tiles to adjacentTilesPositions, so they can be clicked now
    //     arenaTilesPositions[cellPosition].IsEnemy = true;
    // }

    public void AddToAdjacentTiles(Vector3Int cellPosition) {
        // Add the Tile at 'cellPosition' to the adjacentTilesPositions
        UpdateAdjacentTile(cellPosition, true);
        // Add the adjacent Tiles to adjacentTilesPositions, so they can be clicked now
        Debug.Log("cellPosition: " + cellPosition.ToString());
        UpdateAdjacentTile(cellPosition + new Vector3Int(1, 0, 0), true);
        UpdateAdjacentTile(cellPosition + new Vector3Int(-1, 0, 0), true);
        UpdateAdjacentTile(cellPosition + new Vector3Int(0, 1, 0), true);
        UpdateAdjacentTile(cellPosition + new Vector3Int(0, -1, 0), true);

        // Vertical axis has offset, so neighbor coordinates change in dependancy of odd/even row
        if (IsRowEven(cellPosition)) {
            UpdateAdjacentTile(cellPosition + new Vector3Int(-1, -1, 0), true);
            UpdateAdjacentTile(cellPosition + new Vector3Int(-1, 1, 0), true);
        } else {
            UpdateAdjacentTile(cellPosition + new Vector3Int(1, -1, 0), true);
            UpdateAdjacentTile(cellPosition + new Vector3Int(1, 1, 0), true);
        }

    }

    public void RemoveFromAdjacentTiles(Vector3Int cellPosition) {
        // Add the Tile at 'cellPosition' to the adjacentTilesPositions
        // UpdateAdjacentTile(cellPosition, false);
        // Add the adjacent Tiles to adjacentTilesPositions, so they can be clicked now
        Debug.Log("cellPosition: " + cellPosition.ToString());
        UpdateAdjacentTile(cellPosition + new Vector3Int(1, 0, 0), false);
        UpdateAdjacentTile(cellPosition + new Vector3Int(-1, 0, 0), false);
        UpdateAdjacentTile(cellPosition + new Vector3Int(0, 1, 0), false);
        UpdateAdjacentTile(cellPosition + new Vector3Int(0, -1, 0), false);

        // Vertical axis has offset, so neighbor coordinates change in dependancy of odd/even row
        if (IsRowEven(cellPosition)) {
            UpdateAdjacentTile(cellPosition + new Vector3Int(-1, -1, 0), false);
            UpdateAdjacentTile(cellPosition + new Vector3Int(-1, 1, 0), false);
        } else {
            UpdateAdjacentTile(cellPosition + new Vector3Int(1, -1, 0), false);
            UpdateAdjacentTile(cellPosition + new Vector3Int(1, 1, 0), false);
        }

    }

    private void UpdateAdjacentTile(Vector3Int cellPosition, bool addActiveAdjacent) {
        if (tilemap.HasTile(cellPosition) && arenaTilesPositions[cellPosition].IsConquered == false 
            && addActiveAdjacent == true) {
            adjacentTilesPositions.Add(cellPosition);
            // tilemap.SetTile(cellPosition, activeTile);
            TileAdjacent?.Invoke(cellPosition, playerTag);
        } else if (tilemap.HasTile(cellPosition) && addActiveAdjacent == false) {
            adjacentTilesPositions.Remove(cellPosition);
            TileUnadjacent?.Invoke(cellPosition, playerTag);
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

    public GameObject GetInstantiatedObject(Vector3Int cellPosition) {
        return tilemap.GetInstantiatedObject(cellPosition);
    }

    public Vector3 GetTileWorldPosition(Vector3Int cellPosition) {
        return tilemap.CellToWorld(cellPosition);
    }
}
