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
    public bool IsActive { get { return isActive; } set { isActive = value; tile.sprite = activeSprite; } }
    private bool cellPosition;
    public bool CellPosition { get { return cellPosition; } set { cellPosition = value; } }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
