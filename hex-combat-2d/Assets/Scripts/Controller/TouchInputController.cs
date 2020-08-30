using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TouchInputController : MonoBehaviour {
    public Material newMaterialRef;
    public Text textLabel;
    public GameObject gameManager;
    public Grid grid;
    public Tilemap tileMap;
    void Start() {
        //tileMap = transform.GetComponentInParent<Tilemap>();

    }

    void Update() {
        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Began || Input.GetMouseButtonDown(0)) {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane plane = new Plane(Vector3.back, Vector3.zero);
                // Do a Plane Raycast with your screen to world ray
                float hitPosition;
                plane.Raycast(ray, out hitPosition);
                Vector3 point = ray.GetPoint(hitPosition);
                Vector3Int cellPosition = tileMap.WorldToCell(point);
                TileBase tile = tileMap.GetTile(cellPosition);
                if (tileMap.HasTile(cellPosition)) {
                    Debug.Log(string.Format("Tile is: {0}", tile.name));
                    //Debug.Log("Tile sprite: " + tileMap.GetSprite(cellPosition));
                }
                // RaycastHit hit;

                // if (Physics.Raycast(ray, out hit))
                // {
                //     Vector3Int cellPosition = tilemap.WorldToCell(hit.transform.position);
                //     TileBase tile = tilemap.GetTile(cellPosition);
                //     Debug.Log(string.Format("Tile is: {0}", tile.name));
                //     Debug.Log("Tile sprite: " + tilemap.GetSprite(cellPosition));
                // Grid grid = hit.tran.sform.gameObject.GetComponent<Grid>();

                // Hexagon hex = hit.transform.gameObject.GetComponent<Hexagon>();
                // if (hex.isActive)
                // {
                //     gameManager.GetComponent<GameManager>().setSelectedHexagon(hex);

                //     Debug.Log(hit.transform.name);
                //     Renderer renderer = hit.transform.gameObject.GetComponent<Renderer>();
                //     if (renderer != null)
                //     {
                //         if (renderer.material.color == Color.red)
                //         {
                //             renderer.material.color = Color.white;

                //         }
                //         else
                //         {
                //             renderer.material.color = Color.red;
                //             this.textLabel.text = hit.transform.name;
                //         }
                //     }
                // }
            }
        }
    }


    void OnGUI() {
        //GUI.Label(new Rect(0, 0, 200, 100), "Anzahl der Touches: " + Input.touches.Length.ToString() + " fingerPos: " + fingerPos);
    }
}
