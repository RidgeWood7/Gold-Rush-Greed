using UnityEngine;
using UnityEngine.Events;

[ExecuteAlways]
public class InteractManager : MonoBehaviour
{
    [SerializeField] private float interactRange = 1.3f;
    private CircleCollider2D interactCollider;
    
    private void Awake() =>interactCollider = GetComponent<CircleCollider2D>();
    private void Reset() => interactCollider = GetComponent<CircleCollider2D>();
    private void Update() => interactCollider.radius = interactRange;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dialogue"))
        {
            Debug.Log("Dialogue");
        }
        else if(collision.gameObject.CompareTag("Collectable"))
        {
            Debug.Log("Collectable");
        }
        else if (collision.gameObject.CompareTag("Purchaseable"))
        {
            Debug.Log("Purchaseable");
        }
    }

    //Drawing the Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
