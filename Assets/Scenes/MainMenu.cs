using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play1Game()
    {
        SceneManager.LoadScene("Juego Prueba"); // Carga la escena del juego principal
    }

    public void Opensetting()
    {
        SceneManager.LoadScene("Options"); // Carga la escena de opciones
    }

    public void QuitGame()
    {
        Application.Quit(); // Cierra la aplicación
    }

    // Método para volver al menú principal desde la escena de opciones
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Carga la escena del menú principal
    }
}


