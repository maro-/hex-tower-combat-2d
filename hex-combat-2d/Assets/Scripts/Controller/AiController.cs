using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AiController : MonoBehaviour
{
    public Player player;
    // private Tilemap tilemap;
    IEnumerator timeCountEnumerator;
    // public Dictionary<Vector3Int, ArenaTile> arenaTiles;
    // private Arena arena;


    void Awake()
    {
        Debug.Log("Awake AiController.");
    }
    void Start()
    {
        // arenaTiles = GameManager.Instance.arenaTiles;
        // arena = GameManager.Instance.arena;
        player = Instantiate(player, new Vector3(20, 0, 0), Quaternion.identity);
        // player.SelectedPosition = player.startPosition;
        // tilemap = GameManager.Instance.tilemap;
        timeCountEnumerator = RepeatWaitTimer();
        StartCoroutine(timeCountEnumerator);
    }

    IEnumerator RepeatWaitTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            BuildTower();
        }
    }

    private void BuildTower()
    {
        Vector3Int randomPosition = GridManager.Instance.GetRandomAdjacentUnconqueredTilePosition(player.startPosition);
        // player.SelectedPosition = randomPosition;
        player.buildTower(randomPosition);
    }
}
