using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexTile {
    private Tile tile;
    public Tile Tile { get; set; }
    public Sprite inactiveSprite;
    public Sprite activeSprite;
    public Sprite conqueredSprite;
    public Sprite enemySprite;
    private bool isActive;
    public bool IsActive { get { return isActive; } set { isActive = value; } }
    private bool isConquered;
    public bool IsConquered { get { return isConquered; } set { isConquered = value; } }
    private Vector3Int cellPosition;
    public Vector3Int CellPosition { get { return cellPosition; } set { cellPosition = value; } }
    // Start is called before the first frame update
    // public HexTile(Vector3Int cellPosition) {
    //     CellPosition = cellPosition;
    // }
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
