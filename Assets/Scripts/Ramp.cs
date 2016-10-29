using UnityEngine;
using System.Collections;

public class Ramp : WorldRotationObject {
    public enum RampDirection { X, Z};
    public RampDirection rampDirection = RampDirection.X;
   
    void Awake() {}

    protected override void OnSideChanged(World.WorldSideActive side) {
        base.OnSideChanged(side);
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
}
