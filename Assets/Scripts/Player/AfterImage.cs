using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Shader shader;
    public Color color;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shader = Shader.Find("GUI/Text Shader");
    }

    private void Update()
    {
        ColorSprite();
    }

    void ColorSprite()
    {
        spriteRenderer.material.shader = shader;
        spriteRenderer.color = color;
    }
}
