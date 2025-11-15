using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip colocarSonido; // sonido al colocar un edificio
    public AudioClip eliminarSonido; // opcional por si luego quieres
    public AudioSource audioSource;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void PlayColocar()
    {
        if (colocarSonido != null)
            audioSource.PlayOneShot(colocarSonido);
    }

    public void PlayEliminar()
    {
        if (eliminarSonido != null)
            audioSource.PlayOneShot(eliminarSonido);
    }
}
