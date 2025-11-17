using UnityEngine;

public class PanelCategoriasUI : MonoBehaviour
{
    [Header("Paneles")]
    public RectTransform panelComercio;
    public RectTransform panelIndustria;
    public RectTransform panelResidencias;
    public RectTransform panelCarreteras;

    [Header("Animación")]
    public float showX = 0f;
    public float hideX = 500f;
    public float animTime = 0.5f;
    public LeanTweenType easeIn = LeanTweenType.easeInBack;
    public LeanTweenType easeOut = LeanTweenType.easeOutBack;
    // Estado actual del panel activo
    private RectTransform activePanel = null;
    private bool isTransitioning = false;

    void Start()
    {
        // Los paneles se ocultan al iniciar al juego
        OcultarInstantaneo(panelComercio);
        OcultarInstantaneo(panelIndustria);
        OcultarInstantaneo(panelResidencias);
        OcultarInstantaneo(panelCarreteras);
    }

    void OcultarInstantaneo(RectTransform panel)
    {
        // Mover el panel fuera de la vista y desactivarlo
        if (panel == null) return;
        panel.anchoredPosition = new Vector2(hideX, panel.anchoredPosition.y);
        panel.gameObject.SetActive(false);
    }

    void ShowPanel(RectTransform panel)
    {
        // Evitar múltiples transiciones simultáneas
        if (isTransitioning) return;
        isTransitioning = true;

        // Si hay panel activo, ciérralo primero
        if (activePanel != null && activePanel != panel)
        {
            HidePanel(activePanel, false);
        }

        // Mostrar el nuevo panel
        panel.gameObject.SetActive(true);
        LeanTween.moveX(panel, showX, animTime)
            .setEase(easeOut)
            .setOnComplete(() =>
            {
                activePanel = panel;
                isTransitioning = false;
            });
    }

    public void HidePanel(RectTransform panel, bool updateActive = true)
    {
        // Evitar múltiples transiciones simultáneas
        if (isTransitioning) return;
        isTransitioning = true;
        // Ocultar el panel
        LeanTween.moveX(panel, hideX, animTime)
        .setEase(easeIn)
        .setOnComplete(() =>
        {
            panel.gameObject.SetActive(false);

            if (updateActive && activePanel == panel)
                activePanel = null;

            isTransitioning = false;
        });
    }

    // ==========================
    // BOTONES PRINCIPALES
    // ==========================
    // Métodos para abrir cada panel individualmente con los botones principales
    public void AbrirComercio() => ShowPanel(panelComercio);
    public void AbrirIndustria() => ShowPanel(panelIndustria);
    public void AbrirResidencias() => ShowPanel(panelResidencias);
    public void AbrirCarreteras() => ShowPanel(panelCarreteras);

    // ==========================
    // BOTÓN X
    // ==========================
    //  Cerrar el panel activo con el botón X
    public void CerrarPanelActual()
    {
        if (activePanel != null)
            HidePanel(activePanel);
    }
}
