using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseInputController : MonoBehaviour {
    // Start is called before the first frame update
    public Tilemap tilemap;
    public Tile tile;
    TilemapController tilemapController;

    // Update is called once per frame
    void Start() {
        tilemapController = TilemapController.Instance;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // Construct a ray from the current touch coordinates
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.back, Vector3.zero);
            // Do a Plane Raycast with your screen to world ray
            float hitPosition;
            plane.Raycast(ray, out hitPosition);
            Vector3 point = ray.GetPoint(hitPosition);
            Vector3Int cellPosition = tilemap.WorldToCell(point);
            Debug.Log("Tile position: " + cellPosition);
            if (tilemap.HasTile(cellPosition) && tilemapController.isTileInAdjacentTiles(cellPosition)) {
                Debug.Log(string.Format("Tile is: {0}", tile.name));

                tilemap.SetTile(cellPosition, tile);
                tilemapController.AddToConqueredTiles(cellPosition);
                tilemapController.AddToAdjacentTiles(cellPosition);
                // tile.GetTileData(cellPosition,tilemap,)
            }
        }
    }
}
