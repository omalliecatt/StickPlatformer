using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class HighScoreButtonToHighScoreScene : MonoBehaviour {
    public void GotoHighscores() {
        SceneManager.LoadScene("ScoreScene");
    }
}
