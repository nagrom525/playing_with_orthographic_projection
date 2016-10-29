using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuidenceTrigger : MonoBehaviour {
    public Text guidenceText;
    public GameObject guidencePanel;
    public string guidenceQuote;
	
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Pinball") {
            if(guidenceQuote != "") {
                guidenceText.text = guidenceQuote;
                guidencePanel.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Pinball") {
            if(guidenceQuote != "") {
                guidenceText.text = "";
                guidencePanel.SetActive(false);
            }
        }
    }
}
