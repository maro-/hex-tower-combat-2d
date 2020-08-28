using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseInputController : MonoBehaviour
{
    // Start is called before the first frame update
    public Tilemap tilemap;
    public Tile tile;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Construct a ray from the current touch coordinates
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.back, Vector3.zero);
            // Do a Plane Raycast with your screen to world ray
            float hitDist;
            plane.Raycast(ray2, out hitDist);

            // If you aimed towards this infinite Plane, it hit
            var point = ray2.GetPoint(hitDist);
            Debug.Log("Point: " + point);

            Vector3Int cellPosition = tilemap.WorldToCell(point);
            TileBase tileBase = tilemap.GetTile(cellPosition);
            Debug.Log("CellPosition: " + cellPosition);
            if (tileBase != null)
            {
                Debug.Log("Tile sprite: " + tilemap.GetSprite(cellPosition));
                tilemap.SetTile(cellPosition,tile);
                // tile.GetTileData(cellPosition,tilemap,)
            }
        }
    }
}
