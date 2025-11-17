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
        // Obtener referencia al visualizador de grid
        visualizer = GetComponent<GridVisualizer>();
    }

    private void Update()
    {
        // Actualizar el estado del grid
        // Mostrar o esconder el grid según el estado del editor
        if (editorManager != null)
        {
            visualizer.mostrarGrid = (editorManager.currentState == EditorManager.EditorState.Create);
        }
    }

    public Vector3 GetSnappedPosition(Vector3 position)
    {
        // Ajustar la posición al grid
        // Redondear la posición a la celda más cercana
        float x = Mathf.Round(position.x / cellSize) * cellSize;
        float z = Mathf.Round(position.z / cellSize) * cellSize;
        return new Vector3(x, 0, z);
    }
}


