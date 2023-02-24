using System;
using UnityEngine;
public class Ship : MonoBehaviour
{
    
    [SerializeField] private Vector2 movement;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float rotationAngle = 0;
    [SerializeField] private float rotationSpeed = 2;
    [SerializeField] [Range(0,1)] private float driftFactor = 0.8f;

    [SerializeField] private Cannon[] cannons;
    [SerializeField] private float dragFactor = 3;
    [SerializeField] private float  maxSpeed = 3;
    [SerializeField] private Rigidbody2D shipRb;
    private LifeManager lifeManager;
    private DamageAnimation damageAnimation;
    
    void Awake()
    {
        shipRb =  GetComponent<Rigidbody2D>();
        damageAnimation = GetComponent<DamageAnimation>();
        lifeManager = GetComponent<LifeManager>();
        lifeManager.OnTakeDamage += HandleTakeDamage;
        lifeManager.OnEndTakingDamage += HandleEndTakingDamage; 
        lifeManager.OnDie += HangleOnDie;
    }
    private void OnDestroy() 
    {
        lifeManager.OnTakeDamage -= HandleTakeDamage;
        lifeManager.OnEndTakingDamage -= HandleEndTakingDamage; 
        lifeManager.OnDie -= HangleOnDie;
    }

    public bool TakeDamage(int power) => lifeManager.TakeDamage(power);
    
    private void HangleOnDie()
    {
        GameManager.instance.endGame = true;
    }

    private void HandleEndTakingDamage()
    {
        damageAnimation.EndAnimation();
        foreach(var cannon in cannons)
        {
            cannon.EndTakeDamage();
        }
    }

    private void HandleTakeDamage()
    {
        damageAnimation.StartAnimation();
        foreach(var cannon in cannons)
        {
            cannon.TakeDamage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement.y = Input.GetAxis("Vertical");
        movement.x = Input.GetAxis("Horizontal"); 
    }

    void FixedUpdate() 
    {
        ApplySpeed();
        ApplyRotation();
        ApplyDrift();     
    }

    void ApplySpeed()
    {
        if(movement.y < 0) movement.y = 0;
        
        if (movement.y == 0)
        {
            shipRb.drag = Mathf.Lerp(shipRb.drag, dragFactor, Time.fixedDeltaTime);
        }
        else
        {
            shipRb.drag = 0;
        }

        var velocityUp =  Vector2.Dot(transform.up, shipRb.velocity);
        if(velocityUp > maxSpeed) return;
        if (velocityUp < (-maxSpeed * 0.5)) return;
        shipRb.AddForce(transform.up * movement.y * acceleration, ForceMode2D.Force);
        
    }

    void ApplyRotation()
    {
        rotationAngle = rotationAngle - (movement.x * rotationSpeed);
        shipRb.MoveRotation(rotationAngle);
    }
    void ApplyDrift()
    {
        Vector2 velocityUp = transform.up * Vector2.Dot(shipRb.velocity, transform.up);
        Vector2 velocityRight = transform.right * Vector2.Dot(shipRb.velocity, transform.right);
        shipRb.velocity = velocityUp + velocityRight * driftFactor;
    }

}