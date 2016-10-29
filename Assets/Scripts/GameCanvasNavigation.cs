using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvasNavigation : MonoBehaviour {
    public GameObject nextSceneButton;
    public GameObject restartPanel;
    public Text youWonText;
    private int sceneIndex;
    private int numScenes;

    void Awake() {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        numScenes = SceneManager.sceneCountInBuildSettings;
    }
	// Use this for initialization
	void Start () {
        // then we are at the last scene
	    if(sceneIndex == (numScenes - 1)) {
            youWonText.text = "Congrats!\n You made it to the end of the game!";
            nextSceneButton.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadNextScene() {
        SceneManager.LoadScene(sceneIndex + 1);
    }

    public void LoadMainMenu() {
        CloseWindow();
        SceneManager.LoadScene(0);
    }

    public void ReloadLevel() {
        CloseWindow();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CloseWindow() {
        Time.timeScale = 1;
        restartPanel.SetActive(false);
    }

    public void ExitGame() {
        CloseWindow();
        Application.Quit();
    }
}
