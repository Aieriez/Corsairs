using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public static Respawn spawn;
    [SerializeField] private List<GameObject> enemies;
    public List<Transform> respawnPoints;
    [SerializeField] private bool startSpanwn;
    [SerializeField] private float timeBetweenSpanwn;
    [SerializeField] private WaitForSeconds respawnTimer = new WaitForSeconds(5);

    // Start is called before the first frame update
    void Start()
    {
        timeBetweenSpanwn = UIManager.instance.spawnEnemiesTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenSpanwn -= Time.deltaTime; 
        if (timeBetweenSpanwn <= 0)
        {
            EnemySpawn();
            timeBetweenSpanwn = UIManager.instance.spawnEnemiesTime;
        } 
        
        
    }

    private void EnemySpawn()
    {
        GameObject spawn = GameObject.Instantiate(enemies[Random.Range(0,enemies.Count)], respawnPoints[Random.Range(0,respawnPoints.Count)]);            
    }
    
}
