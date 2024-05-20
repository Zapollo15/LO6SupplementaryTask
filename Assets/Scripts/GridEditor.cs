using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridEditor : Editor
{
    private GridManager gridManager;
    private int currentPrefabIndex = 0;

    private void OnEnable()
    {
        gridManager = (GridManager)target;
    }

    private void OnSceneGUI()
    {
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

        Event e = Event.current;
        Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
        Plane plane = new Plane(Vector3.forward, Vector3.zero);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            int x = Mathf.FloorToInt(hitPoint.x / gridManager.spacing);
            int y = Mathf.FloorToInt(hitPoint.y / gridManager.spacing);

            if (x >= 0 && x < gridManager.gridX && y >= 0 && y < gridManager.gridY)
            {
                Vector3 cellCenter = new Vector3(x * gridManager.spacing, y * gridManager.spacing, 0);
                Handles.color = Color.grey;
                Handles.DrawWireCube(cellCenter, Vector3.one * gridManager.spacing);

                Handles.color = new Color(1, 1, 1, 0.5f);
                Handles.DrawSolidRectangleWithOutline(
                    new Vector3[]
                    {
                        cellCenter + new Vector3(-0.5f, -0.5f, 0) * gridManager.spacing,
                        cellCenter + new Vector3(0.5f, -0.5f, 0) * gridManager.spacing,
                        cellCenter + new Vector3(0.5f, 0.5f, 0) * gridManager.spacing,
                        cellCenter + new Vector3(-0.5f, 0.5f, 0) * gridManager.spacing
                    },
                    new Color(1, 1, 1, 0.5f),
                    Color.grey
                );

                if (e.type == EventType.MouseDown)
                {
                    if (e.button == 0)
                    {
                        currentPrefabIndex = (currentPrefabIndex + 1) % gridManager.prefabs.Count;
                        gridManager.ReplaceTile(x, y, currentPrefabIndex);
                        e.Use();
                    }
                    else if (e.button == 1)
                    {
                        currentPrefabIndex = (currentPrefabIndex - 1 + gridManager.prefabs.Count) % gridManager.prefabs.Count;
                        gridManager.ReplaceTile(x, y, currentPrefabIndex);
                        e.Use();
                    }
                }
            }
        }

        SceneView.RepaintAll();
    }
}