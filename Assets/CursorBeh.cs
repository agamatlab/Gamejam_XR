using UnityEngine;

public class ClockSwingCursor : MonoBehaviour
{
    [Header("Set up references")]
    public Transform pivot;              // The center point of the rotation
    public Transform cursor;             // The swinging object
    public Transform targetMarker;       // Target indicator

    [Header("Swing settings")]
    public float limitAngle = 60f;       // Max angle (in degrees) left/right from center
    public float swingSpeed = 60f;       // Degrees per second
    public float radius = 2f;            // Distance from pivot to the cursor/target

    public Collider cursorCollider;
    public Collider targetCollider;

    private float currentAngle = 0f;
    private bool swingingRight = true;
    private float targetAngle;
    public ParticleSystem SuccessParticles;

    void Start()
    {
        // Pick a random target angle within range
        targetAngle = Random.Range(-limitAngle, limitAngle);

        // Place the target marker at that angle
        PositionAtAngle(targetMarker, targetAngle);
    }

    void Update()
    {
        float delta = swingSpeed * Time.deltaTime;

        if (swingingRight)
        {
            currentAngle += delta;
            if (currentAngle >= limitAngle)
            {
                currentAngle = limitAngle;
                swingingRight = false;
            }
        }
        else
        {
            currentAngle -= delta;
            if (currentAngle <= -limitAngle)
            {
                currentAngle = -limitAngle;
                swingingRight = true;
            }
        }

        // Move the cursor along the circle
        PositionAtAngle(cursor, currentAngle);
    }

    void PositionAtAngle(Transform t, float angleDegrees)
    {
        // Convert angle to radians
        float radians = angleDegrees * Mathf.Deg2Rad;

        // Position on a circle in the X-Y plane
        Vector3 offset = new Vector3(Mathf.Sin(radians), Mathf.Cos(radians), 0f) * radius;

        t.position = pivot.position + offset;

        // Optional: rotate the object to face outward
        t.rotation = Quaternion.Euler(0f, 0f, -angleDegrees);
    }
    
    public bool IsCursorInTargetArea()
    {
        if (cursorCollider.bounds.Intersects(targetCollider.bounds))
        {
            Debug.Log("Playing particles");
            SuccessParticles.Play();
            return true;
        }
        return false;
    }

}
