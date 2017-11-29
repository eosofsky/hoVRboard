﻿using UnityEngine;
using UnityEngine.UI;

public class BoardController2 : MonoBehaviour
{
	[SerializeField] Tracker_Controls tracker;
    [SerializeField] GameObject board;
	private Rigidbody rb;

	public float TurnForce = 3f;
	public float ForwardForce = 10f;
	public float ForwardTiltForce = 20f;
	public float TurnTiltForce = 30f;
	public float EffectiveHeight = 100f;

	public float turnTiltForcePercent = 1.5f;
	public float turnForcePercent = 1.3f;

	public bool useTracker;

	private float _engineForce;
	public float EngineForce {
		get { return _engineForce; }
		set {
			_engineForce = value;
		}
	}

	private Vector2 hMove = Vector2.zero;
	private Vector2 hTilt = Vector2.zero;
	private float hTurn = 0f;
	public bool IsOnGround = true;

	void Start () {
		rb = GetComponent<Rigidbody> ();
        transform.rotation = tracker.GetBoardRotation();
    }

	void Update () {

	}

	void FixedUpdate() {
		//transform.rotation = tracker.GetBoardRotation ();
		SimulatePhysics ();
		LiftProcess();
		MoveProcess();
		TiltProcess();
	}

	private void MoveProcess() {
		var turn = TurnForce * Mathf.Lerp(hMove.x, hMove.x * (turnTiltForcePercent - Mathf.Abs(hMove.y)), Mathf.Max(0f, hMove.y));
		hTurn = Mathf.Lerp(hTurn, turn, Time.fixedDeltaTime * TurnForce);
		rb.AddRelativeTorque(0f, hTurn * rb.mass, 0f);
		rb.AddRelativeForce(Vector3.forward * Mathf.Max(0f, hMove.y * ForwardForce * rb.mass));
	}

	private void LiftProcess() {
		var upForce = 1 - Mathf.Clamp(rb.transform.position.y / EffectiveHeight, 0, 1);
		upForce = Mathf.Lerp(0f, EngineForce, upForce) * rb.mass;
		rb.AddRelativeForce(Vector3.up * upForce);
	}

	private void TiltProcess() {
        hTilt.x = Mathf.Lerp(hTilt.x, hMove.x * TurnTiltForce, Time.deltaTime);
        hTilt.y = Mathf.Lerp(hTilt.y, hMove.y * ForwardTiltForce, Time.deltaTime);
		board.transform.localRotation = Quaternion.Euler(hTilt.y, board.transform.localEulerAngles.y, -Mathf.Clamp(hTilt.x, 0.0f, 45.0f));
	}

	private void SimulatePhysics() {
		float tempY = 0;
		float tempX = 0;

		// stable forward
		if (hMove.y > 0)
			tempY = - Time.fixedDeltaTime;
		else if (hMove.y < 0)
			tempY = Time.fixedDeltaTime;

		// stable lurn
		if (hMove.x > 0)
			tempX = -Time.fixedDeltaTime;
		else if (hMove.x < 0)
				tempX = Time.fixedDeltaTime;


		if (Input.GetKey (KeyCode.LeftShift)) {
			/* Speed Up */
			EngineForce += 0.1f;
		}
		if ((!useTracker && Input.GetKey (KeyCode.W)) || (useTracker && tracker.GetForward().y < -6.0f)) {
            Debug.Log("Forward: " + tracker.GetForward().y);
			/* Go Forward */
			if (!IsOnGround) {
                tempY = Time.fixedDeltaTime;
			}
		}
		if ((!useTracker && Input.GetKey(KeyCode.A)) || (useTracker && tracker.GetRight().y > 0.1f)) {
            Debug.Log("Left: " + tracker.GetRight().y);
            /* Left */
            if (!IsOnGround) {
				tempX = -Time.fixedDeltaTime;
			}
		}
		if ((!useTracker && Input.GetKey(KeyCode.D)) || (useTracker && tracker.GetRight().y < -0.2f)) {
            Debug.Log("Right: " + tracker.GetRight().y);
            /* Right */
            if (!IsOnGround) {
				tempX = Time.fixedDeltaTime;
			}
		}
			
		hMove.x += tempX;
		hMove.x = Mathf.Clamp(hMove.x, -1, 1);

		hMove.y += tempY;
		hMove.y = Mathf.Clamp(hMove.y, -1, 1);

	}

	private void OnCollisionEnter()
	{
		IsOnGround = true;
	}

	private void OnCollisionExit()
	{
		IsOnGround = false;
	}
}