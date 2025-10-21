using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private InputManager input;
    private void Start()
    {
        input = InputManager.Instance;
        input.PauzeGame.AddListener(PauseGame);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
    
