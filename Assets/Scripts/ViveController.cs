using UnityEngine;

public class ViveController : MonoBehaviour {

    public delegate void TouchpadDown();
	public static event TouchpadDown OnTouchpadDown;

	public delegate void TriggerDown();
	public static event TriggerDown OnTriggerDown;

    private SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device Controller {
		get { return (trackedObj == null) ? null : SteamVR_Controller.Input ((int)trackedObj.index); }
	}

	private void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}
	
	private void Update () {
		if (Controller != null && Controller.GetAxis () != Vector2.zero) {
			// Finger on touchpad
		}

		if ((Controller != null && Controller.GetHairTriggerDown ()) || Input.GetKey(KeyCode.LeftShift)) {
			// Trigger pressed down
			if(OnTriggerDown != null) {
				OnTriggerDown();
			}
		}

		if (Controller != null && Controller.GetHairTriggerUp ()) {
			// Trigger released
		}

		if (Controller != null && Controller.GetPressDown (SteamVR_Controller.ButtonMask.Grip)) {
			// Grip button pressed
		}

		if (Controller != null && Controller.GetPressUp (SteamVR_Controller.ButtonMask.Grip)) {
			// Grip button released
		}

		if ((Controller != null && Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) || Input.GetKey(KeyCode.C)) {
            // Touchpad button pressed
			if (OnTouchpadDown != null) {
				OnTouchpadDown ();
			}
        }

		if (Controller != null && Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
            // Touchpad button released
        }

		if (Controller != null && Controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu)) {
			// Menu button pressed
		}
    }
}
