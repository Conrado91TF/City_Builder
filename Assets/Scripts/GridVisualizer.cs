using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    [SerializeField]
    public int width = 20;       // ancho en celdas
    public int height = 20;      // alto en celdas
    public float cellSize = 1f;  // tamaño de cada celda
    public Color lineColor = Color.gray;
    public bool mostrarGrid = true;

    private void OnDrawGizmos()
    {
        // Dibuja la cuadrícula en el editor
        Gizmos.color = lineColor;
        Vector3 origin = transform.position;

        // Dibuja las líneas verticales
        for (int x = 0; x <= width; x++)
        {
            Vector3 start = origin + new Vector3(x * cellSize, 0, 0);
            Vector3 end = origin + new Vector3(x * cellSize, 0, height * cellSize);
            Gizmos.DrawLine(start, end);
        }
        // Dibuja las líneas horizontales
        for (int z = 0; z <= height; z++)
        {
            Vector3 start = origin + new Vector3(0, 0, z * cellSize);
            Vector3 end = origin + new Vector3(width * cellSize, 0, z * cellSize);
            Gizmos.DrawLine(start, end);
        }
    }
}
