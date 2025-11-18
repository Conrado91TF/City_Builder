using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public enum EditorState { Neutral, Create, Move, Rotate, Delete } // Estados del editor
    public EditorState currentState = EditorState.Neutral;// Estado actual
    public UIManager uiManager; // Referencia al UIManager 

    [SerializeField]
    public GameObject[] prefabs;
    public GridSystem gridSystem;
    // Referencia al sistema de cuadrícula
    private GameObject objetoTemporal;
    private GameObject objetoSeleccionado;

    

    void Update()
    {
        // Maneja el comportamiento según el estado actual
        switch (currentState)
        {
            // Estados del editor
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
        // Cambia el estado actual
        currentState = nuevoEstado;
        Debug.Log("Estado cambiado a: " + currentState);
    }

    // ----------------------------------------------------
    // 🧱 CREAR OBJETOS
    // ----------------------------------------------------
    void HandleCreate() // Maneja la creación de objetos
    {
        if (objetoTemporal == null) return;
        // Posición del ratón y raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {    // Raycast para detectar el suelo
            Vector3 gridPos = hit.point;
            if (gridSystem != null)
                gridPos = gridSystem.GetSnappedPosition(hit.point);
            // Actualiza la posición del objeto temporal
            objetoTemporal.transform.position = gridPos;
            // Colocar objeto con clic izquierdo
            if (Input.GetMouseButtonDown(0))
            {
                // Reproduce sonido al colocar el objeto
                AudioManager.instance.PlayColocar();

                // Guardamos el objeto final antes de eliminar el temporal
                GameObject objFinal = objetoTemporal;

                objetoTemporal = null;
                currentState = EditorState.Neutral;
                // Cambia al estado neutral
            
                if (objFinal != null)
                {
                    // Ponemos escala inicial pequeña
                    objFinal.transform.localScale = Vector3.zero;

                    // Animación de aparición
                    LeanTween.scale(objFinal, Vector3.one, 0.25f)
                        .setEase(LeanTweenType.easeOutBack);

                }
            }
        }
    }

    public void StartCreating(int index) // Iniciar creación desde índice de prefab
    {
        // Elimina cualquier objeto temporal previo
        if (index >= 0 && index < prefabs.Length)
        {
            objetoTemporal = Instantiate(prefabs[index]); 
            // Instancia el prefab seleccionado
            objetoTemporal.layer = LayerMask.NameToLayer("Edificios"); 
            // Asigna la capa "Edificios"
            currentState = EditorState.Create; 
            // Cambia al estado de creación
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
            //Posición del ratón y raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Raycast detectando en la capa de edificios
                if (hit.collider != null)
                {
                    // Objeto golpeado en la capa de edificios
                    objetoSeleccionado = hit.collider.gameObject;
                    Debug.Log("Objeto seleccionado para mover: " + objetoSeleccionado.name);
                }
            }
        }

        // Si tenemos objeto seleccionado, seguir el ratón
        if (objetoSeleccionado != null)
        {
            // Posición del ratón y raycast EN LA CAPA DE EDIFICIOS
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Actualiza la posición del objeto seleccionado
                Vector3 gridPos = hit.point;
                if (gridSystem != null)
                    gridPos = gridSystem.GetSnappedPosition(hit.point);
                // Ajusta a la cuadrícula si es necesario
                objetoSeleccionado.transform.position = gridPos;

                // Clic derecho = confirmar movimiento
                if (Input.GetMouseButtonDown(1))
                {
                    AudioManager.instance.PlayColocar();
                    // Confirmar movimiento
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
            //Posición del ratón y raycast
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
            AudioManager.instance.PlayRotar();
            // Rota el objeto 90 grados alrededor del eje Y
            objetoSeleccionado.transform.Rotate(Vector3.up, 90f);
            Debug.Log("Objeto rotado 90 grados: " + objetoSeleccionado.name);
        }

        // Salir del modo con tecla ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Salir del modo rotar
            objetoSeleccionado = null;
            currentState = EditorState.Neutral;
        }
    }

    // ----------------------------------------------------
    // ❌ ELIMINAR OBJETOS
    // ----------------------------------------------------
    void HandleDelete() 
    {
       // Maneja la eliminación de objetos
        int layerMask = LayerMask.GetMask("Edificios"); 
        // Capa de edificios

        if (Input.GetMouseButtonDown(0))
        {
            // Clic izquierdo para eliminar
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                // Objeto golpeado
                GameObject obj = hit.collider.gameObject;

                if (obj.CompareTag("Prefab"))
                {
                    // Elimina el objeto con animación
                    AudioManager.instance.PlayEliminar();
                    // Reproduce sonido al eliminar objeto
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
            // Tecla ESC para salir del modo eliminar
            // Salir del modo eliminar
            currentState = EditorState.Neutral;
        }
    }
    
}   

