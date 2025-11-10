using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField]
    public EditorManager editorManager;

    private GridVisualizer visualizer;

    [SerializeField]
    public float cellSize = 1f;
    public bool mostrarGrid = true;
    private void Awake()
    {
        visualizer = GetComponent<GridVisualizer>();
    }

    private void Update()
    {
        // Mostrar o esconder el grid según el estado del editor
        if (editorManager != null)
        {
            visualizer.mostrarGrid = (editorManager.currentState == EditorManager.EditorState.Create);
        }
    }

    public Vector3 GetSnappedPosition(Vector3 position)
    {
        float x = Mathf.Round(position.x / cellSize) * cellSize;
        float z = Mathf.Round(position.z / cellSize) * cellSize;
        return new Vector3(x, 0, z);
    }
}


