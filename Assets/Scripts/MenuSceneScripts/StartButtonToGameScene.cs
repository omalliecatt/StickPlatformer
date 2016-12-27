using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartButtonToGameScene : MonoBehaviour {
    public void StartGame() {
        SceneManager.LoadScene("GameScene");
    }
}
