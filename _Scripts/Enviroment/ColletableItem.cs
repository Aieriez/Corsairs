using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColletableItem : MonoBehaviour
{
    [SerializeField] private ColletableItemData colletableItemData;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public event Action<ColletableItemData> OnItemCollected;
    // Start is called before the first frame update
    void OnValidate()
    {
        if (colletableItemData != null)
        {
            spriteRenderer.sprite = colletableItemData.sprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
          if(other.CompareTag("Player"))
          {
            OnItemCollected?.Invoke(colletableItemData);
            Destroy(gameObject);
          } 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
