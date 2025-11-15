using UnityEngine;



public class EditorManager : MonoBehaviour
{
    public enum EditorState { Neutral, Create, Move, Rotate, Delete } // Estados del editor
    public EditorState currentState = EditorState.Neutral;// Estado actual
    public UIManager uiManager; // Referencia al UIManager 

    [Header("Referencias")]
    public GameObject[] prefabs;
    public GridSystem gridSystem;

    private GameObject objetoTemporal;
    private GameObject objetoSeleccionado;

    void Update()
    {
        switch (currentState)
        {
            case EditorState.Create:
                HandleCreate();
                break;
            case EditorState.Move:
                HandleMove();
                break;
            case EditorState.Rotate:
                HandleRotate();
                break;
            case EditorState.Delete:
                HandleDelete();
                break;
        }
    }

    public void CambiarEstado(EditorState nuevoEstado)
    {
        // Limpia objetos temporales o selección previa
        if (objetoTemporal != null)
        {
            Destroy(objetoTemporal);
            objetoTemporal = null;
        }
        objetoSeleccionado = null;

        currentState = nuevoEstado;
        Debug.Log("Estado cambiado a: " + currentState);
    }

    // ----------------------------------------------------
    // 🧱 CREAR OBJETOS
    // ----------------------------------------------------
    void HandleCreate() // Maneja la creación de objetos
    {
        if (objetoTemporal == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 gridPos = hit.point;
            if (gridSystem != null)
                gridPos = gridSystem.GetSnappedPosition(hit.point);

            objetoTemporal.transform.position = gridPos;

            if (Input.GetMouseButtonDown(0))
            {
                // Reproduce sonido al colocar el objeto
                AudioManager.instance.PlayColocar();

                objetoTemporal = null;
                currentState = EditorState.Neutral;
            }
        }
    }

    public void StartCreating(int index) // Iniciar creación desde índice de prefab
    {
        if (index >= 0 && index < prefabs.Length)
        {
            objetoTemporal = Instantiate(prefabs[index]);
            
            objetoTemporal.layer = LayerMask.NameToLayer("Edificios");
            
            currentState = EditorState.Create;
        }
    }

    public void StartCreating(GameObject prefab)
    {
        if (prefab == null) return;

        // Elimina cualquier objeto temporal previo
        if (objetoTemporal != null)
            Destroy(objetoTemporal);

        // Instancia el nuevo prefab
        objetoTemporal = Instantiate(prefab);
        objetoTemporal.layer = LayerMask.NameToLayer("Edificios");
        currentState = EditorState.Create;

        Debug.Log("Creando desde catálogo: " + prefab.name);
    }
    // ----------------------------------------------------
    // 🔹 MOVER OBJETOS
    // ----------------------------------------------------
    void HandleMove()
    {
        int layerMask = LayerMask.GetMask("Edificios");
        // Clic izquierdo = seleccionar objeto
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null)
                {
                    objetoSeleccionado = hit.collider.gameObject;
                    Debug.Log("Objeto seleccionado para mover: " + objetoSeleccionado.name);
                }
            }
        }

        // Si tenemos objeto seleccionado, seguir el ratón
        if (objetoSeleccionado != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 gridPos = hit.point;
                if (gridSystem != null)
                    gridPos = gridSystem.GetSnappedPosition(hit.point);

                objetoSeleccionado.transform.position = gridPos;

                // Clic derecho = confirmar movimiento
                if (Input.GetMouseButtonDown(1))
                {
                    objetoSeleccionado = null;
                    currentState = EditorState.Neutral;
                    Debug.Log("Movimiento completado.");
                }
            }
        }
    }

    // ----------------------------------------------------
    // 🔁 ROTAR OBJETOS
    // ----------------------------------------------------
    void HandleRotate()
    {
        // Seleccionar con clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                objetoSeleccionado = hit.collider.gameObject;
                Debug.Log("Objeto seleccionado para rotar: " + objetoSeleccionado.name);
            }
        }

        // Rotar con clic derecho
        if (objetoSeleccionado != null && Input.GetMouseButtonDown(1))
        {
            objetoSeleccionado.transform.Rotate(Vector3.up, 90f);
            Debug.Log("Objeto rotado 90 grados: " + objetoSeleccionado.name);
        }

        // Salir del modo con tecla ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            objetoSeleccionado = null;
            currentState = EditorState.Neutral;
        }
    }

    // ----------------------------------------------------
    // ❌ ELIMINAR OBJETOS
    // ----------------------------------------------------
    void HandleDelete()
    {
        int layerMask = LayerMask.GetMask("Edificios");

        if (Input.GetMouseButtonDown(0))
        {
           AudioManager.instance.PlayEliminar();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                GameObject obj = hit.collider.gameObject;

                if (obj.CompareTag("Prefab"))
                {
                    // Animación segura de eliminación
                    LeanTween.scale(obj, Vector3.zero, 0.3f).setOnComplete(() =>
                    {
                        obj.SetActive(false);
                        Destroy(obj, 0.05f);
                    });

                    Debug.Log("Objeto eliminado: " + obj.name);
                }
                else
                {
                    Debug.Log("No se puede eliminar: " + obj.name);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            currentState = EditorState.Neutral;
        }
    }
    
}   

