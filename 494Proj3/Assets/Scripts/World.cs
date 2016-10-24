using UnityEngine;
using System.Collections;

public enum Direction { LEFT, RIGHT, UP, DOWN};

public class World : MonoBehaviour {
    public enum WorldState { NORMAL, ROTATING, RUNNING_LEVEL, PAUSED}
    public enum WorldSideActive { NEG_Z, POS_Z, NEG_X, POS_X}
    public static World S;
    public float roationTime;

    public delegate void WorldSideChanged(WorldSideActive state);
    public delegate void OwnershipSwap(GameObject newOwner);
    public WorldSideChanged OnSideChangeComplete;
    public WorldSideChanged OnSideChangeStarted;
    public OwnershipSwap ownershipSwap;

    public WorldState current_state;
    public WorldSideActive current_side;


    void Awake() {
        if(S != null) {
            Debug.Log("Singleton can only be set once");
        } else {
            S = this;
        }
        current_state = WorldState.NORMAL;

        current_side = retrieveCurrentSide(this.transform.rotation.eulerAngles);


    }

	// Use this for initialization
	void Start () {
        if (OnSideChangeComplete != null) {
            OnSideChangeComplete(current_side);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public void StartToChangeWorldState() {
        WorldSideActive sidePreview = sidePreviewFromRange(this.transform.rotation.eulerAngles);
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
        int rotationY = Mathf.RoundToInt(rotation.y / 90) * 90;
        if (rotationY == 0 || rotationY == 360) {
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

    private WorldSideActive sidePreviewFromRange(Vector3 rotation) {
        float rotationY = rotation.y;
        switch (current_side) {
            case WorldSideActive.POS_X:
                if ((rotationY > 270 && rotationY < 360) || (rotationY > -90 && rotationY < 0)) {
                    // then we are changing to 0 / 360
                    return WorldSideActive.NEG_Z;
                    //} else if((rotationY < 270 && rotationY > 180) || (rotationY < -90 && rotationY > -180)){
                    //    // then we are changing to 180
                    //    return WorldSideActive.POS_Z;
                    //}
                } else {
                    return WorldSideActive.POS_Z;
                }
            case WorldSideActive.NEG_X:
                if ((rotationY > 90 && rotationY < 180) || (rotationY > -270 && rotationY < -180)) {
                    // then we are changing to 180
                    return WorldSideActive.POS_Z;
                    //} else if ((rotationY < 90 && rotationY > 0) || (rotationY < -270 && rotationY > -360)) {
                    //    // then we are changing to 0 / 360
                    //    return WorldSideActive.NEG_Z;
                    //}
                } else {
                    return WorldSideActive.NEG_Z;
                }
            case WorldSideActive.POS_Z:
                if ((rotationY > 180 && rotationY < 270) || (rotationY > -180 && rotationY < -90)) {
                    // then we are changing to 270 / -90
                    return WorldSideActive.POS_X;
                    //} else if ((rotationY < 180 && rotationY > 90) || (rotationY < -180 && rotationY > -270)) {
                    //    // then we are changing to 90 / -270
                    //    return WorldSideActive.NEG_X;
                    //}
                } else {
                    return WorldSideActive.NEG_X;
                }
            case WorldSideActive.NEG_Z:
                if ((rotationY > 0 && rotationY < 90) || (rotationY > -270 && rotationY < -360)) {
                    // then we are changing to 90 / -270
                    return WorldSideActive.NEG_X;
                }
                //} else if ((rotationY < 0 && rotationY > -90) || (rotationY < -360 && rotationY > -270)) {
                //    // then we are changing to 90 / -270
                //    return WorldSideActive.POS_X;
                //}
                else {
                    return WorldSideActive.POS_X;
                }
            default:
                Debug.Log("uh oh, something went wrong...");
                return WorldSideActive.NEG_X;
        }
        return WorldSideActive.NEG_X;
    }

    public void TakeControlOfPinball(GameObject newOwner) {
        if(ownershipSwap != null) {
            ownershipSwap(newOwner);
        }
    }
}
