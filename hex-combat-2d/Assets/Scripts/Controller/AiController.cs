using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AiController : MonoBehaviour {
    public Player player;
    private Tilemap tilemap;
IEnumerator timeCountEnumerator;
    public Dictionary<Vector3Int, ArenaTile> arenaTilesPositions;

    void Start(){
        tilemap = GameManager.Instance.tilemap;
        arenaTilesPositions = GameManager.Instance.arenaTilesPositions;
        player = Instantiate(player,new Vector3(20,0,0),Quaternion.identity);
        Debug.Log("Awake AiController.");

        timeCountEnumerator = RepeatWaitTimer();
        StartCoroutine(timeCountEnumerator);
    }

    IEnumerator RepeatWaitTimer() {
        while (true) {
            yield return new WaitForSeconds(0.5f);
            BuildTower();
        }
    }

    private void BuildTower(){
            System.Random rnd = new System.Random();
            int number = rnd.Next(0,player.playerTilemap.adjacentTilesPositions.Count);
            Vector3Int position = player.playerTilemap.adjacentTilesPositions[number];
            player.playerTilemap.SelectedPosition = position;
            player.createTower();
    }
}
