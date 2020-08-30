using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexTile : Tile {
    public Sprite inactiveSprite;
    public Sprite activeSprite;
    public Sprite conqueredSprite;
    public Sprite enemySprite;
    private bool isActive;

    public bool IsActive { get { return isActive; } set { this.sprite = activeSprite; } }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
