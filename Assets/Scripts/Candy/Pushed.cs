using UnityEngine;

public class Pushed : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float pushForce = 7;

    private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();

        if(collision.gameObject.tag == "Moving Wall")
        {
            rb.velocity = ((Vector2)transform.position - collision.GetContact(0).point) * pushForce;
        }
    }
}
