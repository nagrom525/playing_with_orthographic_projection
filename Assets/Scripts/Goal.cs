using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {
    public AudioClip levelCompleteAudio;
    public GameObject outterRing;
    public GameObject middleRing;
    public GameObject levelCompletePanel;
    public GameObject innerRing;
    private AudioSource levelCompleteSrc;
    private GameObject pinBallInstance;

    void Awake() {
        levelCompleteSrc = this.GetComponent<AudioSource>();
        levelCompleteSrc.clip = levelCompleteAudio;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Pinball") {
            // then the player has reached the goal
            levelCompletePanel.SetActive(true);
            levelCompleteSrc.Play();
            UtilityFunctions.SetRigidBodyNotMoving(other.gameObject.GetComponent<Rigidbody>());
        }
    }

    void turnOffRingRotation(GameObject ring) {
        GoalRing goalRing = ring.GetComponent<GoalRing>();
        goalRing.turn = false;
    }

    public void restartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}