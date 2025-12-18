using UnityEngine;
using EasyPeasyFirstPersonController;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausemenu;
    public bool paused;

    public string menuSceneName;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            if (paused == true)
            {
                pausemenu.SetActive(true);

                // Stop player movement
                FirstPersonController.Instance.SetControl(false);
                FirstPersonController.Instance.SetCursorVisibility(true);

                Time.timeScale = 0f; // Stops every physics and animatiopns
                AudioListener.pause = true; // Stops audio
            }
            else
            {
                pausemenu.SetActive(false);

                // Resume player movement and game
                FirstPersonController.Instance.SetControl(true);
                FirstPersonController.Instance.SetCursorVisibility(false);
                AudioListener.pause = false;
                Time.timeScale = 1f;
            }
        }
    }

    public void ResumeGame()
    {
        pausemenu.SetActive(false);

        // Resume player movement and game
        FirstPersonController.Instance.SetControl(true);
        FirstPersonController.Instance.SetCursorVisibility(false);
        AudioListener.pause = false;
        Time.timeScale = 1f;

        paused = false;
    }

    public void BackToMainMenu()
    {
        //SceneManager.LoadScene(menuSceneName);
        FindFirstObjectByType<SceneFader>().FadeToScene(menuSceneName);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}
