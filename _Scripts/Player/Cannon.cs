using UnityEngine;

public class Cannon : MonoBehaviour
{

    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float timeBetweenShots = 0.5f;
    private float shooterCounter = 0;
    private DamageAnimation damageAnimation;
    [SerializeField] private KeyCode key;

    private void Awake() =>  damageAnimation = GetComponent<DamageAnimation>();

    public void TakeDamage() => damageAnimation.StartAnimation();
    public void EndTakeDamage() => damageAnimation.EndAnimation();

    void Update()
    {

         if(Input.GetKeyDown(key))
        {
            shooterCounter -= Time.deltaTime;
            if (shooterCounter <= 0)
            {
                Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                shooterCounter = timeBetweenShots/2;
            } 
        }
        if(Input.GetKey(key))
        {
            shooterCounter -= Time.deltaTime;
            if (shooterCounter <= 0)
            {
                Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                shooterCounter = timeBetweenShots;
            } 
        }
    }
}
