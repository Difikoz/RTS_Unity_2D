using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public static class Directions
    {
        public static List<Vector2> EightDirections = new()
        {
            new(0f,1f),
            new(0f,-1f),
            new(1f,0f),
            new(-1f,0f),
            new(0.7f,0.7f),
            new(-0.7f,0.7f),
            new(0.7f,-0.7f),
            new(-0.7f,-0.7f),
        };

        public static List<Vector2> SixteenDirections = new()
        {
            new(0f,1f),
            new(0f,-1f),
            new(1f,0f),
            new(-1f,0f),
            new(0.7f,0.7f),
            new(-0.7f,0.7f),
            new(0.7f,-0.7f),
            new(-0.7f,-0.7f),
            new(0.9f,0.45f),
            new(-0.9f,-0.45f),
            new(0.9f,-0.45f),
            new(-0.9f,0.45f),
            new(0.45f,0.9f),
            new(-0.45f,-0.9f),
            new(0.45f,-0.9f),
            new(-0.45f,0.9f),
        };
    }
}