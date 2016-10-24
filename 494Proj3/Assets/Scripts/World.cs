using UnityEngine;
using System.Collections;

public enum Direction { LEFT, RIGHT, UP, DOWN};

public class World : MonoBehaviour {
    enum WorldState { NORMAL, ROTATING, RUNNING_LEVEL, PAUSED}
    enum WorldSideActive { NEG_X, POS_X, NEG_Y, POS_Y, NEG_Z, POS_Z}
    public static World S;
    public float roationTime;

    private WorldState current_state;


    void Awake() {
        if(S != null) {
            Debug.Log("Singleton can only be set once");
        } else {
            S = this;
        }
        current_state = WorldState.NORMAL;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
 
}
