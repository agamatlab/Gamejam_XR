using UnityEngine;
using System.Collections;
public class SwordController : MonoBehaviour
{
    public enum SwordState { Held, Thrown, Landed }

    [SerializeField] private Rigidbody rb;
    [SerializeField] private TrailRenderer trail;
    private Transform originalParent;
    public SwordState state = SwordState.Held;
    PlayerMovement playerMovementScript;
    Transform playerBody;
    void Start()
    {
        originalParent = transform.parent;
        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        if (trail != null) trail.emitting = false;
        playerMovementScript = GetComponentInParent<PlayerMovement>();
        gameObject.layer = 3;
        playerBody = GameObject.Find("Player").transform;
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
        playerMovementScript.isHoldingRight = true;
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
    IEnumerator DelayedAction()
    {
        
        yield return new WaitForSeconds(0.4f);

        
        Throw(new Vector3(5, 2, 0), 1f);
    }

    bool check2DCollide(){
        float x1 = transform.position.x;
        float x2 = playerBody.position.x;
        float diff = x1-x2;
        if(diff < 1&& diff >-1){
            return true;
        }
        return false;
    }
    void Update()
    {

        if (state == SwordState.Held && !playerMovementScript.isHoldingRight)
        {

            StartCoroutine(DelayedAction());
            state = SwordState.Thrown;
        }
        if(state == SwordState.Landed && check2DCollide()){
            Retake();
        }
    }
}