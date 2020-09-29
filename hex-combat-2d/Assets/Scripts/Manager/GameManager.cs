using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    // public GameObject turretTower;
    // public GameObject enemyTurretTower;
    // private HexTilemap hexTilemap;
    // private IncomeManager incomeManager;
    // readonly Vector3Int startPosition = new Vector3Int(0, -2, 0);
    // readonly Vector3Int enemyStartPosition = new Vector3Int(0, 2, 0);
    // public List<Player> players = new List<Player>();
    // public Tilemap tilemap;
    public Tilemap tilemap;
    public Dictionary<Vector3Int, ArenaTile> arenaTilesPositions;


    void Awake() {
        Debug.Log("Awake GameManager");
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.Log("Warning: multiple {} in scene!", this);
        }

        arenaTilesPositions = new Dictionary<Vector3Int, ArenaTile>();
        for (int n = tilemap.cellBounds.xMin; n < tilemap.cellBounds.xMax; n++) {
            for (int p = tilemap.cellBounds.yMin; p < tilemap.cellBounds.yMax; p++) {
                Vector3Int cellPosition = (new Vector3Int(n, p, (int)tilemap.transform.position.y));
                if (tilemap.HasTile(cellPosition)) {
                    // Debug.Log("Active Tile Position: " + cellPosition);
                    arenaTilesPositions.Add(cellPosition, new ArenaTile());
                } else {
                    // Debug.Log("Inactive Tile Position: " + cellPosition);
                }
            }
        }
        
    }
    void Start() {
        // hexTilemap = HexTilemap.Instance;
        // incomeManager = IncomeManager.Instance;
        // Player player1 = new Player("player1", new Vector3Int(0, -2, 0), tilemap);
        // Player player2 = new Player("player2", new Vector3Int(0, 2, 0),tilemap);
        // players.Add(player1);
        // InstantiateTower(startPosition);
        //instantiate enemy


        // GameObject enemyTowerObject = Instantiate(enemyTurretTower, 
        //         hexTilemap.GetTileWorldPosition(enemyStartPosition), 
        //         Quaternion.identity);
        // hexTilemap.AddToEnemyTiles(enemyStartPosition);


    }

    

    
}
