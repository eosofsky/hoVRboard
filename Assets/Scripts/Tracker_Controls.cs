using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker_Controls : MonoBehaviour {

    const float dX = 0.0f;
    const float dY = 0.0f;
    const float dZ = 0.0f;

    const float roll = 0.0f;
    const float yaw = 90.0f;
    const float pitch = 90.0f;

	//private Vector3 lastRotation;
    //private GameObject hoverboard;

	//public Vector3 rotationDelta { get; private set; }

	//private void Start() {
		//lastRotation = transform.eulerAngles;
		//rotationDelta = Vector3.zero;
    //	}

	public Quaternion GetBoardRotation() {
		if (!gameObject.active) {
			return Quaternion.identity;
		}
		Quaternion delta_rotation = Quaternion.Euler(roll, yaw, pitch);
		Quaternion tracker_rotation = transform.rotation;
		return tracker_rotation * delta_rotation;
	}

	public Vector3 GetRight() {
		return transform.right;
	}

	public Vector3 GetForward() {
		return transform.forward;
	}

	/*private void Update() {
        Vector3 delta_displacement = new Vector3(dX, dY, dZ);
        Quaternion delta_rotation = Quaternion.Euler(roll, yaw, pitch);
        Vector3 tracker_position = transform.position;
        Quaternion tracker_rotation = transform.rotation;
        hoverboard.transform.rotation = tracker_rotation * delta_rotation;
        //hoverboard.transform.position = tracker_position + (tracker_rotation * delta_rotation) * delta_displacement;

        /* Save the change in rotation since the last frame */
        /*Vector3 newRotation = transform.eulerAngles;
		Vector3 newrotationDelta = newRotation - lastRotation;
		if (newrotationDelta.x > 180) {
			newrotationDelta.x -= 360;
		}
		if (newrotationDelta.x < -180) {
			newrotationDelta.x += 360; 
		}
		if (newrotationDelta.y > 180) {
			newrotationDelta.y -= 360;
		}
		if (newrotationDelta.y < -180) {
			newrotationDelta.y += 360; 
		}
		if (newrotationDelta.z > 180) {
			newrotationDelta.z -= 360;
		}
		if (newrotationDelta.z < -180) {
			newrotationDelta.z += 360; 
		}
		rotationDelta = newrotationDelta / 180;
		lastRotation = newRotation;
    }*/
}
