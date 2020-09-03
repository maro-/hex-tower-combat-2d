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
        hexTilemap.AddToAdjacentTiles(startPosition);
        hexTilemap.AddToConqueredTiles(startPosition);
        GameObject gameobject = Instantiate(turretTower,
                hexTilemap.GetTileWorldPosition(startPosition),
                Quaternion.identity);
    }

    public void createTower() {
        if (incomeManager.GoldAmount >= 15) {
            GameObject gameobject = Instantiate(turretTower,
                hexTilemap.GetTileWorldPosition(hexTilemap.SelectedPosition),
                Quaternion.identity);
            hexTilemap.AddToAdjacentTiles(hexTilemap.SelectedPosition);
            hexTilemap.AddToConqueredTiles(hexTilemap.SelectedPosition);
            incomeManager.GoldAmount -= 15;
        }
    }
}
