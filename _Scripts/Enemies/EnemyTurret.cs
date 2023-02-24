using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float timeBetweenShots = 1.5f;
    private float shooterCounter = 0;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] internal LifeManager lifeManager;
    [SerializeField] private MaterialTintColor materialTintColor;
    [SerializeField] private Color damageColor = Color.white;
    [SerializeField] private float rotSpeed = 90;
    [SerializeField] private float maxSpeed = 0.8f;
    [SerializeField] private float distanceBetween = 10;
    [SerializeField] private int power = 1;
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
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            float zAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);
                        
            if(distance < distanceBetween)
            {
                maxSpeed = 1f;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, rotSpeed * Time.deltaTime);
                if (distance < (distanceBetween/2))
                {
                    maxSpeed /= 2;
                    shooterCounter -= Time.deltaTime;
                    if (shooterCounter <= 0)
                    {
                        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                        shooterCounter = timeBetweenShots;
                    }
                }
            }

            
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceBetween);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceBetween/2);
        
    }
}
