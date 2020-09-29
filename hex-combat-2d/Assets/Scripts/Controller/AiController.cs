using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AiController : MonoBehaviour {
    public Player player;
    private Tilemap tilemap;

    public Dictionary<Vector3Int, ArenaTile> arenaTilesPositions;

    void Start(){
        tilemap = GameManager.Instance.tilemap;
        arenaTilesPositions = GameManager.Instance.arenaTilesPositions;
        player = Instantiate(player,new Vector3(20,0,0),Quaternion.identity);
        Debug.Log("Awake AiController.");

    }

}
