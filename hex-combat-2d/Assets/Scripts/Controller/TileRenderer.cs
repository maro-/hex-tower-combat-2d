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

    void Awake() {
        tilemap = GameManager.Instance.tilemap;
        playerTag = gameObject.GetComponentInParent<Player>().playerTag;
        PlayerTilemap.TileConquered += TileConquered;
        PlayerTilemap.TileAdjacent += TileAdjacent;
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

    void OnDestroy() {
        PlayerTilemap.TileConquered -= TileConquered;
        PlayerTilemap.TileAdjacent -= TileAdjacent;
    }
}
