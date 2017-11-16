using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker_Controls : MonoBehaviour {

	private Vector3 lastRotation;
	[SerializeField] private GameObject hoverboard;

	public Vector3 rotationDelta { get; private set; }

	private void Start() {
		Align();
		lastRotation = transform.eulerAngles;
		rotationDelta = Vector3.zero;
	}

	private void Update() {
		/* Save the change in rotation since the last frame */
		Vector3 newRotation = transform.eulerAngles;
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
		rotationDelta = newrotationDelta;
		lastRotation = newRotation;
	}

	private void Align() {
		/* Align tracker so that its forward position matches
		 * that of the board */
		hoverboard.transform.parent = null;
		transform.forward = hoverboard.transform.up;
		hoverboard.transform.parent = this.transform;
	}
}
