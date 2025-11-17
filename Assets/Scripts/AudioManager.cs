using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip colocarSonido; // sonido al colocar un edificio
    public AudioClip eliminarSonido; // opcional por si luego quieres
    public AudioSource audioSource; // fuente de audio para reproducir los sonidos

    void Awake()
    {
        // Implementar el patrón singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void PlayColocar()
    {
        // Reproducir el sonido de colocar edificio
        if (colocarSonido != null)
            audioSource.PlayOneShot(colocarSonido);
    }

    public void PlayEliminar()
    {
        // Reproducir el sonido de eliminar edificio
        if (eliminarSonido != null)
            audioSource.PlayOneShot(eliminarSonido);
    }
}
