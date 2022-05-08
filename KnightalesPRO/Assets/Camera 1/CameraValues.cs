using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraValues
{
    private static Vector2 maxPosition;
    private static Vector2 minPosition;

    private static Dictionary<string, List<Vector2>> mapaBounds = new Dictionary<string, List<Vector2>>
    {
        {"castillo", new List<Vector2>() { new Vector2(6.9f, 5.37f), new Vector2(-6.09f, -8.92f)} },
        {"cueva", new List<Vector2>() { new Vector2(-18.12f, -15.75f), new Vector2(-27.92f, -25.18f)} },
        {"tienda", new List<Vector2>() { new Vector2(17.5f, -8.98f), new Vector2(16.65f, -10.52f)} },
    };
   

    
    
    public static void setCameraPos(string mapa)
    {

        maxPosition = mapaBounds[mapa][0];
        minPosition = mapaBounds[mapa][1];
    
    }

    public static Vector2 getMaxPos() { return maxPosition; }

    public static Vector2 getMinPos() { return minPosition; }
}
