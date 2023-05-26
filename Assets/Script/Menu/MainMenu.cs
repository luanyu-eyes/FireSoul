using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene(1,LoadSceneMode.Single);
        }
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
