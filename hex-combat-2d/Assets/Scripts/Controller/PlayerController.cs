using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    // Start is called before the first frame update
    public Player player;
    private Tilemap tilemap;
    public Tile selectedTile;
    private Vector3Int previouslySelectedPosition;
    private TileBase previouslySelectedTile;
    private bool wasPreviouslySelected = false;
    public Dictionary<Vector3Int, ArenaTile> arenaTilesPositions;
    public Button buildButton;

    void Awake() {
        tilemap = GameManager.Instance.tilemap;
        arenaTilesPositions = GameManager.Instance.arenaTilesPositions;
        player = Instantiate(player, new Vector3(20, 0, 0), Quaternion.identity);
    }
    void Start() {
        TileRenderer.TilePreviouslySelected += UpdatePreviouslySelectedTile;

        buildButton = UIManager.Instance.buildButton;
        buildButton.onClick.AddListener(player.createTower);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // Construct a ray from the current click coordinates
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.back, Vector3.zero);
            // Do a Plane Raycast with your screen to world ray
            float hitPosition;
            plane.Raycast(ray, out hitPosition);
            Vector3 point = ray.GetPoint(hitPosition);
            Vector3Int cellPosition = tilemap.WorldToCell(point);
            // Debug.Log("Tile position: " + cellPosition);
            if (tilemap.HasTile(cellPosition) && player.playerTilemap.IsTileInAdjacentTiles(cellPosition)) {
                player.playerTilemap.SelectedPosition = cellPosition;
                if (wasPreviouslySelected) {
                    tilemap.SetTile(previouslySelectedPosition, previouslySelectedTile);
                }
                wasPreviouslySelected = true;
                previouslySelectedTile = tilemap.GetTile(cellPosition);
                previouslySelectedPosition = cellPosition;

                // Debug.Log(string.Format("Tile is: {0}", selectedTile.name));
                tilemap.SetTile(cellPosition, selectedTile);

            }
        }
    }

    void UpdatePreviouslySelectedTile(TileBase tileBase, PlayerTag playerTag) {
        if (this.player.playerTag == playerTag) {
            previouslySelectedTile = tileBase;
        }
    }

    void OnDestroy() {
        TileRenderer.TilePreviouslySelected -= UpdatePreviouslySelectedTile;
    }

}
