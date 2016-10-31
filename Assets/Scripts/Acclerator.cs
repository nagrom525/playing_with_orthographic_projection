using UnityEngine;
using System.Collections;

public class Acclerator : MonoBehaviour {
   public  World.WorldSideActive sideArrowActive;
   public World.WorldSideActive currentSideActive;
   public float force;
   public Vector3 direction;

    private bool triggered;
    private SpriteRenderer spriteRend;

	// Use this for initialization
	void Start () {
        spriteRend = this.GetComponent<SpriteRenderer>();
        World.S.OnSideChangeStarted += OnSideChangeStarted;
        World.S.OnSideChangeComplete += OnSideChanged;
    }


    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Pinball") {
            if (!triggered && sideArrowActive == currentSideActive) {
                triggered = true;
                AcceleratePlayer(other.gameObject);
            }

        }
    }

  void OnTriggerExit(Collider other) {
        triggered = false;
    }



    void AcceleratePlayer(GameObject player) {
        UtilityFunctions.SetRigidBodyMoving(player.GetComponent<Rigidbody>());
        player.GetComponent<Rigidbody>().AddForce(force * direction);
    }

   void OnSideChanged(World.WorldSideActive side) {
 
        currentSideActive = side;
        if (currentSideActive == sideArrowActive) {
            spriteRend.enabled = true;
        } else {
            spriteRend.enabled = false;
        }
    }

    void OnSideChangeStarted(World.WorldSideActive side) {
        if (currentSideActive == sideArrowActive) {
            spriteRend.enabled = true;
        } else {
            spriteRend.enabled = false;
        }
        print(side);
    }
}
