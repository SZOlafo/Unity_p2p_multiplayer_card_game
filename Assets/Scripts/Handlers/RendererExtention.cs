using Assets.Scripts.Models;
using UnityEngine;

public static class RendererExtentions
{
    public static int MoveToTopLayer(this Renderer renderer, int topLayer)
    {
        if (renderer.sortingOrder < topLayer || renderer.sortingOrder == 0)
        {
            renderer.sortingOrder = ++topLayer;
        }
        return topLayer;
    }
}