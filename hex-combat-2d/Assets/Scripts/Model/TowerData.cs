using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerLevel {
    public int cost;
    public GameObject visualization;
    public GameObject bullet;
    public float attackSpeed; //fireRate;
    public float attackDamage;
    public float healthPoints;
    public float attackRange;

}


public class TowerData : MonoBehaviour {
    public List<TowerLevel> levels;
    private TowerLevel currentLevel;
    public TowerLevel CurrentLevel {
        get {
            return currentLevel;
        }
        set {
            currentLevel = value;
            int currentLevelIndex = levels.IndexOf(currentLevel);

            GameObject levelVisualization = levels[currentLevelIndex].visualization;
            for (int i = 0; i < levels.Count; i++) {
                if (levelVisualization != null) {
                    if (i == currentLevelIndex) {
                        levels[i].visualization.SetActive(true);
                    } else {
                        levels[i].visualization.SetActive(false);
                    }
                }
            }
        }
    }

    private Vector3Int cellPosition;
    public Vector3Int CellPosition {
        set {
            cellPosition = value;
            SetAdjacentTiles(value);
        }
    }

    private List<Vector3Int> adjacentCellPositions = new List<Vector3Int>();
    public List<Vector3Int> AdjacentCellPositions { get; private set; }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnEnable() {
        CurrentLevel = levels[0];
    }

    public TowerLevel GetNextLevel() {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevelIndex = levels.Count - 1;
        if (currentLevelIndex < maxLevelIndex) {
            return levels[currentLevelIndex + 1];
        } else {
            return null;
        }
    }

    public void IncreaseLevel() {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        if (currentLevelIndex < levels.Count - 1) {
            CurrentLevel = levels[currentLevelIndex + 1];
        }
    }

    private void SetAdjacentTiles(Vector3Int cellPosition) {
        adjacentCellPositions.Add(cellPosition + new Vector3Int(1, 0, 0));
        adjacentCellPositions.Add(cellPosition + new Vector3Int(-1, 0, 0));
        adjacentCellPositions.Add(cellPosition + new Vector3Int(0, 1, 0));
        adjacentCellPositions.Add(cellPosition + new Vector3Int(0, -1, 0));
        adjacentCellPositions.Add(cellPosition + new Vector3Int(-1, -1, 0));

        // Vertical axis has offset, so neighbor coordinates change in dependancy of odd/even row
        if (IsRowEven(cellPosition)) {
            adjacentCellPositions.Add(cellPosition + new Vector3Int(-1, 1, 0));
        } else {
            adjacentCellPositions.Add(cellPosition + new Vector3Int(1, 1, 0));
        }
    }

    private bool IsRowEven(Vector3Int cellPosition) {
        if (cellPosition.y % 2 == 0) {
            return true;
        }
        return false;
    }


}
