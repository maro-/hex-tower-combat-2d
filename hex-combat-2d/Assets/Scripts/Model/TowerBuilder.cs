using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



public class TowerBuilder : MonoBehaviour
{

    // public void createTower()
    // {
    //     if (incomeManager.GoldAmount >= 15 && !playerArena.isTileConquered(playerArena.SelectedPosition))
    //     {
    //         InstantiateTower(playerArena.SelectedPosition);
    //         incomeManager.GoldAmount -= 15;
    //     }
    // }


    public void BuildTower(Vector3Int cellPosition, Vector3 worldPosition, GameObject towerObject, PlayerTag playerTag)
    {
        GameObject iTowerObject = Instantiate(towerObject, worldPosition, Quaternion.identity);
        TowerData towerData = iTowerObject.GetComponent<TowerData>();
        towerData.playerTag = playerTag;
        towerData.CellPosition = cellPosition;
        towerData.invokeOnDestroy = GridManager.Instance.updateCells;
        GridManager.Instance.updateCells(cellPosition, playerTag, TileEvent.CONQUERED);
    }


}