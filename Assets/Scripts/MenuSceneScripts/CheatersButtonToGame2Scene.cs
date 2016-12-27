using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatersButtonToGame2Scene : MonoBehaviour
{
    public void StartGame()
    {
        PlayerPrefs.SetInt("lives", 3);
        PlayerPrefs.SetInt("newScore", 0);
        SceneManager.LoadScene("Game2Scene");
    }
}
