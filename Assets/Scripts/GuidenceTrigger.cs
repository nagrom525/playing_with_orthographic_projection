using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuidenceTrigger : MonoBehaviour {
    public Text guidenceText1;
    public Text guidenceText2;
    public GameObject guidencePanel1;
    public GameObject guidencePanel2;
    public string player_1_text;
    public string player_2_text;
    public string player_1_text_2p;
    public string player_2_text_2p;
    private int numPlayers;
	// Use this for initialization
	void Start () {
        numPlayers = MainMenu.numPlayers;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player") {
            PlayerControl pc = other.gameObject.GetComponent<PlayerControl>();
            if(pc != null) {
                if(numPlayers == 1) {
                    if (pc.type == CharacterType.CHARACTER_1) {
                        if (player_1_text != "") {
                            guidenceText1.text = player_1_text;
                            guidencePanel1.SetActive(true);
                        }
                    } else {
                        // then player 2 controls
                        if (player_2_text != "") {
                            guidenceText2.text = player_2_text;
                            guidencePanel2.SetActive(true);
                        }
                    }
                } else {
                    if (pc.type == CharacterType.CHARACTER_1) {
                        if (player_1_text_2p != "") {
                            guidenceText1.text = player_1_text_2p;
                            guidencePanel1.SetActive(true);
                        }
                    } else {
                        // then player 2 controls
                        if (player_2_text_2p != "") {
                            guidenceText2.text = player_2_text_2p;
                            guidencePanel2.SetActive(true);
                        }
                    }
                }
                
            }
        }
    }

    void OnCollisionEnter(Collision other) {
        print("meow");
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            PlayerControl pc = other.gameObject.GetComponent<PlayerControl>();
            if(pc != null) {
                if (pc.type == CharacterType.CHARACTER_1) {
                    guidenceText1.text = "";
                    guidencePanel1.SetActive(false);
                } else {
                    // then player 2
                    guidenceText2.text = "";
                    guidencePanel2.SetActive(false);
                }
            }
        }
    }
}
