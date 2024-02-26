using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    // private Tilemap tilemap;
    // public Tile selectedTile;
    // private Vector3Int previouslySelectedPosition;
    // private TileBase previouslySelectedTile;
    // private bool wasPreviouslySelected = false;
    // public Dictionary<Vector3Int, ArenaTile> arenaTiles;
    // private Arena arena;
    private GridManager gridManager;
    public Button buildButton;

    void Awake()
    {

        Debug.Log("Awake PlayerController.");
        player = Instantiate(player, new Vector3(20, 0, 0), Quaternion.identity);
        gridManager = GridManager.Instance;
    }
    void Start()
    {
        // tilemap = GameManager.Instance.tilemap;
        // arena = GameManager.Instance.arena;
        // arenaTiles = GameManager.Instance.arenaTiles;
        // TileRenderer.TilePreviouslySelected += UpdatePreviouslySelectedTile;

        buildButton = UIManager.Instance.buildButton;
        buildButton.onClick.AddListener(player.buildTower);
    }

    // void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         // Construct a ray from the current click coordinates
    //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //         Plane plane = new Plane(Vector3.back, Vector3.zero);
    //         // Do a Plane Raycast with your screen to world ray
    //         float hitPosition;
    //         plane.Raycast(ray, out hitPosition);
    //         Vector3 point = ray.GetPoint(hitPosition);
    //         Vector3Int cellPosition = tilemap.WorldToCell(point);
    //         // Debug.Log("Tile position: " + cellPosition);
    //         if (arena.isArenaTile(cellPosition) && arena.isTileInAdjacentTiles(cellPosition))
    //         { // TODO&& player.playerArena.isTileNotConqueredByOtherPlayerThan()) {
    //             player.SelectedPosition = cellPosition;
    //             if (wasPreviouslySelected)
    //             {
    //                 tilemap.SetTile(previouslySelectedPosition, previouslySelectedTile);
    //             }
    //             wasPreviouslySelected = true;
    //             previouslySelectedTile = tilemap.GetTile(cellPosition);
    //             previouslySelectedPosition = cellPosition;

    //             // Debug.Log(string.Format("Tile is: {0}", selectedTile.name));
    //             tilemap.SetTile(cellPosition, selectedTile);

    //         }
    //     }
    // }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3Int cellPosition = GetCellFromClickedPosition();
            if (gridManager.isCellSelectable(cellPosition, player.playerTag))
            {

                gridManager.selectCell(cellPosition, player.playerTag);
                // player.SelectedPosition = cellPosition;

            }
        }
    }

    private Vector3Int GetCellFromClickedPosition()
    {
        // Construct a ray from the current click coordinates
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.back, Vector3.zero);
        // Do a Plane Raycast with your screen to world ray
        float hitPosition;
        plane.Raycast(ray, out hitPosition);
        Vector3 point = ray.GetPoint(hitPosition);
        return gridManager.GetPointInCellPosition(point);
    }


    // void OnDestroy()
    // {
    //     TileRenderer.TilePreviouslySelected -= UpdatePreviouslySelectedTile;
    // }

}
