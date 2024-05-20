using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public List<GameObject> prefabs;
    public int gridX = 5;
    public int gridY = 5;
    public float spacing = 1f;

    private GameObject[,] gridObjects;

    void Start()
    {
        gridObjects = new GameObject[gridX, gridY];
        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                Vector2 pos = new Vector2(x, y) * spacing;
                gridObjects[x, y] = Instantiate(prefabs[0], pos, Quaternion.identity);
            }
        }
    }

    public void ReplaceTile(int x, int y, int nextPrefabIndex)
    {
        if (x < 0 || x >= gridX || y < 0 || y >= gridY) return;

        GameObject currentObject = gridObjects[x, y];
        if (currentObject != null)
        {
            DestroyImmediate(currentObject);
        }

        Vector2 pos = new Vector2(x, y) * spacing;
        gridObjects[x, y] = Instantiate(prefabs[nextPrefabIndex], pos, Quaternion.identity);
    }
}