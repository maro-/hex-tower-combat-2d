using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    public Dictionary<Vector3Int, ArenaCell> arenaCells;
    private HexTilemapRenderer hexTilemapRenderer;
    private TowerBuilder towerBuilder;

    private Vector3Int? selectedPosition;
    public Vector3Int? SelectedPosition { get { return selectedPosition; } set { selectedPosition = value; } }


    System.Random randomGen;


    // Update is called once per frame
    void Awake()
    {
        InstantiateSingleInstance();
        hexTilemapRenderer = gameObject.GetComponent<HexTilemapRenderer>();
        towerBuilder = gameObject.GetComponent<TowerBuilder>();
        InstantiateArenaTiles();
        randomGen = new System.Random();
    }



    private void InstantiateSingleInstance()
    {
        Debug.Log("Awake GridManager");
        if (Instance != null && Instance != this)
        {
            Debug.Log("Warning: multiple {} in scene! Destroying this gameObejcts and re-setting Instance", this);
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void InstantiateArenaTiles()
    {
        List<Vector3Int> tilePositions = hexTilemapRenderer.getTilemapFieldPositions();
        arenaCells = new Dictionary<Vector3Int, ArenaCell>();
        foreach (Vector3Int tilePosition in tilePositions)
        {
            arenaCells.Add(tilePosition, new ArenaCell());
        }
    }

    public void updateCells(Vector3Int cellPosition, PlayerTag playerTag, TileEvent tileEvent)
    {
        // Updates the specific tile and it's neighbours depending on their status
        switch (tileEvent)
        {
            case TileEvent.CONQUERED:
                conquerArenaTile(cellPosition, playerTag);
                updateAdjacentArenaTilesAfterConquer(cellPosition, playerTag);
                break;
            case TileEvent.DEFEATED:
                defeatArenaTile(cellPosition, playerTag);
                updateAdjacentArenaTilesAfterDefeat(cellPosition, playerTag);
                break;
        }
    }

    private void conquerArenaTile(Vector3Int cellPosition, PlayerTag playerTag)
    {
        arenaCells[cellPosition].cellState = CellState.CONQUERED;
        arenaCells[cellPosition].cellStateBeforeSelection = CellState.CONQUERED;
        arenaCells[cellPosition].ConqueredByPlayerTag = playerTag;
        arenaCells[cellPosition].IsConquered = true;
        arenaCells[cellPosition].IsSelected = false;
        SelectedPosition = null; // TODO resets every time the Selected Position on conquer. bad
        hexTilemapRenderer.setTileConquered(cellPosition, playerTag);
    }

    public void selectCell(Vector3Int cellPosition, PlayerTag playerTag)
    {
        if (SelectedPosition.HasValue)
        {
            unselectCell(SelectedPosition.GetValueOrDefault(), playerTag);
        }
        // ArenaCell arenaCell = arenaCells[cellPosition];
        // if (wasPreviouslySelected)
        // {
        //     gridManager.setTileToSelected(previouslySelectedPosition, player.playerTag);
        // }
        if (!arenaCells[cellPosition].IsSelected)
        {
            arenaCells[cellPosition].cellStateBeforeSelection = arenaCells[cellPosition].cellState;
        }
        arenaCells[cellPosition].IsSelected = true;
        hexTilemapRenderer.setTileSelected(cellPosition, playerTag);
        SelectedPosition = cellPosition;
        // wasPreviouslySelected = true;
        // previouslySelectedPosition = cellPosition;

        // Debug.Log(string.Format("Tile is: {0}", selectedTile.name));
        // hexTilemapRenderer.setTileSelected(cellPosition, playerTag);
    }

    public void unselectCell(Vector3Int cellPosition, PlayerTag playerTag)
    {
        // ArenaCell arenaCell = arenaCells[cellPosition];
        // if (wasPreviouslySelected)
        // {
        //     gridManager.setTileToSelected(previouslySelectedPosition, player.playerTag);
        // }
        // if (!arenaCells[cellPosition].IsSelected)
        // {
        //     arenaCells[cellPosition].cellStateBeforeSelection = arenaCells[cellPosition].cellState;
        // }
        arenaCells[cellPosition].IsSelected = false;
        switch (arenaCells[cellPosition].cellStateBeforeSelection)
        {
            case CellState.CONQUERED:
                hexTilemapRenderer.setTileConquered(cellPosition, playerTag);
                arenaCells[cellPosition].cellStateBeforeSelection = CellState.CONQUERED;
                break;
            case CellState.FREE:
                hexTilemapRenderer.setTileFreed(cellPosition, playerTag);
                arenaCells[cellPosition].cellStateBeforeSelection = CellState.FREE;
                break;
            case CellState.NEIGHBOUR:
                hexTilemapRenderer.setTileAdjacent(cellPosition, playerTag);
                arenaCells[cellPosition].cellStateBeforeSelection = CellState.NEIGHBOUR;
                break;

        }
        // wasPreviouslySelected = true;
        // previouslySelectedTile = tilemap.GetTile(cellPosition);
        // previouslySelectedPosition = cellPosition;

        // Debug.Log(string.Format("Tile is: {0}", selectedTile.name));
        // gridManager.setTileToSelected(cellPosition, player.playerTag);
        // hexTilemapRenderer.setTileSelected(cellPosition, playerTag);
    }

    private void updateAdjacentArenaTilesAfterConquer(Vector3Int cellPosition, PlayerTag playerTag)
    {
        foreach (Vector3Int adjacentCellPosition in getAdjacentArenaCells(cellPosition))
        {
            if (!arenaCells[adjacentCellPosition].IsConquered)
            {
                //TODO Maybe handle selected field, if enemy ai player built/conquered a tower
                // TileIsAdjacent?.Invoke(adjacentCellPosition, playerTag);
                arenaCells[adjacentCellPosition].cellState = CellState.NEIGHBOUR;
                hexTilemapRenderer.setTileAdjacent(adjacentCellPosition, playerTag);
            }
            // adjacentArenaTiles.Add(adjacentCellPosition);
        }
    }

    private void defeatArenaTile(Vector3Int cellPosition, PlayerTag playerTag)
    {
        Debug.Log("ArenaTile defeated. Position: " + cellPosition + " | playerTag: " + playerTag);
        /*
        arenaCells[cellPosition].IsConquered = true;
        arenaCells[cellPosition].ConqueredByPlayerTag = playerTag;
        arenaCells[cellPosition].cellState = CellState.CONQUERED;
        arenaCells[cellPosition].IsSelected = false;
        arenaCells[cellPosition].cellStateBeforeSelection = CellState.CONQUERED;
        // if (!arenaCells[cellPosition].IsSelected)
        // {
        hexTilemapRenderer.setTileConquered(cellPosition, playerTag);
        */
        Debug.Log("isCellAnAdjacentCellOfPlayer1.: " + isCellAnAdjacentCellOfPlayer(cellPosition, playerTag));
        // TODO: which player defeats?
        arenaCells[cellPosition].IsConquered = false;
        arenaCells[cellPosition].ConqueredByPlayerTag = PlayerTag.None;
        Debug.Log("isCellAnAdjacentCellOfPlayer2: " + isCellAnAdjacentCellOfPlayer(cellPosition, playerTag));
        // if (!arenaCells[cellPosition].IsSelected)
        // {
        //     hexTilemapRenderer.setTileConquered(cellPosition, playerTag);
        // }

        if (arenaCells[cellPosition].IsSelected)
        {
            arenaCells[cellPosition].IsSelected = false;
            SelectedPosition = null;
        }

        if (isCellAnAdjacentCellOfPlayer(cellPosition, playerTag))
        {
            arenaCells[cellPosition].cellState = CellState.NEIGHBOUR;
            arenaCells[cellPosition].cellStateBeforeSelection = CellState.NEIGHBOUR;
            hexTilemapRenderer.setTileAdjacent(cellPosition, playerTag);

        }
        else
        {
            arenaCells[cellPosition].cellState = CellState.FREE;
            arenaCells[cellPosition].cellStateBeforeSelection = CellState.FREE;
            hexTilemapRenderer.setTileFreed(cellPosition, playerTag);
            // Debug.Log("Count of TileIsFree: " + TileIsFree.GetInvocationList().GetLength(0));
        }
    }

    private void updateAdjacentArenaTilesAfterDefeat(Vector3Int cellPosition, PlayerTag playerTag)
    {
        // TODO: selectedPosition gets lost, when tower is killed, can not continue building
        foreach (Vector3Int adjacentCellPosition in getAdjacentArenaCells(cellPosition))
        {
            if (!isCellConqueredByPlayer(adjacentCellPosition, playerTag))
            {
                if (isCellAnAdjacentCellOfPlayer(adjacentCellPosition, playerTag))
                {
                    // TileIsAdjacent?.Invoke(adjacentCellPosition, playerTag);
                    arenaCells[adjacentCellPosition].cellStateBeforeSelection = CellState.NEIGHBOUR;
                    if (!arenaCells[adjacentCellPosition].IsSelected)
                    {
                        arenaCells[adjacentCellPosition].cellState = CellState.NEIGHBOUR;
                        hexTilemapRenderer.setTileAdjacent(adjacentCellPosition, playerTag);
                    }
                }
                else
                {
                    arenaCells[adjacentCellPosition].cellState = CellState.FREE;
                    arenaCells[adjacentCellPosition].cellStateBeforeSelection = CellState.FREE;
                    hexTilemapRenderer.setTileFreed(adjacentCellPosition, playerTag);
                    if (arenaCells[adjacentCellPosition].IsSelected)
                    {
                        arenaCells[adjacentCellPosition].IsSelected = false;
                        SelectedPosition = null;
                    }
                }

            }
        }
    }

    public bool isCellSelectable(Vector3Int cellPosition, PlayerTag playerTag)
    {
        if (!isArenaCell(cellPosition))
        {
            return false;
        }
        bool isACellInAdjacentCells = isCellAnAdjacentCellOfPlayer(cellPosition, playerTag);
        bool isACellConqueredByPlayer = isCellConqueredByPlayer(cellPosition, playerTag);
        bool isNotAConqueredTileByOtherPlayer = isNotConqueredByOtherPlayer(cellPosition, playerTag);
        return (isACellInAdjacentCells || isACellConqueredByPlayer) && isNotAConqueredTileByOtherPlayer;
    }

    private bool isCellAnAdjacentCellOfPlayer(Vector3Int cellPosition, PlayerTag playerTag)
    {
        foreach (Vector3Int pos in getAdjacentArenaCells(cellPosition))
        {
            if (isCellConqueredByPlayer(pos, playerTag))
            {
                return true;
            }
        }
        return false;
    }

    private List<Vector3Int> getAdjacentArenaCells(Vector3Int cellPosition)
    {
        List<Vector3Int> adjacentTilePositions = new List<Vector3Int>{
            cellPosition + new Vector3Int(1, 0, 0),
            cellPosition + new Vector3Int(-1, 0, 0),
            cellPosition + new Vector3Int(0, 1, 0),
            cellPosition + new Vector3Int(0, -1, 0)};
        if (IsRowEven(cellPosition))
        {
            adjacentTilePositions.Add(cellPosition + new Vector3Int(-1, -1, 0));
            adjacentTilePositions.Add(cellPosition + new Vector3Int(-1, 1, 0));
        }
        else
        {
            adjacentTilePositions.Add(cellPosition + new Vector3Int(1, -1, 0));
            adjacentTilePositions.Add(cellPosition + new Vector3Int(1, 1, 0));
        }
        List<Vector3Int> adjacentArenaTiles = new List<Vector3Int>();
        foreach (Vector3Int adjacentTilePosition in adjacentTilePositions)
        {
            if (isArenaCell(adjacentTilePosition))
            {
                adjacentArenaTiles.Add(adjacentTilePosition);
            }
        }
        return adjacentArenaTiles;
    }

    private bool IsRowEven(Vector3Int cellPosition)
    {
        if (cellPosition.y % 2 == 0)
        {
            return true;
        }
        return false;
    }

    private bool isArenaCell(Vector3Int cellPosition)
    {
        return arenaCells.ContainsKey(cellPosition);
    }

    public bool isCellConqueredByPlayer(Vector3Int cellPosition, PlayerTag playerTag)
    {
        return arenaCells[cellPosition].IsConquered == true && arenaCells[cellPosition].ConqueredByPlayerTag == playerTag;
    }

    public bool isSelectedCellConqueredByPlayer(PlayerTag playerTag)
    {
        return arenaCells[SelectedPosition.Value].IsConquered == true && arenaCells[SelectedPosition.Value].ConqueredByPlayerTag == playerTag;
    }

    public bool isNotConqueredByOtherPlayer(Vector3Int cellPosition, PlayerTag playerTag)
    {
        return !(arenaCells[cellPosition].IsConquered == true && arenaCells[cellPosition].ConqueredByPlayerTag != playerTag);
    }

    public Vector3 GetCellInWorldPosition(Vector3Int cellPosition)
    {
        return hexTilemapRenderer.tilemap.CellToWorld(cellPosition);
    }

    public Vector3Int GetPointInCellPosition(Vector3 worldPosition)
    {
        return hexTilemapRenderer.tilemap.WorldToCell(worldPosition);
    }

    public Vector3Int GetRandomAdjacentUnconqueredTilePosition(Vector3Int cellPosition)
    {
        List<Vector3Int> adjacentCellPositions = getAdjacentArenaCells(cellPosition);
        List<Vector3Int> unconqueredCellPositions = new List<Vector3Int>();
        foreach (Vector3Int adjacentCell in adjacentCellPositions)
        {
            if (!arenaCells[adjacentCell].IsConquered)
            {
                unconqueredCellPositions.Add(adjacentCell);
            }
        }
        int r = randomGen.Next(unconqueredCellPositions.Count);
        return unconqueredCellPositions[r];
    }

    public void BuildTower(Vector3Int cellPosition, GameObject towerObject, PlayerTag playerTag)
    {
        towerBuilder.BuildTower(cellPosition, GetCellInWorldPosition(cellPosition), towerObject, playerTag);
    }

    public bool BuildTowerOnSelectedPosition(GameObject towerObject, PlayerTag playerTag)
    {
        if (SelectedPosition.HasValue)
        {
            towerBuilder.BuildTower(SelectedPosition.GetValueOrDefault(), GetCellInWorldPosition(SelectedPosition.Value), towerObject, playerTag);
            return true;
        }
        else
        {
            Debug.Log("Can not build tower. No selectedPosition for Player: " + playerTag);
            return false;
        }
    }

}
