using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererOrderSystem : MonoBehaviour
{
    SpriteRenderer[] renderers;
    void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.sortingOrder = (int)(renderer.transform.position.y * -100);
        }
    }
}
