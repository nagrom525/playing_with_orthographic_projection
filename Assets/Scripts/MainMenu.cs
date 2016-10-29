using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public GameObject levelSelect;
    public GameObject assetSources;
    public GameObject LevelRestartMenu;

    public void Awake() {

    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            LevelRestartMenu.SetActive(true);
        }
    }

    public void LoadScene(int level) {
        SceneManager.LoadScene(level);
    }

    public void ShowAssetSources() {
        assetSources.SetActive(true);
    }

    public void CloseAssetSources() {
        assetSources.SetActive(false);
    }
}
