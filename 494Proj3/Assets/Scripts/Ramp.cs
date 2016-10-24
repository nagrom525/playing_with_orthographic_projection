using UnityEngine;
using System.Collections;

public class Ramp : MonoBehaviour {

    public GameObject pinBallInstance;
    public bool attachedPinball = false;
    public float posNegX;
    public float posPosX;
    public float posNegY;
    public float posPosY;

    World.WorldSideActive current_side;

	// Use this for initialization
	void Start () {
        World.S.OnSideChangeComplete += OnSideChanged;
        World.S.OnSideChangeStarted += OnSideStartingToChange;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnSideStartingToChange(World.WorldSideActive side) {

    }

    void OnSideChanged(World.WorldSideActive side) {
        current_side = side;
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Pinball") {
            attachedPinball = false;
            pinBallInstance = other.gameObject;
        }
    }

    void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "Pinball") {
            attachedPinball = false;
            pinBallInstance = null;
        }
    }
}
