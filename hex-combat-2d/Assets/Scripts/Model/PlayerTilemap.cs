using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerTilemap : MonoBehaviour {

    public static event Action<Vector3Int, PlayerTag> TileConquered;
    public static event Action<Vector3Int, PlayerTag> TileAdjacent;
    // HashSet<Vector3Int> conqueredTilesPositions = new HashSet<Vector3Int>();
    HashSet<Vector3Int> adjacentTilesPositions = new HashSet<Vector3Int>();
    // HashSet<Vector3Int> enemyTilesPositions = new HashSet<Vector3Int>();
    public Tilemap tilemap;
    public Dictionary<Vector3Int, ArenaTile> arenaTilesPositions;
    private Vector3Int selectedPosition;
    public Vector3Int SelectedPosition { get; set; }
    private PlayerTag playerTag;
    private TileRenderer tileRenderer;

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
        UpdateAdjacentTile(cellPosition);
        // Add the adjacent Tiles to adjacentTilesPositions, so they can be clicked now
        Debug.Log("cellPosition: " + cellPosition.ToString());
        UpdateAdjacentTile(cellPosition + new Vector3Int(1, 0, 0));
        UpdateAdjacentTile(cellPosition + new Vector3Int(-1, 0, 0));
        UpdateAdjacentTile(cellPosition + new Vector3Int(0, 1, 0));
        UpdateAdjacentTile(cellPosition + new Vector3Int(0, -1, 0));

        // Vertical axis has offset, so neighbor coordinates change in dependancy of odd/even row
        if (IsRowEven(cellPosition)) {
            UpdateAdjacentTile(cellPosition + new Vector3Int(-1, -1, 0));
            UpdateAdjacentTile(cellPosition + new Vector3Int(-1, 1, 0));
        } else {
            UpdateAdjacentTile(cellPosition + new Vector3Int(1, -1, 0));
            UpdateAdjacentTile(cellPosition + new Vector3Int(1, 1, 0));
        }

    }

    private void UpdateAdjacentTile(Vector3Int cellPosition) {
        if (tilemap.HasTile(cellPosition) && arenaTilesPositions[cellPosition].IsConquered == false) {
            adjacentTilesPositions.Add(cellPosition);
            // tilemap.SetTile(cellPosition, activeTile);
            TileAdjacent?.Invoke(cellPosition, playerTag);
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
