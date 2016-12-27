using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreSceneToMenuScene : MonoBehaviour {
    public void MainMenu() {
        SceneManager.LoadScene("MenuScene");
    }
}
