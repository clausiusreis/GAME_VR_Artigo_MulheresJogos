using UnityEngine;
using UnityEngine.SceneManagement;

public class HMDPresenceListener : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnEnable()
    {
        OVRManager.HMDMounted += OnHMDMounted;
        OVRManager.HMDUnmounted += OnHMDUnmounted;
    }

    private void OnDisable()
    {
        OVRManager.HMDMounted -= OnHMDMounted;
        OVRManager.HMDUnmounted -= OnHMDUnmounted;
    }

    private void OnHMDMounted()
    {
        // Óculos COLOCADO novamente
        //gameManager.OnHeadsetWorn();

        // Obtém o nome da cena atual e a recarrega.
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        gameManager.reposition();

        Time.timeScale = 1f;
    }

    private void OnHMDUnmounted()
    {
        // Óculos REMOVIDO
        //gameManager.OnHeadsetRemoved();
        // Obtém o nome da cena atual e a recarrega.
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        gameManager.reposition();

        Time.timeScale = 1f;
    }
}
