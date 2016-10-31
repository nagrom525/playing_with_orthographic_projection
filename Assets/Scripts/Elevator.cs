using UnityEngine;
using System.Collections;


public class Elevator : WorldRotationObject {
    public enum MoveableDirection { X, Y, Z}
    public MoveableDirection moveableDirection = MoveableDirection.X;
    public float maxDistance;
    public float minDistance;
    public LayerMask elevatorLayerMask;


    protected override void Start() {
        base.Start();
        switch (moveableDirection) {
            case MoveableDirection.X:
                maxDistance += transform.position.x;
                minDistance += transform.position.x;
                break;
            case MoveableDirection.Y:
                maxDistance += transform.position.y;
                minDistance += transform.position.y;
                break;
            case MoveableDirection.Z:
                maxDistance += transform.position.z;
                maxDistance += transform.position.z;
                break;
        }
    }
    // Update is called once per frame
    protected override void Update () {
        base.Update();
        if (Input.GetMouseButton(0)) {
            // case that the user is actively rotating the wheel
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ray.origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ray.direction = Camera.main.transform.forward;
            RaycastHit[] hit = Physics.RaycastAll(ray, 1000000.0f, elevatorLayerMask);
            if (hit.Length != 0) {
                bool elevatorHit = false;
                for(int i = 0; i < hit.Length; ++i) {
                    if(hit[i].collider.gameObject.GetComponent<Elevator>() == this) {
                        elevatorHit = true;
                        break;
                    }
                }
                if (elevatorHit) {
                    Vector3 mousePosition = Input.mousePosition;
                    mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    Vector3 movePosition = this.transform.position;
                    if (moveableDirection == MoveableDirection.X) {
                        if (World.S.current_side != World.WorldSideActive.POS_X && World.S.current_side != World.WorldSideActive.NEG_X) {
                            if (mousePosition.x < maxDistance && mousePosition.x > minDistance)
                                movePosition = new Vector3(mousePosition.x, this.transform.position.y, this.transform.position.z);
                        }
                    } else if (moveableDirection == MoveableDirection.Y) {
                        if (mousePosition.y < maxDistance && mousePosition.y > minDistance) {
                            movePosition = new Vector3(this.transform.position.x, mousePosition.y, this.transform.position.z);
                        }
                    } else {
                        if (World.S.current_side != World.WorldSideActive.POS_Z && World.S.current_side != World.WorldSideActive.NEG_Z) {
                            if (mousePosition.z < maxDistance && mousePosition.z > minDistance) {
                                movePosition = new Vector3(this.transform.position.z, this.transform.position.y, mousePosition.z);
                            }
                        }
                    }
                    Vector3 oldPosition = this.transform.position;
                    this.transform.position = movePosition;
                    if (attachedPinball) {
                        pinBallInstance.transform.position += (movePosition - oldPosition);
                    }
                }
            }
        }
    }

    protected override void OnCollisionEnter(Collision other) {
        base.OnCollisionEnter(other);
        if(other.gameObject.tag == "Pinball") {
            UtilityFunctions.SetRigidBodyNotMoving(other.gameObject.GetComponent<Rigidbody>());
            UtilityFunctions.SetRigidBodyMoving(other.gameObject.GetComponent<Rigidbody>());
        }
    }

}
