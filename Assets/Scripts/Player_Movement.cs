using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

	[SerializeField] private Tracker_Controls tracker;
	private Rigidbody rb;

	private void Start() {
		rb = GetComponent<Rigidbody> ();
	}

	private void LateUpdate() {
		Vector3 rotationDelta = tracker.rotationDelta;
	}

	private void Turn(float amount) {
		Vector3 f = new Vector3 ();
		rb.AddForce ();
	}

	private void AccelerateForwards(float amount) {

	}

	private void AccelerateUpDown(float amount) {

	}
}
