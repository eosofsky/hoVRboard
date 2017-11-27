using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
	private Rigidbody rb;

	public float TurnForce = 3f;
	public float ForwardForce = 10f;
	public float ForwardTiltForce = 20f;
	public float TurnTiltForce = 30f;
	public float EffectiveHeight = 100f;

	public float turnTiltForcePercent = 1.5f;
	public float turnForcePercent = 1.3f;

	private float _engineForce;
	public float EngineForce
	{
		get { return _engineForce; }
		set
		{
			_engineForce = value;
		}
	}

	private Vector2 hMove = Vector2.zero;
	private Vector2 hTilt = Vector2.zero;
	private float hTurn = 0f;
	public bool IsOnGround = true;

	void Start ()
	{
		ControlPanel2.KeyPressed += OnKeyPressed;
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{
		LiftProcess();
		MoveProcess();
		TiltProcess();
	}

	private void MoveProcess()
	{
		var turn = TurnForce * Mathf.Lerp(hMove.x, hMove.x * (turnTiltForcePercent - Mathf.Abs(hMove.y)), Mathf.Max(0f, hMove.y));
		hTurn = Mathf.Lerp(hTurn, turn, Time.fixedDeltaTime * TurnForce);
		rb.AddRelativeTorque(0f, hTurn * rb.mass, 0f);
		rb.AddRelativeForce(Vector3.forward * Mathf.Max(0f, hMove.y * ForwardForce * rb.mass));
	}

	private void LiftProcess()
	{
		var upForce = 1 - Mathf.Clamp(rb.transform.position.y / EffectiveHeight, 0, 1);
		upForce = Mathf.Lerp(0f, EngineForce, upForce) * rb.mass;
		rb.AddRelativeForce(Vector3.up * upForce);
	}

	private void TiltProcess()
	{
		hTilt.x = Mathf.Lerp(hTilt.x, hMove.x * TurnTiltForce, Time.deltaTime);
		hTilt.y = Mathf.Lerp(hTilt.y, hMove.y * ForwardTiltForce, Time.deltaTime);
		rb.transform.localRotation = Quaternion.Euler(hTilt.y, rb.transform.localEulerAngles.y, -hTilt.x);
	}

	private void OnKeyPressed(PressedKeyCode[] obj)
	{
		float tempY = 0;
		float tempX = 0;

		// stable forward
		if (hMove.y > 0)
			tempY = - Time.fixedDeltaTime;
		else
			if (hMove.y < 0)
				tempY = Time.fixedDeltaTime;

		// stable lurn
		if (hMove.x > 0)
			tempX = -Time.fixedDeltaTime;
		else
			if (hMove.x < 0)
				tempX = Time.fixedDeltaTime;


		foreach (var pressedKeyCode in obj)
		{
			switch (pressedKeyCode)
			{
			case PressedKeyCode.SpeedUpPressed:

				EngineForce += 0.1f;
				break;
			case PressedKeyCode.SpeedDownPressed:

				EngineForce -= 0.12f;
				if (EngineForce < 0) EngineForce = 0;
				break;

			case PressedKeyCode.ForwardPressed:

				if (IsOnGround) break;
				tempY = Time.fixedDeltaTime;
				break;
			case PressedKeyCode.BackPressed:

				if (IsOnGround) break;
				tempY = -Time.fixedDeltaTime;
				break;
			case PressedKeyCode.LeftPressed:

				if (IsOnGround) break;
				tempX = -Time.fixedDeltaTime;
				break;
			case PressedKeyCode.RightPressed:

				if (IsOnGround) break;
				tempX = Time.fixedDeltaTime;
				break;
			case PressedKeyCode.TurnRightPressed:
				{
					if (IsOnGround) break;
					var force = (turnForcePercent - Mathf.Abs(hMove.y))*rb.mass;
					rb.AddRelativeTorque(0f, force, 0);
				}
				break;
			case PressedKeyCode.TurnLeftPressed:
				{
					if (IsOnGround) break;

					var force = -(turnForcePercent - Mathf.Abs(hMove.y))*rb.mass;
					rb.AddRelativeTorque(0f, force, 0);
				}
				break;

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
