using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private AIPath aIPath;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float timeBetweenShots = 2f;
    private float shooterCounter = 0;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] internal LifeManager lifeManager;
    [SerializeField] private MaterialTintColor materialTintColor;
    [SerializeField] private Color damageColor = Color.white;
    [SerializeField] private float distanceBetween = 10;
    [SerializeField] private int power = 1;
    [SerializeField] private AIDestinationSetter setter;
    public List<Transform> patrolPoint;
    public int i = 0;
    [SerializeField] private int index = 0; 

    private void Awake()
    {
        lifeManager.OnTakeDamage += HandleOnTakeDamage;
        lifeManager.OnDie += HandleDie;
    }

    private void OnDestroy()
    {
        lifeManager.OnTakeDamage -= HandleOnTakeDamage;
        lifeManager.OnDie -= HandleDie;
    }

    private void HandleOnTakeDamage()
    {
        materialTintColor.TintColor = damageColor;
        this.GetComponent<SpriteRenderer>().sprite = sprites[index];
        index++;
        
    }

    private void HandleDie()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(transform.parent.gameObject, 0.4f);
        GameManager.instance.pointsCount += 150;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Ship>(out var ship))
        {
            if(ship.TakeDamage(power))
            {
                this.lifeManager.TakeDamage(1);
            }
        }
    }
    void Start() 
    {
        var points = GameObject.FindGameObjectsWithTag("PatrolPoint");
        foreach (GameObject p in points)
        {
            patrolPoint.Add(p.GetComponent<Transform>());
        }
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        //Face the Player
        if(player == null)
        {
           GameObject go = GameObject.Find("Corsair");
           if(go != null)
           {
                player = go.transform;
           }
        }
        else
        {
            var distance = Vector2.Distance(transform.position, player.transform.position);
            if(distance < distanceBetween)
            {   
                Vector3 direction = player.position - transform.position;
                direction.Normalize();
                float zAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, aIPath.rotationSpeed * Time.deltaTime);
                setter.target = player;
                if (distance < (distanceBetween/2))
                {
                   
                    aIPath.endReachedDistance = distanceBetween/2;
                    shooterCounter -= Time.deltaTime;
                    if (shooterCounter <= 0)
                    {
                        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                        shooterCounter = timeBetweenShots;
                    }
                }  
            }
            else
            {
                aIPath.endReachedDistance = 0.1f;
                var d = Vector2.Distance(transform.position, patrolPoint[i].position); 
                if( d <= 0.5f) i = Random.Range(0,patrolPoint.Count); 
                setter.target = patrolPoint[i];                
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceBetween);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, (distanceBetween/2)); 
        
    }
}
