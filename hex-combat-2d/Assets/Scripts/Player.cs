using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public PlayerTag playerTag;
    public Vector3Int startPosition;
    public GameObject turretTower;
    public PlayerTilemap playerTilemap;
    public IncomeManager incomeManager;
    public TileRenderer tileRenderer;

    void Awake(){
    }

    void Start() {
        // Debug.Log("Start Player." + playerTag);
        // incomeManager = IncomeManager.Instance; //TODO, do not use instance
        InstantiateTower(startPosition);
    }

    public void createTower() {
        if (incomeManager.GoldAmount >= 15 && !playerTilemap.arenaTilesPositions[playerTilemap.SelectedPosition].IsConquered) {
            InstantiateTower(playerTilemap.SelectedPosition);
            incomeManager.GoldAmount -= 15;
        }
    }

    private void InstantiateTower(Vector3Int cellPosition) {
        GameObject towerObject = Instantiate(turretTower,
                playerTilemap.GetTileWorldPosition(cellPosition),
                Quaternion.identity);
        // towerObject.tag = playerTag.ToString();
        towerObject.GetComponent<TowerData>().invokeOnDisable = playerTilemap.RemoveFromConqueredTiles;
        towerObject.GetComponent<TowerData>().CellPosition = cellPosition;
        towerObject.GetComponent<TowerData>().playerTag = playerTag;
        playerTilemap.AddToAdjacentTiles(cellPosition);
        playerTilemap.AddToConqueredTiles(cellPosition);
    }
}
