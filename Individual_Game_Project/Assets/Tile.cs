using System;
using UnityEngine;

[ExecuteAlways]
public class Tile : MonoBehaviour
{
    SpriteRenderer renderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            SetTileColor();
        }
    }

    public void SetTileColor()
    {
        //set color to black/white if the sum of the x-y position is even/odd
        if ((int)(transform.position.x + transform.position.y) % 2 == 0)
        {
            renderer.color = Color.white;
        }
        else
        {
            renderer.color = Color.black;
        }
    }

    private void OnMouseDown()
    {
        if (Application.isPlaying)
        {
            renderer.color = Color.green;
            Invoke("SetTileColor", 2f);
        }
    }

    private void OnMouseEnter()
    {
        if (Application.isPlaying)
        {
            renderer.color = Color.yellow;
        }
    }

    private void OnMouseExit()
    {
        if (Application.isPlaying)
        {
            SetTileColor();
        }
    }
}
