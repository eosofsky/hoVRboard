using UnityEngine;

public class Player_Movement : MonoBehaviour {

	[SerializeField] private Tracker_Controls tracker;
	private Rigidbody rb;
 
    private const float maxVelocity = 10.0f;
    private const float rightLeftAmplifier = 0.1f;
    private const float upDownAmplifier = 1.0f;
    private const float speedUpAmplifier = 0.1f;
    private const float slowDownAmplifier = 1.0f;

    [SerializeField] GameObject hoverboard;

    public bool poweredOn { get; set; }

    /* Physics 
    Vector3 accel;
    Vector3 vel = new Vector3(0.01f, 0.01f, 0.01f);
    Vector3 decay = new Vector3(1.01f, 1.01f, 1.01f);
    Vector3 resist = new Vector3(1.0f, 1.0f, 1.0f);
    */
    private enum Component {
        X,
        Y,
        Z
    }

	private void Start() {
		//rb = hoverboard.GetComponent<Rigidbody> ();
        //rb.detectCollisions = false;
        poweredOn = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P))
        {
            poweredOn = !poweredOn;
            if (poweredOn)
            {
                Debug.Log("Powered On");
            }
            else
            {
                Debug.Log("Powered Off");
            }
        }
        //Debug.DrawLine(hoverboard.transform.position, hoverboard.transform.localPosition + 100 * hoverboard.transform.right, Color.black);
        //l1.SetPosition(0, transform.position);
        //l1.SetPosition(1, transform.position + transform.forward * 10);
    }

    private void FixedUpdate() {

        //accel = new Vector3();
        if (!poweredOn) {
            return;
        }

        if (Input.GetKey(KeyCode.R)) {
            //Debug.Log("Speed Up");
            SpeedUp(Vector3.one, Component.Y);
        }
        if (Input.GetKey(KeyCode.E)) {
            //Debug.Log("Slow Down");
            //SlowDown(Vector3.one, Component.Y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            RightLeft(Vector3.one, Component.X);
        }

        Vector3 rotationDelta = tracker.rotationDelta;
        //RightLeft(hoverboard.transform.right, Component.Y);
        //UpDown(rotationDelta, Component.Y);

        /* Don't go backwards */
        /////float angleDiff = Vector3.Angle(hoverboard.transform.forward, rb.velocity);
        //if (angleDiff > 90.0f) {
        //    Vector3 clampedVelocity = rb.velocity;
        //    clampedVelocity.x = 0.0f;
        //    clampedVelocity.z = 0.0f;
            //rb.velocity = clampedVelocity;
        //}
        /*
        vel += accel * Time.deltaTime;
        vel -= decay * Time.deltaTime;
        accel = new Vector3(accel.x / decay.x, accel.y / decay.y, accel.z / decay.z);
        if (vel.magnitude > 0.01f)
        {
            transform.position += vel * Time.deltaTime;
        }
        */
        //clampedVelocity.x = Mathf.Clamp(rb.velocity.x, 0.0f, maxVelocity);
        //clampedVelocity.y = Mathf.Clamp(rb.velocity.y, 0.0f, maxVelocity);
        //clampedVelocity.z = Mathf.Clamp(rb.velocity.z, 0.0f, maxVelocity);
        ///rb.velocity = clampedVelocity;
	}

    private Vector3 GetComponent(Vector3 vector, Component c) {
        switch (c) {
            case Component.X:
                return new Vector3(vector.x, 0.0f, 0.0f);
                break;
            case Component.Y:
                return new Vector3(0.0f, vector.y, 0.0f);
                break;
            case Component.Z:
                return new Vector3(0.0f, 0.0f, vector.z);
                break;
            default:
                return Vector3.zero;
                break;
        }
    }

    private float ExtractComponent(Vector3 vector, Component c) {
        switch (c) {
            case Component.X:
                return vector.x;
                break;
            case Component.Y:
                return vector.y;
                break;
            case Component.Z:
                return vector.z;
                break;
            default:
                return 0.0f;
                break;
        }
    }

    private Vector3 SetComponent(Vector3 vector, Component from, Component to) {
        float fromFloat = 0.0f;
        switch (from) {
            case Component.X:
                fromFloat = vector.x;
                break;
            case Component.Y:
                fromFloat = vector.y;
                break;
            case Component.Z:
                fromFloat = vector.z;
                break;
            default:
                break;
        }

        switch (to) {
            case Component.X:
                return new Vector3(fromFloat, 0.0f, 0.0f);
                break;
            case Component.Y:
                return new Vector3(0.0f, fromFloat, 0.0f);
                break;
            case Component.Z:
                return new Vector3(0.0f, 0.0f, fromFloat);
                break;
            default:
                return Vector3.zero;
                break;
        }
    }

    private Vector3 SetComponent(float fromFloat, Component to) {
        switch (to) {
            case Component.X:
                return new Vector3(fromFloat, 0.0f, 0.0f);
                break;
            case Component.Y:
                return new Vector3(0.0f, fromFloat, 0.0f);
                break;
            case Component.Z:
                return new Vector3(0.0f, 0.0f, fromFloat);
                break;
            default:
                return Vector3.zero;
                break;
        }
    }

    private void RightLeft(Vector3 f, Component c) {
        Vector3 right = hoverboard.transform.right;
        float amount = Mathf.RoundToInt(right.y * 10) / 10.0f;
        right.y = 0.0f;
        Vector3 pos = hoverboard.transform.position;
        if (amount != 0.0f)
        {
            pos = pos + rightLeftAmplifier * right * -1 * amount / Mathf.Abs(amount);
            hoverboard.transform.position = pos;
        }
        //if (amount != 0.0f)
        //{
        //    rb.AddForce(rightLeftAmplifier * right * -1 * amount / Mathf.Abs(amount));
        //}
        //Debug.Log(amount);
        //Debug.DrawLine(rb.gameObject.transform.position, rb.gameObject.transform.position + 100 * right, Color.blue);
        /*float amount = rb.gameObject.transform.right.y;
        Vector3 up = rb.gameObject.transform.up;
        Vector3 sideDir = Vector3.Cross(-1 * amount / Mathf.Abs(amount) * rb.gameObject.transform.up, rb.velocity);
        Debug.DrawLine(rb.gameObject.transform.position, rb.gameObject.transform.position + 100 * up, Color.red);
        Debug.DrawLine(rb.gameObject.transform.position, rb.gameObject.transform.position + 100 * rb.velocity, Color.black);
        sideDir.y = 0.0f;
        Debug.DrawLine(rb.gameObject.transform.position, rb.gameObject.transform.position + 100 * sideDir, Color.blue);
        Debug.Log(sideDir);*/
       
        //rb.AddTorque(hoverboard.transform.up * sideDir.magnitude * 0.01f);
        //rb.AddForce(ExtractComponent(f, c) * hoverboard.transform.forward);
        //rb.AddForce(sideDir * rightLeftAmplifier);
        //Vector3 right = hoverboard.transform.right;
        //float amount = right.y;
        //right.y = 0.0f;
        //transform.Rotate(GetComponent(hoverboard.transform.position, Component.Y), -1 * amount);
        //Vector3 rightLeft = rightLeftAmplifier * amount * -1 * right;
        //Vector3 forward = Vector3.Dot(rb.velocity, hoverboard.transform.forward.normalized) * hoverboard.transform.forward;
        //Vector3 up = Vector3.Dot(rb.velocity, hoverboard.transform.up) * hoverboard.transform.up;
        //rb.velocity = rightLeft + forward;// + up;
    }
    
	private void SpeedUp(Vector3 f, Component c) {
        Vector3 forward = hoverboard.transform.forward;
        forward.y = 0.0f;
        //accel += speedUpAmplifier * ExtractComponent(f, c) * forward;
        Vector3 pos = hoverboard.transform.position;
        pos = pos + speedUpAmplifier * forward;
        hoverboard.transform.position = pos;
        //rb.AddForce(speedUpAmplifier * ExtractComponent(f,c) * forward);
    }

    private void SlowDown(Vector3 f, Component c) {
        //   if (rb.velocity.Equals(Vector3.zero)) {
        //     return;
        // }
        Vector3 forward = -1 * hoverboard.transform.forward;
        forward.y = 0.0f;
        //accel += speedUpAmplifier * ExtractComponent(f, c) * forward;
        rb.AddForce(slowDownAmplifier * ExtractComponent(f, c) * forward);
    }

    private void UpDown(Vector3 f, Component c) {
        rb.AddForce(upDownAmplifier * SetComponent(f, c, Component.Y));
    }
}
