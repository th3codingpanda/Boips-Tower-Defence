using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseUi : MonoBehaviour
{
    private bool _isPaused = false;
    public GameObject LoseUI;
    public GameObject WinUI;
    //private InputManager _input;
    //public PlayerMovement playerMovement;
    private void Start()
    {
       
        //_input = InputManager.Instance;
        //_input.PauzeGame.AddListener(TogglePause);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    private void TogglePause()
    {
        if (_isPaused)
        {
            ContinueGame();
        }
        else
        {
            winGame();
        }
    }

    public void LoseGame()
    {
            LoseUI.SetActive(true);
            //Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
    } 

    public void winGame()
    {
        WinUI.SetActive(true);
        //Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
        Debug.Log("Load Game Scene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    //public void PauseGame()
    //{
    //    Time.timeScale = 0f;
    //    LoseUI.SetActive(true);
    //    _isPaused = true;

    //    Cursor.lockState = CursorLockMode.None;
    //    Cursor.visible = true;
    //}

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        WinUI.SetActive(false);
        _isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

  
}
