using UnityEngine;
using System.Collections;

public class OnCatch : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] bool stand;

    [SerializeField] private GameObject winHeart = null;
    [SerializeField] private bool shouldSpawn = false;
    [SerializeField] private bool isSpawned = false;

    [SerializeField] private AudioClip winSound = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        RollControl();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Candy")
        {
            Destroy(collision.gameObject);
            animator.SetBool("isCandy", true);
        }
    }

    private void RollControl()
    {
        if (rb.velocity.sqrMagnitude > 1
            || (animator.GetBool("isCandy") == true && animator.GetBool("isRolling") == false))
        {
            animator.SetBool("isRolling", true);
        }
        else if (rb.velocity.sqrMagnitude <= 1 && rb.velocity.sqrMagnitude > Mathf.Epsilon
            && animator.GetBool("isRolling") == true)
        {
            rb.drag = 1f;
        }
        else if (rb.velocity.sqrMagnitude <= Mathf.Epsilon
            && animator.GetBool("isRolling") == true)
        {
            rb.drag = 0.3f; //drag value after stand up
            animator.SetBool("isRolling", false);
            animator.SetBool("isCandy", false);
        }

        if (stand == true)
        {
            rb.rotation = 0;
            if(shouldSpawn == true && isSpawned == false)
            {
                Instantiate(winHeart, transform.position + Vector3.up * 1, Quaternion.identity);
                StartCoroutine(OnWin());
                isSpawned = true;
            }
            stand = false;
        }
    }

    private IEnumerator OnWin()
    {
        GetComponent<AudioSource>().clip = winSound;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2);
        GameManager.instance.NextLevel();
    }
}
