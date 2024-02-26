using System;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Arena : MonoBehaviour
{
    public event Action<Vector3Int, PlayerTag> TileIsConquered;
    public event Action<Vector3Int, PlayerTag, bool> TileIsDefeated;
    public event Action<Vector3Int, PlayerTag> TileIsAdjacent;
    public event Action<Vector3Int, PlayerTag> TileUnadjacent;
    public event Action<Vector3Int, PlayerTag> TileIsFree;

    public HashSet<Vector3Int> adjacentArenaTiles = new HashSet<Vector3Int>();
    // public static Dictionary<Vector3Int, ArenaTile> arenaTiles;
    // private Vector3Int selectedPosition;
    // public Vector3Int SelectedPosition { get; set; }
    // private PlayerTag _playerTag;



    void Awake()
    {
        // tilemap = GameObject.FindObjectOfType<Tilemap>();
        // _playerTag = GetComponentInParent<Player>().playerTag;
        // arenaTiles = FindObjectOfType<GameManager>().arenaTiles;
        // if (arenaTiles == null){
        //     Debug.LogError("arenaTiles is null");
        //     arenaTiles = GameManager.Instance.arenaTiles;
        // }
    }

    void Start()
    {
        // tilemap = GameManager.Instance.tilemap;
    }

    // public bool isTileInAdjacentTiles(Vector3Int cellPosition)
    // {
    //     return adjacentArenaTiles.Contains(cellPosition);
    // }

    // // TODO: should the Arena be reused, how to use playerTag??
    // public void updateTiles(Vector3Int cellPosition, PlayerTag playerTag, TileEvent tileEvent)
    // {
    //     // Updates the specific tile and it's neighbours depending on their status
    //     switch (tileEvent)
    //     {
    //         case TileEvent.CONQUERED:
    //             conquerArenaTile(cellPosition, playerTag);
    //             updateAdjacentArenaTilesAfterConquer(cellPosition, playerTag);
    //             break;
    //         case TileEvent.DEFEATED:
    //             defeatArenaTile(cellPosition, playerTag);
    //             updateAdjacentArenaTilesAfterDefeat(cellPosition, playerTag);
    //             break;
    //     }
    // }

    // private void updateAdjacentArenaTilesAfterConquer(Vector3Int cellPosition, PlayerTag playerTag)
    // {
    //     foreach (Vector3Int adjacentCellPosition in getAdjacentArenaTiles(cellPosition))
    //     {
    //         if (!isTileConquered(adjacentCellPosition))
    //         {
    //             TileIsAdjacent?.Invoke(adjacentCellPosition, playerTag);
    //         }
    //         adjacentArenaTiles.Add(adjacentCellPosition);
    //     }
    // }


    // private void updateAdjacentArenaTilesAfterDefeat(Vector3Int cellPosition, PlayerTag playerTag)
    // {
    //     foreach (Vector3Int adjacentCellPosition in getAdjacentArenaTiles(cellPosition))
    //     {
    //         if (!isTileConquered(adjacentCellPosition))
    //         {
    //             if (hasAdjacentConqueredTilesOfPlayer(adjacentCellPosition, playerTag))
    //             {
    //                 TileIsAdjacent?.Invoke(adjacentCellPosition, playerTag);
    //             }
    //             else
    //             {
    //                 adjacentArenaTiles.Remove(adjacentCellPosition);
    //                 TileIsFree?.Invoke(adjacentCellPosition, playerTag);
    //             }

    //         }
    //     }
    // }

    // private void defeatArenaTile(Vector3Int cellPosition, PlayerTag playerTag)
    // {
    //     GameManager.Instance.arenaTiles[cellPosition].IsConquered = false;
    //     GameManager.Instance.arenaTiles[cellPosition].ConqueredByPlayerTag = PlayerTag.None;
    //     if (hasAdjacentConqueredTilesOfPlayer(cellPosition, playerTag))
    //     {
    //         // TileIsDefeated?.Invoke(cellPosition, _playerTag, false);
    //         TileIsAdjacent?.Invoke(cellPosition, playerTag);
    //         Debug.Log("Count of TileIsFree: "+ TileIsFree.GetInvocationList().GetLength(0) );

    //     }
    //     else
    //     {
    //         adjacentArenaTiles.Remove(cellPosition);
    //         Debug.Log("Count of TileIsFree: "+ TileIsFree.GetInvocationList().GetLength(0) );
    //         TileIsFree?.Invoke(cellPosition, playerTag);
    //     }
    // }

    // private void conquerArenaTile(Vector3Int cellPosition, PlayerTag playerTag)
    // {
    //     GameManager.Instance.arenaTiles[cellPosition].IsConquered = true;
    //     GameManager.Instance.arenaTiles[cellPosition].ConqueredByPlayerTag = playerTag;
    //     adjacentArenaTiles.Add(cellPosition);
    //     Debug.Log("Arena Tile conquered for player " + playerTag);
    //     Debug.Log("Count of TileIsConquered: "+ TileIsConquered.GetInvocationList().GetLength(0) );
    //     TileIsConquered?.Invoke(cellPosition, playerTag);
    // }

    // public bool isTileConquered(Vector3Int cellPosition)
    // {
    //     return GameManager.Instance.arenaTiles[cellPosition].IsConquered;
    // }

    // public bool isArenaTile(Vector3Int cellPosition)
    // {
    //     // Debug.Log("cellPosition "+cellPosition+" contains key? "+GameManager.Instance.arenaTiles.ContainsKey(cellPosition));
    //     return GameManager.Instance.arenaTiles.ContainsKey(cellPosition);
    // }

    // public Boolean hasAdjacentConqueredTilesOfPlayer(Vector3Int cellPosition, PlayerTag playerTag)
    // {
    //     foreach (Vector3Int pos in getAdjacentArenaTiles(cellPosition))
    //     {
    //         if (isConqueredByPlayer(pos, playerTag))
    //         {
    //             return true;
    //         }
    //     }
    //     return false;
    // }

    // private bool isConqueredByPlayer(Vector3Int cellPosition, PlayerTag playerTag)
    // {
    //     return GameManager.Instance.arenaTiles[cellPosition].IsConquered == true && GameManager.Instance.arenaTiles[cellPosition].ConqueredByPlayerTag == playerTag;
    // }

    // private bool IsRowEven(Vector3Int cellPosition)
    // {
    //     if (cellPosition.y % 2 == 0)
    //     {
    //         return true;
    //     }
    //     return false;
    // }

    // public bool IsTileInAdjacentArenaTiles(Vector3Int cellPosition)
    // {
    //     if (adjacentArenaTiles.Contains(cellPosition))
    //     {
    //         return true;
    //     }
    //     return false;
    // }

    // public GameObject GetInstantiatedObject(Vector3Int cellPosition)
    // {
    //     return tilemap.GetInstantiatedObject(cellPosition);
    // }

    // public Vector3 GetTileWorldPosition(Vector3Int cellPosition)
    // {
    //     return tilemap.CellToWorld(cellPosition);
    // }

    // private List<Vector3Int> getAdjacentArenaTiles(Vector3Int cellPosition)
    // {
    //     List<Vector3Int> adjacentTilePositions = new List<Vector3Int>{
    //         cellPosition + new Vector3Int(1, 0, 0),
    //         cellPosition + new Vector3Int(-1, 0, 0),
    //         cellPosition + new Vector3Int(0, 1, 0),
    //         cellPosition + new Vector3Int(0, -1, 0)};
    //     if (IsRowEven(cellPosition))
    //     {
    //         adjacentTilePositions.Add(cellPosition + new Vector3Int(-1, -1, 0));
    //         adjacentTilePositions.Add(cellPosition + new Vector3Int(-1, 1, 0));
    //     }
    //     else
    //     {
    //         adjacentTilePositions.Add(cellPosition + new Vector3Int(1, -1, 0));
    //         adjacentTilePositions.Add(cellPosition + new Vector3Int(1, 1, 0));
    //     }
    //     List<Vector3Int> adjacentArenaTiles = new List<Vector3Int>();
    //     foreach (Vector3Int adjacentTilePosition in adjacentTilePositions)
    //     {
    //         if (isArenaTile(adjacentTilePosition))
    //         {
    //             adjacentArenaTiles.Add(adjacentTilePosition);
    //         }
    //     }
    //     return adjacentArenaTiles;
    // }


}
