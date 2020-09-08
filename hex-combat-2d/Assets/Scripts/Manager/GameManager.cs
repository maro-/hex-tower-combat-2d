using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    public GameObject turretTower;
    private HexTilemap hexTilemap;
    private IncomeManager incomeManager;
    readonly Vector3Int startPosition = new Vector3Int(0, -2, 0);
    readonly Vector3Int enemyStartPosition = new Vector3Int(0, 2, 0);


    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.Log("Warning: multiple {} in scene!", this);
        }
    }
    void Start() {
        hexTilemap = HexTilemap.Instance;
        incomeManager = IncomeManager.Instance;
        InstantiateTower(startPosition);
        //instantiate enemy
        GameObject towerObject = Instantiate(turretTower, 
                hexTilemap.GetTileWorldPosition(enemyStartPosition), 
                Quaternion.identity);
        hexTilemap.AddToEnemyTiles(enemyStartPosition);
        

    }

    public void createTower() {
        if (incomeManager.GoldAmount >= 15) {
            InstantiateTower(hexTilemap.SelectedPosition);
            incomeManager.GoldAmount -= 15;
        }
    }

    private void InstantiateTower(Vector3Int cellPosition) {
        GameObject towerObject = Instantiate(turretTower, 
                hexTilemap.GetTileWorldPosition(cellPosition), 
                Quaternion.identity);
        towerObject.GetComponent<TowerData>().CellPosition = cellPosition;
        hexTilemap.AddToAdjacentTiles(cellPosition);
        hexTilemap.AddToConqueredTiles(cellPosition);
    }
}
