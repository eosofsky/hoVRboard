using System;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel2 : MonoBehaviour {

	[SerializeField]
	KeyCode SpeedUp = KeyCode.LeftShift;
	[SerializeField]
	KeyCode SpeedDown = KeyCode.C;
	[SerializeField]
	KeyCode Forward = KeyCode.W;
	[SerializeField]
	KeyCode Back = KeyCode.S;
	[SerializeField]
	KeyCode Left = KeyCode.A;
	[SerializeField]
	KeyCode Right = KeyCode.D;

	private KeyCode[] keyCodes;

	public static Action<PressedKeyCode[]> KeyPressed;
	private void Awake()
	{
		keyCodes = new[] {
			SpeedUp,
			SpeedDown,
			Forward,
			Back,
			Left,
			Right
		};

	}

	void FixedUpdate ()
	{
		var pressedKeyCode = new List<PressedKeyCode>();
		for (int index = 0; index < keyCodes.Length; index++)
		{
			var keyCode = keyCodes[index];
			if (Input.GetKey (keyCode)) {
				pressedKeyCode.Add ((PressedKeyCode)index);
			}
		}

		if (KeyPressed != null) {
			KeyPressed (pressedKeyCode.ToArray ());
		}
	}
}