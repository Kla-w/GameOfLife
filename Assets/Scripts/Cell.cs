using System;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color deadColor;
    public Color aliveColor;
    [HideInInspector] public GridManager grid;

    [SerializeField] private bool _alive;

    public bool Alive
    {
        get => _alive;
        set
        {
            _alive = value;
            UpdateColor();
        }
    }

    private void OnValidate()
    {
        UpdateColor();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            Alive = true;
        }
    }

    private void UpdateColor()
    {
        spriteRenderer.color = Alive ? aliveColor : deadColor;
    }
}