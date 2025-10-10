using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

[ExecuteAlways]
public class InteractManager : MonoBehaviour
{
    //All Unity Events
    public UnityEvent onInteractDialogue;
    public UnityEvent collectDust;
    public UnityEvent collectIngot; //There is no drilled collection because it is an automatic collection
    public UnityEvent onInteractPurchaseable;

    [SerializeField] private float _interactRange;
    private CircleCollider2D _interactCollider;
    private bool _nearbyDialogue;
    private bool _nearbyCollectableDust;
    private bool _nearbyCollectableIngot;
    private bool _nearbyPurchaseable;

    private void Awake() =>_interactCollider = GetComponent<CircleCollider2D>();
    private void Reset() => _interactCollider = GetComponent<CircleCollider2D>();

    private void Update()
    {
        _interactCollider.radius = _interactRange;

        if (Input.GetKey(KeyCode.E))
        {
            if (_nearbyDialogue)
                onInteractDialogue.Invoke();
            else if (_nearbyCollectableDust)
                collectDust.Invoke();
            else if (_nearbyCollectableIngot)
                collectIngot.Invoke();
            else if (_nearbyPurchaseable)
                onInteractPurchaseable.Invoke();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dialogue"))
            _nearbyDialogue = true;
        else if (collision.gameObject.CompareTag("CollectableDust"))
            _nearbyCollectableDust = true;
        else if (collision.gameObject.CompareTag("CollectableIngot"))
            _nearbyCollectableIngot = true;
        else if (collision.gameObject.CompareTag("Purchaseable"))
            _nearbyPurchaseable = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
            _nearbyDialogue = false;
            _nearbyCollectableDust = false;
            _nearbyCollectableIngot = false;
            _nearbyPurchaseable = false;
    }

    //Drawing the Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _interactRange);
    }
}
