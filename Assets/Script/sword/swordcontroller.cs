using UnityEngine;

public class SwordController : MonoBehaviour
{
    public enum SwordState { Held, Thrown, Landed }

    [SerializeField] private Rigidbody rb;
    [SerializeField] private TrailRenderer trail;
    private Transform originalParent;
    public SwordState state = SwordState.Held;
    PlayerMovement playerMovementScript;

    void Start()
    {
        originalParent = transform.parent;
        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        if (trail != null) trail.emitting = false;
        playerMovementScript = GetComponentInParent<PlayerMovement>();
    }

    public void Throw(Vector3 direction, float force)
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.AddForce(direction * force, ForceMode.Impulse);
        state = SwordState.Thrown;
        if (trail != null) trail.emitting = true;
    }

    public void Retake()
    {
        rb.isKinematic = true;
        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        state = SwordState.Held;
        if (trail != null) trail.emitting = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state == SwordState.Thrown)
        {
            state = SwordState.Landed;
            rb.velocity = Vector3.zero;
        }
    }

    public bool IsHeld => state == SwordState.Held;

    void Update(){

        if(state == SwordState.Held && !playerMovementScript.isHoldingRight){
            //state = SwordState.Thrown;
            Throw(new Vector3(1, 0, 0),1f);
            
        }
    }
}