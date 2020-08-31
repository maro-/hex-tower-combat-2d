using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour {
    
    
    readonly Vector3Int startPosition = new Vector3Int(0, -2, 0);
    private HexTilemap hexTilemap;
    public static TilemapController Instance { get; private set; }

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.Log("Warning: multiple {} in scene!", this);
        }
    }

    void Start() {
        hexTilemap = HexTilemap.Instance;
        hexTilemap.AddToAdjacentTiles(startPosition);
        hexTilemap.AddToConqueredTiles(startPosition);
    }

    // Update is called once per frame
    void Update() {

    }


    
}
