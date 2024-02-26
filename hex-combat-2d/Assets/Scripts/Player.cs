using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerTag playerTag;
    public Vector3Int startPosition;
    public GameObject turretTower;
    // private Arena arena;
    public IncomeManager incomeManager;
    // public TileRenderer tileRenderer;
    // public TowerBuilder towerBuilder;

    // private Vector3Int selectedPosition;
    // public Vector3Int SelectedPosition { get; set; }
    void Awake()
    {
    }

    void Start()
    {
        // arena = GameManager.Instance.arena;
        Debug.Log("Start Player. and build tower on position: " + startPosition + "|playerTag:" + playerTag + "|turretTower:" + turretTower);
        // incomeManager = IncomeManager.Instance; //TODO, do not use instance
        GridManager.Instance.SelectedPosition = startPosition;
        // towerBuilder.InstantiateTower(turretTower, playerTag);
        GridManager.Instance.BuildTower(startPosition, turretTower, playerTag);
    }

    public void buildTower()
    {
        if (incomeManager.GoldAmount >= 15 && !GridManager.Instance.isSelectedCellConqueredByPlayer(playerTag))
        {
            bool successful = GridManager.Instance.BuildTowerOnSelectedPosition(turretTower, playerTag);
            if (successful)
            {
                incomeManager.GoldAmount -= 15;
            }
        }
    }

    public void buildTower(Vector3Int cellPosition)
    {
        if (incomeManager.GoldAmount >= 15 && !GridManager.Instance.isCellConqueredByPlayer(cellPosition, playerTag))
        {
            GridManager.Instance.BuildTower(cellPosition, turretTower, playerTag);
            incomeManager.GoldAmount -= 15;
        }
    }

    // private void InstantiateTower(Vector3Int cellPosition)
    // {
    //     GameObject towerObject = Instantiate(turretTower, playerArena.GetTileWorldPosition(cellPosition), Quaternion.identity);
    //     // towerObject.tag = playerTag.ToString();
    //     TowerData towerData = towerObject.GetComponent<TowerData>();
    //     towerData.invokeOnDestroy = playerArena.updateTiles;
    //     towerData.CellPosition = cellPosition;
    //     towerData.playerTag = playerTag;
    //     playerArena.updateTiles(cellPosition, playerTag, TileEvent.CONQUERED);
    // }
}
