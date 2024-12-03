using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Tooltip("The rotation acceleration in degrees / second")]
    [SerializeField] private Vector2 acceleration;
    [Tooltip("The input rotation multiplier. Max speed in degrees / second. Set Y sensetivity to a negative value to invert Y")]
    [SerializeField] private Vector2 sensetivity;
    [Tooltip("The max angle from the horizon the player can rotate, in degrees")]
    [SerializeField] private float maxVerticalAngleFromHorizon;
    [Tooltip("The period to wait until resetting the input value. Set this as low as possible, without encountering stuttering")]
    [SerializeField] private float inputLagPeriod;

    private Vector2 velocity;
    private Vector2 rotation;
    private Vector2 lastInputEvent;
    private float inputLagTimer;

    private float ClampVerticalAngle(float angle)
    {
        return Mathf.Clamp(angle, -maxVerticalAngleFromHorizon, maxVerticalAngleFromHorizon);
    }

    private Vector2 GetInput()
    {
        // increment the lag timer
        inputLagTimer += Time.deltaTime;

        // get the input vector
        Vector2 input = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
            );

        if ((Mathf.Approximately(0, input.x) && Mathf.Approximately(0, input.y)) == false || inputLagTimer > inputLagPeriod)
        { 
            lastInputEvent = input;
            inputLagTimer = 0;
        }
        return lastInputEvent;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        Vector2 wantedVelocity = GetInput() * sensetivity;
        velocity = new Vector2(
            Mathf.MoveTowards(velocity.x, wantedVelocity.x, acceleration.x * Time.deltaTime),
            Mathf.MoveTowards(velocity.y, wantedVelocity.x, acceleration.x * Time.deltaTime));

        rotation += wantedVelocity * Time.deltaTime;
        rotation.y = ClampVerticalAngle(rotation.y);
        transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);
    }
}
