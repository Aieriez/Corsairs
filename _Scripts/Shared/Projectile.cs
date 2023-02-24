using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 8;
    [SerializeField] private GameObject explosionEffect;
    
    void Start()
    {
        var  rb  = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
        Destroy(this.gameObject, 1.5f);  
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {    
        if(other.TryGetComponent<EnemyShooter>(out var enemyShooter))
        {   
            enemyShooter.lifeManager.TakeDamage(1);
            GameManager.instance.pointsCount += 20;
        }
        if(other.TryGetComponent<EnemyChaser>(out var enemyChaser))
        {   
            GameManager.instance.pointsCount += 25;
            Destroy(enemyChaser.gameObject);
        }
        else if(other.TryGetComponent<Ship>(out var ship))
        {
            ship.TakeDamage(1);
        }
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
