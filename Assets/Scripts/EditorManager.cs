using UnityEngine;



public class EditorManager : MonoBehaviour
{
    public enum EditorState { Neutral, Create, Move, Rotate, Delete }
    public EditorState currentState = EditorState.Neutral;
    public UIManager uiManager;

    [Header("Referencias")]
    public GameObject[] prefabs;     // Prefabs disponibles

    private GameObject objetoTemporal;

    void Update()
    {
        switch (currentState)
        {
            case EditorState.Create:
                HandleCreate();
                break;
            case EditorState.Move:
                // luego añadiremos esto
                break;
            case EditorState.Rotate:
                // luego añadiremos esto
                break;
            case EditorState.Delete:
                // luego añadiremos esto
                break;
    }   }

    public void CambiarEstado(EditorState nuevoEstado)
    {
        currentState = nuevoEstado;
        Debug.Log("Estado cambiado a: " + currentState);
    }

    void HandleCreate()
    {
        if (objetoTemporal == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            objetoTemporal.transform.position = hit.point;

            if (Input.GetMouseButtonDown(0))
            {
                objetoTemporal = null;
                currentState = EditorState.Neutral;
            }
        }
    }

    public void StartCreating(int index)
    {
        if (index >= 0 && index < prefabs.Length)
        {
            objetoTemporal = Instantiate(prefabs[index]);
            currentState = EditorState.Create;
        }
        else
        {
            Debug.LogWarning("Índice fuera de rango al intentar crear prefab.");
        }
    }
   

}


