using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool _isPaused = false;
    public GameObject PauseMenuUI;
    private InputManager _input;
    private void Start()
    {
        //Time.timeScale = 0f;
        _input = InputManager.Instance;
        _input.PauzeGame.AddListener(TogglePause);
    }

    private void TogglePause()
    {
        if (_isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        PauseMenuUI.SetActive(true);
        _isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PauseMenuUI.SetActive(false);
        _isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MouseSensitivity()
    {

    }
}
    
