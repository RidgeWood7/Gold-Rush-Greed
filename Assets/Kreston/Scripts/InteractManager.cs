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
    public UnityEvent collectIngot;
    public UnityEvent collectCoal;
    public UnityEvent onInteractStore;
    public UnityEvent onInteractLandStore;
    public UnityEvent collectWheel;
    public UnityEvent collectFeed;

    [SerializeField] private float _interactRange;
    private CircleCollider2D _interactCollider;
    private bool _nearbyDialogue;
    private bool _nearbyCollectableDust;
    private bool _nearbyCollectableIngot;
    private bool _nearbyCollectableCoal;
    private bool _nearbyCollectableWheel;
    private bool _nearbyCollectableFeed;
    private bool _nearbyStore;
    private bool _nearbyLandStore;

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
            else if (_nearbyCollectableCoal)
                collectCoal.Invoke();
            else if (_nearbyCollectableWheel)
                collectWheel.Invoke();
            else if (_nearbyCollectableFeed)
                collectFeed.Invoke();
            else if (_nearbyStore)
                onInteractStore.Invoke();
            else if (_nearbyLandStore)
                onInteractLandStore.Invoke();
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
        else if (collision.gameObject.CompareTag("CollectableCoal"))
            _nearbyCollectableCoal = true;
        else if (collision.gameObject.CompareTag("CollectableWheel"))
            _nearbyCollectableWheel = true;
        else if (collision.gameObject.CompareTag("CollectableFeed"))
            _nearbyCollectableFeed = true;
        else if (collision.gameObject.CompareTag("Store"))
            _nearbyStore = true;
        else if (collision.gameObject.CompareTag("LandStore"))
            _nearbyLandStore = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _nearbyDialogue = false;
        _nearbyCollectableDust = false;
        _nearbyCollectableIngot = false;
        _nearbyCollectableCoal = false;
        _nearbyCollectableWheel = false;
        _nearbyCollectableFeed = false;
        _nearbyStore = false;
        _nearbyLandStore = false;
    }

    //Drawing the Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _interactRange);
    }
}
