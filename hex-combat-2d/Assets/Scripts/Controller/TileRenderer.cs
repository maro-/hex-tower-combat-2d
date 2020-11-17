using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRenderer : MonoBehaviour {
    public static event Action<TileBase, PlayerTag> TilePreviouslySelected;
    public Tile inactiveTile;
    public Tile activeTile;
    public Tile conqueredTile;
    private Tilemap tilemap;
    PlayerTag playerTag;
    public Dictionary<Vector3Int, ArenaTile> arenaTilesPositions;

    void Awake() {
        tilemap = GameManager.Instance.tilemap;
        playerTag = gameObject.GetComponentInParent<Player>().playerTag;
        arenaTilesPositions = GameManager.Instance.arenaTilesPositions;
        PlayerTilemap.TileConquered += TileConquered;
        PlayerTilemap.TileAdjacent += TileAdjacent;
        PlayerTilemap.TileUnadjacent += TileUnadjacent;
        PlayerTilemap.TileFree += TileFree;
    }

    void Start() {

    }

    public void TileConquered(Vector3Int cellPosition, PlayerTag playerTag) {
        if (this.playerTag == playerTag) {
            tilemap.SetTile(cellPosition, conqueredTile);
            TilePreviouslySelected?.Invoke(conqueredTile, playerTag);
        }
    }

    public void TileAdjacent(Vector3Int cellPosition, PlayerTag playerTag) {
        if (this.playerTag == playerTag) {
            if (activeTile != null) {
                tilemap.SetTile(cellPosition, activeTile);
            }
        }
    }

    public void TileUnadjacent(Vector3Int cellPosition, PlayerTag playerTag) {
        if (this.playerTag == playerTag) {
            // if (arenaTilesPositions[cellPosition].ActiveAdjacentsTileCount == 0) {
                tilemap.SetTile(cellPosition, inactiveTile);
            // }
        }
    }


    public void TileFree(Vector3Int cellPosition, PlayerTag playerTag) {
        if (this.playerTag == playerTag) {
            if (arenaTilesPositions[cellPosition].ActiveAdjacentsTileCount == 0) {
                tilemap.SetTile(cellPosition, inactiveTile);
            } else {
                tilemap.SetTile(cellPosition, activeTile);
            }
        }
    }

    void OnDestroy() {
        PlayerTilemap.TileConquered -= TileConquered;
        PlayerTilemap.TileAdjacent -= TileAdjacent;
        PlayerTilemap.TileUnadjacent -= TileUnadjacent;
        PlayerTilemap.TileFree -= TileFree;
    }
}
