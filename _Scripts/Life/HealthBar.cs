using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private LifeManager lifeManager;
    [SerializeField] private Transform barTransform;
    [SerializeField] private GameObject root;
    [SerializeField] private float duration = 1;
    private Tweener lifeTweener;
    void Awake()
    {
        lifeManager.OnLifeChanged += HandleLifeChanged;
        root.gameObject.SetActive(false);
    }

    private void HandleLifeChanged(int obj)
    {
        //barTransform.localScale = new Vector3(lifeManager.GetLifeNormalized(),1, 1);
        root.gameObject.SetActive(true);
        lifeTweener.KillIfActive();
        lifeTweener = barTransform.DOScaleX(lifeManager.GetLifeNormalized(), duration).OnComplete(() =>{
            root.gameObject.SetActive(!lifeManager.isFullLife());
        });
        
    }

    private void OnDestroy() 
    {
        lifeManager.OnLifeChanged -= HandleLifeChanged;
        lifeTweener.KillIfActive();
    }
}
