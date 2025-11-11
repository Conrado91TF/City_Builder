using UnityEngine;
using UnityEngine.UI;

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
    public LeanTweenType showEaseType = LeanTweenType.easeOutBack;
    public LeanTweenType hideEaseType = LeanTweenType.easeInBack;

    private bool isVisible = false;

    void Start()
    {
        // Ocultar al inicio
        if (panelUI != null)
            panelUI.anchoredPosition = new Vector2(panelUI.anchoredPosition.x, hideY);
    }

    public void ToggleUIX()
    {
        if (isVisible)
            HideUIX();
        else
            ShowUIX();
    }
    public void ToggleUI()
    {
        if (isVisible)
            HideUI();
        else
            ShowUI();
    }

    public void ShowUIX()
    {
        if (panelUI == null) return;
        LeanTween.moveX(panelUI, showX, animDurationX)
            .setEase(showEaseType);
        isVisible = true;
    }
    
    public void HideUIX()
    {
        if (panelUI == null) return;
        LeanTween.moveX(panelUI, hideX, animDurationX)
            .setEase(hideEaseType);
        isVisible = false;
    }
    public void ShowUI()
    {
        if (panelUI == null) return;

        LeanTween.moveY(panelUI, showY, animDuration)
            .setEase(showEaseType);
        isVisible = true;
    }

    public void HideUI()
    {
        if (panelUI == null) return;

        LeanTween.moveY(panelUI, hideY, animDuration)
            .setEase(hideEaseType);
        isVisible = false;
    }
    public void BotonCrear()
    {
        editorManager.CambiarEstado(EditorManager.EditorState.Create);

        if (catalogoUI != null)
            catalogoUI.gameObject.SetActive(true);
    }

    public void BotonMover()
    {
        editorManager.CambiarEstado(EditorManager.EditorState.Move);

        if (catalogoUI != null)
            catalogoUI.gameObject.SetActive(false);
    }

    public void BotonRotar()
    {
        editorManager.CambiarEstado(EditorManager.EditorState.Rotate);

        if (catalogoUI != null)
            catalogoUI.gameObject.SetActive(false);
    }

    public void BotonEliminar()
    {
        editorManager.CambiarEstado(EditorManager.EditorState.Delete);

        if (catalogoUI != null)
            catalogoUI.gameObject.SetActive(false);
    }
}

