using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TowerData))]
public class Attack : MonoBehaviour {
    public List<Vector3Int> adjacentEnemyPositions;
    public List<GameObject> enemiesInRange;
    private float lastShotTime;
    private TowerData towerData;
    public HexTilemap hexTilemap;
    GameObject target = null;


    // Start is called before the first frame update
    void Start() {
        hexTilemap = HexTilemap.Instance;
        lastShotTime = Time.time;
        towerData = gameObject.GetComponentInChildren<TowerData>();
        adjacentEnemyPositions = towerData.AdjacentCellPositions;
        enemiesInRange = new List<GameObject>();

    }

    // Update is called once per frame
    void Update() {
        // foreach(Vector3Int cellPosition in adjacentEnemyPositions)
        //  if (hexTilemap.combatTilesPositions[cellPosition].IsEnemy){
        //      //set attackingEnemy=true
        //      // if attackingEnemy = true, then check if timer cooled down and another attack can proceed
        //      // else check other enemy to set targetEnemy true
        //  }

        // 1
        if (target == null) {
            foreach (GameObject enemy in enemiesInRange) {
                if (target == null) {
                    target = enemy;
                }
            }
        }
        // float minimalEnemyDistance = float.MaxValue;
        // foreach (GameObject enemy in enemiesInRange)
        // {
        //     float distanceToGoal = enemy.GetComponent<MoveEnemy>().DistanceToGoal();
        //     if (distanceToGoal < minimalEnemyDistance)
        //     {
        //         target = enemy;
        //         minimalEnemyDistance = distanceToGoal;
        //     }
        // }
        // 2
        if (target != null) {
            if (Time.time - lastShotTime > towerData.CurrentLevel.attackSpeed) {
                Shoot(target.GetComponent<Collider2D>());
                lastShotTime = Time.time;
            }
            // 3
            Vector3 direction = gameObject.transform.position - target.transform.position;
            gameObject.transform.rotation = Quaternion.AngleAxis(
                Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI,
                new Vector3(0, 0, 1));
        }
    }

    // 1
    void OnEnemyDestroy(GameObject enemy) {
        enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("OnTriggerEnter");
        // 2
        if (other.gameObject.tag.Equals("Enemy")) {
            Debug.Log("enemy entered");
            enemiesInRange.Add(other.gameObject);
            EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate += OnEnemyDestroy;
        }
    }
    // 3
    void OnTriggerExit2D(Collider2D other) {
        Debug.Log("OnTriggerExit");
        if (other.gameObject.tag.Equals("Enemy")) {
            Debug.Log("enemy exited");
            enemiesInRange.Remove(other.gameObject);
            EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }

    void Shoot(Collider2D target)
    {
        Debug.Log("shoot");
        GameObject bulletPrefab = towerData.CurrentLevel.bullet;
        // 1 
        Vector3 startPosition = gameObject.transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = bulletPrefab.transform.position.z;
        targetPosition.z = bulletPrefab.transform.position.z;

        // 2 
        GameObject newBullet = (GameObject)Instantiate(bulletPrefab);
        newBullet.transform.position = startPosition;
        BulletBehaviour bulletComp = newBullet.GetComponent<BulletBehaviour>();
        bulletComp.target = target.gameObject;
        bulletComp.startPosition = startPosition;
        bulletComp.targetPosition = targetPosition;

        // 3 
        // Animator animator = towerData.CurrentLevel.visualization.GetComponent<Animator>();
        // animator.SetTrigger("fireShot");
        // AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        // audioSource.PlayOneShot(audioSource.clip);
    }

}
