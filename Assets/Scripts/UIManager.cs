using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    public RectTransform panelUI;
    public EditorManager editorManager;
    public CatalogoUI catalogoUI;

    [SerializeField]
    public float showY = 0f;       // Posición Y cuando se muestra
    public float hideY = -300f;    // Posición Y cuando se oculta
    public float animDuration = 0.5f;

    [SerializeField]
    public float showX = 0f;       // Posición X cuando se muestra
    public float hideX = -300f;    // Posición X cuando se oculta
    public float animDurationX = 0.5f;
    
    [SerializeField]
    public LeanTweenType showEaseType = LeanTweenType.easeOutBack; // Tipo de easing para mostrar
    public LeanTweenType hideEaseType = LeanTweenType.easeInBack; // Tipo de easing para ocultar

    [SerializeField]
    TextMeshProUGUI UI_Texts;

    private bool isVisible = false;

    void Start()
    {
        // Ocultar al inicio
        if (panelUI != null)
            panelUI.anchoredPosition = new Vector2(panelUI.anchoredPosition.x, hideY);
    }

    public void ToggleUIX()
    {
        // Alternar visibilidad en el eje X
        if (isVisible)
            HideUIX();
        else
            ShowUIX();
    }
    public void ToggleUI()
    {
        // Alternar visibilidad en el eje Y
        if (isVisible)
            HideUI();
        else
            ShowUI();
    }

    public void ShowUIX()
    {
        // Verificar si panelUI no es nulo
        if (panelUI == null) return;
        // Animar la posición X para mostrar
        LeanTween.moveX(panelUI, showX, animDurationX)
            .setEase(showEaseType);
        isVisible = true;
    }
    
    public void HideUIX()
    {
        // Verificar si panelUI no es nulo
        if (panelUI == null) return;
        // Animar la posición X para ocultar
        LeanTween.moveX(panelUI, hideX, animDurationX)
            .setEase(hideEaseType);
        isVisible = false;
    }
    public void ShowUI()
    {
        // Verificar si panelUI no es nulo
        if (panelUI == null) return;
        // Animar la posición Y para mostrar
        LeanTween.moveY(panelUI, showY, animDuration)
            .setEase(showEaseType);
        isVisible = true;
    }

    public void HideUI()
    {
        // Verificar si panelUI no es nulo
        if (panelUI == null) return;
        // Animar la posición Y para ocultar
        LeanTween.moveY(panelUI, hideY, animDuration)
            .setEase(hideEaseType);
        isVisible = false;
        // Ocultar el catálogo si está abierto
    }
    public void BotonCrear()
    {
        // Actualizar el texto del UI
        UI_Texts.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI_Texts", "_crear");
        // Cambiar el estado del editor a Crear
        if (catalogoUI != null)
            catalogoUI.gameObject.SetActive(true);
        
    }

    public void BotonMover()
    {
        // Actualizar el texto del UI
        UI_Texts.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI_Texts", "_mover");
        // Cambiar el estado del editor a Mover
        editorManager.CambiarEstado(EditorManager.EditorState.Move);
        // Ocultar el catálogo si está abierto
        if (catalogoUI != null)
            catalogoUI.gameObject.SetActive(false);

       
    }

    public void BotonRotar()
    {
        UI_Texts.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI_Texts", "_rotar");
        // Cambiar el estado del editor a Rotar
        editorManager.CambiarEstado(EditorManager.EditorState.Rotate);
        // Ocultar el catálogo si está abierto
        if (catalogoUI != null)
            catalogoUI.gameObject.SetActive(false);

        
    }

    public void BotonEliminar()
    {
        UI_Texts.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI_Texts", "_eliminar");
        // Cambiar el estado del editor a Eliminar
        editorManager.CambiarEstado(EditorManager.EditorState.Delete);
        // Ocultar el catálogo si está abierto
        if (catalogoUI != null)
            catalogoUI.gameObject.SetActive(false);

    }
    
    

   
}




