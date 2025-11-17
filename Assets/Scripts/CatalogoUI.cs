using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatalogoUI : MonoBehaviour
{
    [SerializeField]
    public EditorManager editorManager; // referencia al EditorManager
    public GameObject botonPrefab; // prefab del botón
    public Transform contenedorBotones; // contenedor donde se instancian los botones

    private void Start()
    {
        // Generar los botones al iniciar
        GenerarBotones();
        gameObject.SetActive(false); // inicia oculto
    }

    public void GenerarBotones()
    {
        //Limpiar botones existentes antes de generar nuevos
        foreach (Transform hijo in contenedorBotones)
            Destroy(hijo.gameObject);
        // Crear un botón por cada prefab en el EditorManager
        for (int i = 0; i < editorManager.prefabs.Length; i++)
        {
            // Capturar el índice para el listener del botón
            int index = i;
            GameObject nuevoBoton = Instantiate(botonPrefab, contenedorBotones);
            TMP_Text texto = nuevoBoton.GetComponentInChildren<TMP_Text>();
            texto.text = editorManager.prefabs[i].name;
            Button btn = nuevoBoton.GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                // Iniciar el modo creación con el prefab seleccionado
                editorManager.StartCreating(index);
                gameObject.SetActive(false);
            });
        }
    }
}

