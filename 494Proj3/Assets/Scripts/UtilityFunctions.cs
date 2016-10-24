using UnityEngine;
using System.Collections;

public class UtilityFunctions : MonoBehaviour {

    public static void SetRigidBodyMoving(Rigidbody rb) {
        rb.constraints = RigidbodyConstraints.None;
    }

    public static void SetRigidBodyNotMoving(Rigidbody rb) {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
