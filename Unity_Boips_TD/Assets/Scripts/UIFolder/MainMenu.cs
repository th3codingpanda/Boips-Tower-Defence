using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        //Time.timeScale = 0f;
        //gameObject.SetActive(true);

        //freeze game to prevent game running in backgrount. if doesnt work maybe in another scene and then load into game.
    }
    public void StartGame()
   {
       SceneManager.LoadScene("TestingGrid"); //loads into game scene
    }

    public void QuitGame()
    {
         Application.Quit(); //quits game durr
    }
}
