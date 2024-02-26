using UnityEngine;
using UnityEngine.Tilemaps;

public class ArenaCell
{
    // private Tile tile;
    // public Tile Tile { get { return tile; } set { tile = value; } }

    private PlayerTag conqueredByPlayerTag = PlayerTag.None;
    public PlayerTag ConqueredByPlayerTag { get { return conqueredByPlayerTag; } set { conqueredByPlayerTag = value; } }
    // public Sprite inactiveSprite;
    // public Sprite activeSprite;
    // public Sprite conqueredSprite;
    // public Sprite enemySprite;
    // private bool isActive;
    // public bool IsActive { get { return isActive; } set { isActive = value; } }
    private bool isConquered = false;
    public bool IsConquered { get { return isConquered; } set { isConquered = value; } }

    private bool isSelected = false;
    public bool IsSelected { get { return isSelected; } set { isSelected = value; } }
    public CellState cellStateBeforeSelection = CellState.FREE;
    public CellState cellState = CellState.FREE;
    // private bool isEnemy;
    // public bool IsEnemy { get { return isEnemy; } set { isEnemy = value; } }
    // private Vector3Int cellPosition;
    // public Vector3Int CellPosition { get { return cellPosition; } set { cellPosition = value; } }
    // private int activeAdjacentsTileCount=0;
    // public int ActiveAdjacentsTileCount { get { return activeAdjacentsTileCount; } set { activeAdjacentsTileCount = value; } }
}
