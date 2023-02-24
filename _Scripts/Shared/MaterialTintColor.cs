using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTintColor : MonoBehaviour
{
    private int colorPropertyId = Shader.PropertyToID("_Color");
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float tintFadeSpeed = 1;
    private Color tintColor = new Color(1, 1, 1, 0);
    public Color TintColor
    {
        get { return tintColor; }
        set { tintColor = value; }
    }

    private MaterialPropertyBlock block;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        block = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if (tintColor.a > 0)
        {           
            tintColor.a = Mathf.Clamp01(tintColor.a - tintFadeSpeed * Time.deltaTime);
            spriteRenderer.GetPropertyBlock(block);
            block.SetColor(colorPropertyId, tintColor);
            spriteRenderer.SetPropertyBlock(block);
  
        }
    }
}
