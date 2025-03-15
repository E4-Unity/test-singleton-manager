using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    [ContextMenu("Load Next Scene")]
    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
