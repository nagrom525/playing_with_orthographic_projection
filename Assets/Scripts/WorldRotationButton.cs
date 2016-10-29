using UnityEngine;
using System.Collections;

public class WorldRotationButton : MonoBehaviour {
    enum RotationButtonState { ROTATING_MOUSE, ROTATING_WORLD, NORMAL}
    public float speed = 1.0f;
    public float rotationCutoff = 25.0f;
    public float fullRotationTime = 1.0f;
    private AudioSource rotateAudioSrc;
    public AudioClip rotateAudioClip;

    /////////////////////////////////////////


    private float degreesY;
    private RotationButtonState current_state = RotationButtonState.NORMAL;
    private Vector3 prevBaseRotation;

    // handle the auto rotation after the user lets go of the wheel
    private Vector3 endAutoRotationValue;
    private Vector3 startAutoRotationValue;
    private float autoRotationStartTime;
    private float timeToAutoRotate;

    void Awake() {
        prevBaseRotation = this.transform.rotation.eulerAngles;
        rotateAudioSrc = this.GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start() {
        degreesY = transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update() {
        switch (current_state) {
            case RotationButtonState.NORMAL:
                NormalMouseListening();
                break;
            case RotationButtonState.ROTATING_MOUSE:
                NormalMouseListening();
                break;
            case RotationButtonState.ROTATING_WORLD:
                RotatingWorld();
                break;
        }
       
    }

    void NormalMouseListening() {
        if (Input.GetMouseButton(0) && (current_state == RotationButtonState.NORMAL || current_state == RotationButtonState.ROTATING_MOUSE)) {
            // case that the user is actively rotating the wheel
            Ray ray = new Ray();
            ray.origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ray.direction = Camera.main.transform.forward;
            
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                WorldRotationButton button = hit.collider.gameObject.GetComponent<WorldRotationButton>();
                if (button == this) {
                    // then the mouse key is clickd and it is also over the rotation button
                    current_state = RotationButtonState.ROTATING_MOUSE;
                    degreesY = this.transform.rotation.eulerAngles.y;
                    degreesY -= Input.GetAxis("Mouse X") * speed;
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, degreesY, 0);
                    World.S.transform.rotation = this.transform.rotation;
                    World.S.StartToChangeWorldState();
                }
            }
        } else if (current_state == RotationButtonState.ROTATING_MOUSE) {
            // case that the user lets go of the wheel
            current_state = RotationButtonState.ROTATING_WORLD;
            // we have to round the value back to the nerest rotation (only 4 possiable worlds)
            float rotationChange =  Mathf.DeltaAngle(this.transform.rotation.eulerAngles.y, prevBaseRotation.y);

            if (Mathf.Abs(rotationChange) < rotationCutoff) {
                // then send it back to the previous rotation
                endAutoRotationValue = prevBaseRotation;

            } else if(rotationChange >= rotationCutoff){
                // then find the direction to rotate it
                endAutoRotationValue.y = Mathf.Floor(transform.rotation.eulerAngles.y / 90) * 90;
            } else {
                endAutoRotationValue.y = Mathf.Ceil(transform.rotation.eulerAngles.y / 90) * 90;
            }
            rotateAudioSrc.clip = rotateAudioClip;
            rotateAudioSrc.Play();
          
            startAutoRotationValue = transform.rotation.eulerAngles;
            autoRotationStartTime = Time.time;
            prevBaseRotation = endAutoRotationValue;
            timeToAutoRotate = fullRotationTime * (Mathf.Abs(rotationChange) / 90.0f);
        }
    }

    void RotatingWorld() {
        // figure out which way to finish the rotation
        float t = (Time.time - autoRotationStartTime) / timeToAutoRotate;
        if (t > 1) {
            transform.rotation = Quaternion.Euler(endAutoRotationValue);
            current_state = RotationButtonState.NORMAL;
            World.S.transform.rotation = this.transform.rotation;
            World.S.ChangedWorldState();
        } else {
            Vector3 newRotation = Vector3.Lerp(startAutoRotationValue, endAutoRotationValue, t);
            transform.rotation = Quaternion.Euler(newRotation);
            World.S.transform.rotation = this.transform.rotation;
        }
    }
}
