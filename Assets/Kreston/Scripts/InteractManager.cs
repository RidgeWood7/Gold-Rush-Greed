using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

[ExecuteAlways]
public class InteractManager : MonoBehaviour
{
    [SerializeField] public TMPro.TMP_Text pressE;

    //All Unity Events
    public UnityEvent onInteractDialogue;
    public UnityEvent collectDust;
    public UnityEvent collectIngot;
    public UnityEvent collectCoal;
    public UnityEvent onInteractStore;
    public UnityEvent onInteractLandStore;
    public UnityEvent collectWheel1;
    public UnityEvent collectWheel2;
    public UnityEvent collectWheel3;
    public UnityEvent collectWheel4;
    public UnityEvent collectFeed;

    [SerializeField] private float _interactRange;
    private CircleCollider2D _interactCollider;
    private bool _nearbyDialogue;
    private bool _nearbyCollectableDust;
    private bool _nearbyCollectableIngot;
    private bool _nearbyCollectableCoal;
    private bool _nearbyCollectableWheel1;
    private bool _nearbyCollectableWheel2;
    private bool _nearbyCollectableWheel3;
    private bool _nearbyCollectableWheel4;
    private bool _nearbyCollectableFeed;
    private bool _nearbyStore;
    private bool _nearbyLandStore;

    private void Awake() =>_interactCollider = GetComponent<CircleCollider2D>();
    private void Reset() => _interactCollider = GetComponent<CircleCollider2D>();

    private void Update()
    {
        _interactCollider.radius = _interactRange;

        if (_nearbyDialogue || _nearbyCollectableDust || _nearbyCollectableIngot || _nearbyCollectableCoal || _nearbyCollectableWheel1 || _nearbyCollectableWheel2 || _nearbyCollectableWheel3 || _nearbyCollectableWheel4 || _nearbyCollectableFeed || _nearbyStore || _nearbyLandStore)
        {
            pressE.enabled = true;
        }
        else
        {
            pressE.enabled = false;
        }

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
            else if (_nearbyCollectableWheel1)
                collectWheel1.Invoke();
            else if (_nearbyCollectableWheel2)
                collectWheel2.Invoke();
            else if (_nearbyCollectableWheel3)
                collectWheel3.Invoke();
            else if (_nearbyCollectableWheel4)
                collectWheel4.Invoke();
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
        else if (collision.gameObject.CompareTag("CollectableWheel1"))
            _nearbyCollectableWheel1 = true;
        else if (collision.gameObject.CompareTag("CollectableWheel2"))
            _nearbyCollectableWheel2 = true;
        else if (collision.gameObject.CompareTag("CollectableWheel3"))
            _nearbyCollectableWheel3 = true;
        else if (collision.gameObject.CompareTag("CollectableWheel4"))
            _nearbyCollectableWheel4 = true;
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
        _nearbyCollectableWheel1 = false;
        _nearbyCollectableWheel2 = false;
        _nearbyCollectableWheel3 = false;
        _nearbyCollectableWheel4 = false;
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
