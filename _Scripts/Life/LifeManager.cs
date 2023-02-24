using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private CharacterLifeData characterLifeData;
    public event Action<int> OnLifeChanged;
    public event Action OnTakeDamage;
    public event Action OnEndTakingDamage;
    public event Action OnDie;
    private int life;
    public int Life
    {
        get { return life; }
        set { 
            if(life < 0) return; 
            life = value; 
            OnLifeChanged?.Invoke(life);
            if(life == 0)
            {
                OnDie?.Invoke();
            }
        }
    }
    private DateTime lastTimeDamage;
    private WaitForSeconds endTakingDamageDelay;
        
    // Start is called before the first frame update
    void Start()
    {
        Life = characterLifeData.fullLife;
        endTakingDamageDelay = new WaitForSeconds (characterLifeData.timeBetweenDamage);
    }

    public bool isFullLife()
    {
        return life == characterLifeData.fullLife;
    }
    public float GetLifeNormalized()
    {
        return (float) life / characterLifeData.fullLife;
    }

    public bool TakeDamage (int power)
    {
        if(!CanTakeDamage()) return false;
        this.Life -= power;
        OnTakeDamage?.Invoke();
        StartCoroutine(EndTakeDamage());
        lastTimeDamage = DateTime.UtcNow;
        return true;
    }
    private IEnumerator EndTakeDamage()
    {
        yield return endTakingDamageDelay;
        OnEndTakingDamage?.Invoke();
    }
    private bool CanTakeDamage()
    {
        if(!characterLifeData.invulnarableOnDamage) return true;
        if(characterLifeData.timeBetweenDamage > 0)
        {
            TimeSpan timeSpan = DateTime.UtcNow - lastTimeDamage;
            return timeSpan.TotalSeconds > characterLifeData.timeBetweenDamage;
        }
        return true;
    }
}
