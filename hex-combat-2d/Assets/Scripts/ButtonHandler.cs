using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour {
    public GameObject turretTower;

    private HexTilemap hexTilemap;

    void Start() {
        hexTilemap = HexTilemap.Instance;
    }
    public void createTower() {
        GameObject gameobject = Instantiate(turretTower,
            hexTilemap.getTileWorldPosition(hexTilemap.SelectedPosition),
            Quaternion.identity);
        // Hexagon hex = gameManager.GetComponent<GameManager>().getSelectedHexagon();
        //set Hexfield to hasBilding = true
        // hex.hasBuilding = true;
        //set adjacent Hexagons to isadjacent = true 
        hexTilemap.AddToAdjacentTiles(hexTilemap.SelectedPosition);
        hexTilemap.AddToConqueredTiles(hexTilemap.SelectedPosition);
    }
}
