using UnityEngine;

public class BreakableCeiling : MonoBehaviour
{
    [SerializeField] private float jumpThresholdSpeed = 5f; // Minimum speed to break the ceiling
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.relativeVelocity.y > jumpThresholdSpeed)
            {
                // Allow passing through ceiling
                animator.SetTrigger("explode");
            }
        }
    }

    public void DestroyCeiling()
    {
        Destroy(gameObject);
    }
}
