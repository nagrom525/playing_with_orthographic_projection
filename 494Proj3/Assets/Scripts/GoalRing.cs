using UnityEngine;
using System.Collections;

public enum Direction2D { LEFT, RIGHT };

public class GoalRing : MonoBehaviour {
    public Direction2D turnDirection;
    public float secondsToTurn = 1.0f;
    public bool turn = true;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        float amountToTurn = (Time.deltaTime / secondsToTurn) * 360;
        Vector3 oldRotation = this.transform.rotation.eulerAngles;
        if (turn) {
            if (turnDirection == Direction2D.LEFT) {
                this.transform.rotation = Quaternion.Euler(new Vector3(oldRotation.x, oldRotation.y, oldRotation.z - amountToTurn));
            } else {
                // then turn right
                this.transform.rotation = Quaternion.Euler(new Vector3(oldRotation.x, oldRotation.y, oldRotation.z + amountToTurn));
            }
        }
    }
}
