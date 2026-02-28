using UnityEngine;

public class GrabReleaseNotifier : MonoBehaviour
{
    [Tooltip("Referência para o manager global que controla o movimento magnético")]
    public MagneticRelease magneticManager;

    /// <summary>
    /// Chamado quando o objeto é solto.
    /// </summary>
    public void NotifyRelease()
    {
        if (magneticManager != null)
        {
            magneticManager.OnReleased(gameObject);
        }
        else
        {
            Debug.LogWarning($"{name}: MagneticRelease não atribuído no GrabReleaseNotifier!");
        }
    }
}
