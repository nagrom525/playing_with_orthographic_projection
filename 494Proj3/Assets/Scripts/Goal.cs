using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
    public AudioClip levelCompleteAudio;
    public GameObject outterRing;
    public GameObject middleRing;
    public GameObject innerRing;
    private AudioSource levelCompleteSrc;
    private GameObject pinBallInstance;

    void Awake() {
        levelCompleteSrc = this.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Pinball") {
            // then the player has reached the goal
        }
    }

    void turnOffRingRotation(GameObject ring) {
        GoalRing goalRing = ring.GetComponent<GoalRing>();
        goalRing.turn = false;
    }
}