using System.IO.Enumeration;
using UnityEngine;
using Pathfinding;

public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float distanceBetween = 10;
    [SerializeField] private Transform player;
    [SerializeField] private int power = 1;
    [SerializeField] private AIDestinationSetter setter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Ship>(out var ship))
        {
            ship.TakeDamage(power);
        }
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(transform.parent.gameObject);
        GameManager.instance.pointsCount += 75; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanceBetween);
    }

    void Update()
    {
        if(player == null)
        {
           GameObject go = GameObject.Find("Corsair");
           if(go != null)
           {
                player = go.transform;
           }
        }
        var distance = Vector2.Distance(transform.position, player.transform.position);
        if(distance < distanceBetween)
        {
            setter.target = player;
        }
        else 
        {
            setter.target = null;
        }

    }
    /* public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint;
    bool reachEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null) return;
        
        if (currentWaypoint >+ path.vectorPath.Count)
        {
            reachEndOfPath = true;
        } else
        {
            reachEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    } */

}
