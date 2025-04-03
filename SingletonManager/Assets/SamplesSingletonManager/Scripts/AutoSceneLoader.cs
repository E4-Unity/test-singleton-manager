using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eu4ng.Manager.Singleton.Sample
{
    internal class AutoSceneLoader : MonoBehaviour
    {
        void Start()
        {
            Invoke(nameof(LoadNextScene), 3);
        }

        [ContextMenu("Load Next Scene")]
        void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
