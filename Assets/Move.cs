using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 newpos = transform.position;
		float time = Time.deltaTime * 10;

		if (Input.GetKey (KeyCode.LeftArrow)) {
			newpos.x = transform.position.x - time;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			newpos.x = transform.position.x + time;
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			newpos.z = transform.position.z + time;
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			newpos.z = transform.position.z - time;
		}

		transform.position = newpos;
	}

	
}
