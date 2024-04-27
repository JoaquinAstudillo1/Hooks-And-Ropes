using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        // Cargar la escena "Test2 - copia" del otro proyecto
        SceneManager.LoadScene("SampleScene");
    }

    public void Setting()
    {
        // Cargar la escena de opciones
        SceneManager.LoadScene("Setting");
    }
}
