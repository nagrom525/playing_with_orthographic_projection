using UnityEngine;
using System.Collections;

public class Pinball : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if(UtilityFunctions.ScreenBoundsCheck(this.GetComponent<SphereCollider>().bounds) != Vector3.zero) {
            GameCanvasNavigation.S.ReloadLevel();
        }
	}
}
