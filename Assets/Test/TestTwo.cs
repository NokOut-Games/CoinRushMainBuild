using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTwo : MonoBehaviour
{
    public Texture2D tex;
    public Sprite mySprite;
    public SpriteRenderer SpriteRenderer;

    void Start()
    {
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        mySprite = Sprite.Create(tex, new Rect(0, 0f,tex.width, tex.height), new Vector2(0.5f, 0.5f), 100f);
        
        SpriteRenderer.sprite = mySprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
