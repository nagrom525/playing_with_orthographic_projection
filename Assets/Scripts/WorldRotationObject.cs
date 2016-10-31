using UnityEngine;
using System.Collections;

public class WorldRotationObject : MonoBehaviour {
    public float posNegX = float.MaxValue;
    public float posPosX = float.MaxValue;
    public float posNegZ = float.MaxValue;
    public float posPosZ = float.MaxValue;
    public float continuePosX;
    public float continuePosZ;
    public bool continueActive = false;
    public bool adjustPosOnRotateActive = false;
    public GameObject pinBallInstance;
    public bool attachedPinball = false;
    protected World.WorldSideActive current_side;

    // Use this for initialization
    protected virtual void Start () {
        World.S.OnSideChangeComplete += OnSideChanged;
        World.S.OnSideChangeStarted += OnSideStartingToChange;
        OnSideChanged(World.S.current_side);
    }
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}

    protected virtual void OnSideStartingToChange(World.WorldSideActive side) {
        if (attachedPinball && adjustPosOnRotateActive) {
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

    protected virtual void OnSideChanged(World.WorldSideActive side) {
        current_side = side;
    }

    private void PinballControl(GameObject gameObject) {
        if (gameObject.tag == "Pinball") {
            attachedPinball = true;
            pinBallInstance = gameObject;
            OnSideChanged(current_side);
            World.S.TakeControlOfPinball(this.gameObject);
            World.S.ownershipSwap += OnOwnershipSwap;
        }
    }

    private void PinballRelease(GameObject gameObject) {
        if (gameObject.tag == "Pinball") {
            attachedPinball = false;
            pinBallInstance = null;
            Vector3 oldPosition = gameObject.transform.localPosition;
            // if continue on rotation
            if (continueActive) {
                print("Exiting: " + this.gameObject.name);
                if (current_side == World.WorldSideActive.POS_Z || current_side == World.WorldSideActive.NEG_Z) {
                    gameObject.transform.localPosition = new Vector3(oldPosition.x, oldPosition.y, continuePosZ);
                } else {
                    gameObject.transform.localPosition = new Vector3(continuePosX, oldPosition.y, oldPosition.z);
                }
            }
            World.S.ownershipSwap -= OnOwnershipSwap;
        }
    }

    protected virtual void OnCollisionEnter(Collision other) {
        PinballControl(other.gameObject);
    }

    protected virtual void OnCollisionExit(Collision other) {
        PinballRelease(other.gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other) {
        PinballControl(other.gameObject);
    }

    protected virtual void OnTriggerExit(Collider other) {
        PinballRelease(other.gameObject);
    }

    protected virtual void OnOwnershipSwap(GameObject newOwner) {
        attachedPinball = false;
        pinBallInstance = null;
        World.S.ownershipSwap -= OnOwnershipSwap;
    }



}
