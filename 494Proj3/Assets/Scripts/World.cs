using UnityEngine;
using System.Collections;

public enum Direction { LEFT, RIGHT, UP, DOWN};

public class World : MonoBehaviour {
    public enum WorldState { NORMAL, ROTATING, RUNNING_LEVEL, PAUSED}
    public enum WorldSideActive { NEG_Z, POS_Z, NEG_X, POS_X}
    public static World S;
    public float roationTime;

    public delegate void WorldSideChanged(WorldSideActive state);
    public WorldSideChanged OnSideChangeComplete;
    public WorldSideChanged OnSideChangeStarted;

    private WorldState current_state;
    private WorldSideActive current_side;


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
        current_side =  retrieveCurrentSide(this.transform.rotation.eulerAngles);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartToChangeWorldState() {
        WorldSideActive sidePreview = retrieveCurrentSide(this.transform.rotation.eulerAngles);
        if(OnSideChangeStarted != null) {
            OnSideChangeStarted(sidePreview);
        }
    }

    public void ChangedWorldState() {
        current_side = retrieveCurrentSide(this.transform.rotation.eulerAngles);
        if(OnSideChangeStarted != null) {
            OnSideChangeStarted(current_side);
        }
    }

    private WorldSideActive retrieveCurrentSide(Vector3 rotation) {
        float rotationY = rotation.y;
        if(rotationY == 0 || rotationY == 360) {
            return WorldSideActive.NEG_Z;
        } else if(rotationY == 180 || rotationY == -180) {
            return WorldSideActive.POS_Z;
        } else if(rotationY == 270 || rotationY == -90) {
            return WorldSideActive.POS_X;
        } else if(rotationY == 90 || rotationY == -270) {
            return WorldSideActive.NEG_X;
        }else {
            Debug.Log("We got an incorrect angle");
            return WorldSideActive.POS_X;
        }
    }
}
