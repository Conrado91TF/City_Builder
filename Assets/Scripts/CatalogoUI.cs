using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatalogoUI : MonoBehaviour
{
    [SerializeField]
    public EditorManager editorManager;
    public GameObject botonPrefab;
    public Transform contenedorBotones;

    private void Start()
    {
        GenerarBotones();
        gameObject.SetActive(false); // inicia oculto
    }

    public void GenerarBotones()
    {
        foreach (Transform hijo in contenedorBotones)
            Destroy(hijo.gameObject);

        for (int i = 0; i < editorManager.prefabs.Length; i++)
        {
            int index = i;
            GameObject nuevoBoton = Instantiate(botonPrefab, contenedorBotones);
            TMP_Text texto = nuevoBoton.GetComponentInChildren<TMP_Text>();
            texto.text = editorManager.prefabs[i].name;
            Button btn = nuevoBoton.GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                editorManager.StartCreating(index);
                gameObject.SetActive(false);
            });
        }
    }
}

