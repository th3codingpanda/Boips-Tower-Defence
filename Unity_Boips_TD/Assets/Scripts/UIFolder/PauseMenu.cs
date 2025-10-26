using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private bool _isPaused = false;
    public GameObject PauseMenuUI;
    private InputManager _input;
    public PlayerMovement playerMovement;
    [SerializeField] GameObject inputScript;
    //InputManager inputManager;

    //static PlayerInput playerInput; 
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
        PauseMenuUI.SetActive(true); //show pause menu
        _isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        //playerInput.SwitchCurrentActionMap("UI");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PauseMenuUI.SetActive(false); //hide pause menu
        _isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //playerInput.SwitchCurrentActionMap("Player");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit(); //quits game durr
    }

    public void MouseSensitivity()
    {
        //changes mouseSens float to slider value -> you can change mouse sensitivity

        playerMovement.mouseSensitivity = PauseMenuUI.GetComponentInChildren<UnityEngine.UI.Slider>().value;
    }
}
    
