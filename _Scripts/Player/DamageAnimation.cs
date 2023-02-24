using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class DamageAnimation : MonoBehaviour
{
    private Tweener tweener;
    [SerializeField] private float blinkDuration = .7f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if(this != null) spriteRenderer = GetComponent<SpriteRenderer>();
    } 

    // Start is called before the first frame update
    public void StartAnimation()
    {
        tweener = spriteRenderer.DOFade(0, blinkDuration).SetLoops(-1);
    }

    public void EndAnimation()
    {
        tweener?.KillIfActive();
        spriteRenderer.color = Color.white;
    }
   
}
