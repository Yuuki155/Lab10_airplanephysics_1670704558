using UnityEngine;
using UnityEngine.InputSystem;

public class AirplaneFlightPhysicsSimulation : MonoBehaviour
{
    public float thrust;
    public float liftCoefficient;
    Rigidbody rb;
    bool engineOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.6f, -0.2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        if (kb.spaceKey.isPressed)
        {
            engineOn = true;
            rb.AddRelativeForce(Vector3.forward * thrust, ForceMode.Acceleration);
        }
        float forwardSpeed = Vector3.Dot(rb.linearVelocity, transform.forward);

        if (engineOn && forwardSpeed > 5)
        {
            float lift = forwardSpeed * forwardSpeed * liftCoefficient;
            float pitchAngle = Vector3.Angle(transform.forward, Vector3.ProjectOnPlane(transform.forward, Vector3.up));

            if (pitchAngle > stallAngle)
            {
                lift *= stallLiftMultiplier;
            }

            rb.AddForce(transform.up * lift, ForceMode.Acceleration);

            Debug.DrawRay(transform.position, transform.up * 5f, Color.green);
        }

    }
}
