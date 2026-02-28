using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartSceneButton : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    // Este método pode ser chamado via OnClick() de um botão no mundo VR.
    public void RestartScene()
    {
        // Obtém o nome da cena atual e a recarrega.
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        gameManager.reposition();
    }
}
