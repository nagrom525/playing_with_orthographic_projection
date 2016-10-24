using UnityEngine;
using System.Collections;

public class Ramp : MonoBehaviour {
    public enum RampDirection { X, Z};
    public RampDirection rampDirection = RampDirection.X;
    public GameObject pinBallInstance;
    public bool attachedPinball = false;
    public float posNegX;
    public float posPosX;
    public float posNegZ;
    public float posPosZ;
    public float continueRapPos;
    public bool endOfRamp = false;

    World.WorldSideActive current_side;

    void Awake() {
        
    }

	// Use this for initialization
	void Start () {
        World.S.OnSideChangeComplete += OnSideChanged;
        World.S.OnSideChangeStarted += OnSideStartingToChange;
        OnSideChanged(World.S.current_side);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnSideStartingToChange(World.WorldSideActive side) {
        if (attachedPinball) {
            Vector3 oldPosition = pinBallInstance.transform.position;
            switch (side) {
                case World.WorldSideActive.POS_X:
                    pinBallInstance.transform.position = new Vector3(oldPosition.x + posPosX, oldPosition.y, oldPosition.z);
                    break;
                case World.WorldSideActive.NEG_X:
                    pinBallInstance.transform.position = new Vector3(oldPosition.x + posNegX, oldPosition.y, oldPosition.z);
                    break;
                case World.WorldSideActive.POS_Z:
                    pinBallInstance.transform.position = new Vector3(oldPosition.x, oldPosition.y, oldPosition.z + posPosZ);
                    break;
                case World.WorldSideActive.NEG_Z:
                    pinBallInstance.transform.position = new Vector3(oldPosition.x, oldPosition.y, oldPosition.z + posNegZ);
                    break;
            }
        }
    }

   

    void OnSideChanged(World.WorldSideActive side) {
        current_side = side;
        if (attachedPinball) {
            if ((current_side == World.WorldSideActive.NEG_X || current_side == World.WorldSideActive.POS_X) && (rampDirection == RampDirection.Z)) {
                Rigidbody rb = pinBallInstance.GetComponent<Rigidbody>();
                UtilityFunctions.SetRigidBodyMoving(rb);

            } else if((current_side == World.WorldSideActive.NEG_Z || current_side == World.WorldSideActive.POS_Z) && (rampDirection == RampDirection.X)) {
                Rigidbody rb = pinBallInstance.GetComponent<Rigidbody>();
                UtilityFunctions.SetRigidBodyMoving(rb);
            } else {
                Rigidbody rb = pinBallInstance.GetComponent<Rigidbody>();
                UtilityFunctions.SetRigidBodyNotMoving(rb);
            }
        }
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Pinball") {
            attachedPinball = true;
            pinBallInstance = other.gameObject;
            OnSideChanged(current_side);
            World.S.TakeControlOfPinball(this.gameObject);
            World.S.ownershipSwap += OnOwnershipSwap;
        }
    }

    void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "Pinball") {
            attachedPinball = false;
            pinBallInstance = null;
            Vector3 oldPosition = other.transform.localPosition;
            if(rampDirection == RampDirection.X && endOfRamp) {
                if(current_side == World.WorldSideActive.POS_Z || current_side == World.WorldSideActive.NEG_Z) {
                    other.transform.localPosition = new Vector3(oldPosition.x, oldPosition.y, continueRapPos);
                }
            } else if(rampDirection == RampDirection.Z && endOfRamp){
                if (current_side == World.WorldSideActive.POS_X || current_side == World.WorldSideActive.NEG_X) {
                    other.transform.localPosition = new Vector3(continueRapPos, oldPosition.y, oldPosition.z);
                }
            }
            World.S.ownershipSwap -= OnOwnershipSwap;
        }
    }

    void OnOwnershipSwap(GameObject newOwner) {
        attachedPinball = false;
        pinBallInstance = null;
        World.S.ownershipSwap -= OnOwnershipSwap;
    }
}
