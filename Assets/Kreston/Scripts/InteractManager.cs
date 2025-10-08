using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

[ExecuteAlways]
public class InteractManager : MonoBehaviour
{
    [SerializeField] private float interactRange;
    private CircleCollider2D interactCollider;
    private bool nearbyDialogue;
    private bool nearbyCollectable;
    private bool nearbyPurchaseable;

    private void Awake() =>interactCollider = GetComponent<CircleCollider2D>();
    private void Reset() => interactCollider = GetComponent<CircleCollider2D>();

    private void Update()
    {
        interactCollider.radius = interactRange;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (nearbyDialogue)
                onInteractDialogue.Invoke();
            else if (nearbyCollectable)
                onInteractCollectable.Invoke();
            else if (nearbyPurchaseable)
                onInteractPurchaseable.Invoke();
        }
    }

    public UnityEvent onInteractDialogue;
    public UnityEvent onInteractCollectable;
    public UnityEvent onInteractPurchaseable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dialogue"))
            nearbyDialogue = true;
        else if (collision.gameObject.CompareTag("Collectable"))
            nearbyCollectable = true;
        else if (collision.gameObject.CompareTag("Purchaseable"))
            nearbyPurchaseable = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dialogue"))
            nearbyDialogue = false;
        else if (collision.gameObject.CompareTag("Collectable"))
            nearbyCollectable = false;
        else if (collision.gameObject.CompareTag("Purchaseable"))
            nearbyPurchaseable = false;
    }

    //Drawing the Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
