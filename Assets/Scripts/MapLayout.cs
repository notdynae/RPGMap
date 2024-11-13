using System;
using UnityEngine;

public class MapLayout : MonoBehaviour
{
    public MapManager mapManager;

    public void GenerateRoads(int width, int height) {
        int horizontalRoads = (int)Math.Round(1 + UnityEngine.Random.Range(-0.80f, 0.80f));
        Debug.Log($"{horizontalRoads}");
    }
}
