using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

[ExecuteAlways]
public class InteractManager : MonoBehaviour
{
    [SerializeField] public TMPro.TMP_Text pressE;

    //All Unity Events
    public UnityEvent onInteractDialogueLA;
    public UnityEvent onInteractDialogueMO;
    public UnityEvent collectDust;
    public UnityEvent collectIngot;
    public UnityEvent collectCoal;
    public UnityEvent onInteractStore;
    public UnityEvent onInteractLandStore;
    public UnityEvent onInteractTransport;
    public UnityEvent collectWheel1;
    public UnityEvent collectWheel2;
    public UnityEvent collectWheel3;
    public UnityEvent collectWheel4;
    public UnityEvent collectFeed;
    public UnityEvent horse;

    [SerializeField] private float _interactRange;
    private CircleCollider2D _interactCollider;
    private bool _nearbyDialogueLA;
    private bool _nearbyDialogueMO;
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
    private bool _nearbyTransport;
    private bool _nearbyHorse;

    private void Awake() =>_interactCollider = GetComponent<CircleCollider2D>();
    private void Reset() => _interactCollider = GetComponent<CircleCollider2D>();

    private void Update()
    {
        _interactCollider.radius = _interactRange;

        if (_nearbyDialogueLA || _nearbyDialogueMO || _nearbyCollectableDust || _nearbyCollectableIngot || _nearbyCollectableCoal || _nearbyCollectableWheel1 || _nearbyCollectableWheel2 || _nearbyCollectableWheel3 || _nearbyCollectableWheel4 || _nearbyCollectableFeed || _nearbyStore || _nearbyLandStore || _nearbyHorse || _nearbyTransport)
        {
            pressE.enabled = true;
        }
        else
        {
            pressE.enabled = false;
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (_nearbyCollectableDust)
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
            else if (_nearbyTransport)
                onInteractTransport.Invoke();

            //ADD TO THE IF ABOVE IF YOU ADD MORE INTERACTIONS!!!!!!!!!!!!!!!!!!!!
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_nearbyDialogueLA)
                    onInteractDialogueLA.Invoke();
                else if (_nearbyDialogueMO)
                    onInteractDialogueMO.Invoke();
                else if (_nearbyHorse)
                    horse.Invoke();
            }

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DialogueLA"))
            _nearbyDialogueLA = true;
        else if (collision.gameObject.CompareTag("DialogueMO"))
            _nearbyDialogueMO = true;
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
        else if (collision.gameObject.CompareTag("Horse"))
            _nearbyHorse = true;
        else if (collision.gameObject.CompareTag("Transport"))
            _nearbyTransport = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _nearbyDialogueLA = false;
        _nearbyDialogueMO = false;
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
        _nearbyTransport = false;
        _nearbyHorse = false;
    }

    //Drawing the Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _interactRange);
    }
}
