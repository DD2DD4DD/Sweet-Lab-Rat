using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour
{
    private bool isCounting;
    private Rigidbody2D rb;

    [SerializeField] private GameObject explosion = null;
    [SerializeField] private GameObject indicator = null;
    [SerializeField] private float pushForce = 7;
    [SerializeField] private float effectTime = 0.1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.name == "Candy"
            || collision.gameObject.name == "Mouse"
            || collision.gameObject.tag == "Moving Wall"
            || collision.gameObject.tag == "Explosive"))
        {
            if(collision.gameObject.tag == "Moving Wall")
            {
                Vector2 comPt = Vector2.zero;
                foreach(ContactPoint2D conPt in collision.contacts)
                {
                    comPt += conPt.point;
                }
                comPt /= collision.contactCount;
                rb.velocity = ((Vector2)transform.position - comPt) * pushForce;
            }

            GetComponent<Animator>().SetBool("startCount", true);
            if (isCounting == false)
            {
                indicator.SetActive(true);
                StartCoroutine(CountToExplode());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PointEffector2D>())
        {
            GetComponent<Animator>().SetBool("startCount", true);
            if (isCounting == false)
            {
                indicator.SetActive(true);
                StartCoroutine(CountToExplode());
            }
        }
    }

    private IEnumerator CountToExplode()
    {
        isCounting = true;
        yield return new WaitForSeconds(3);
        Explode();
    }

    private void Explode()
    {
        EZCameraShake.CameraShaker.Instance.ShakeOnce(4f, 6f, 0f, 1f);

        GameObject instance = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(instance, 1);
        Destroy(instance.GetComponent<PointEffector2D>(), effectTime);
        Destroy(gameObject);
        foreach (Collider2D obj in Physics2D.OverlapCircleAll(transform.position, 1.4f))
        {
            if (obj.name == "Candy" || obj.name == "Mouse")
            {
                Destroy(obj.gameObject);
                if (GameObject.Find("Game Manager"))
                {
                    StartCoroutine(GameObject.Find("Game Manager").GetComponent<GameManager>().OnLose());
                }
            }
            else if(obj.tag == "Explosive")
            {
                obj.GetComponent<ExplosionController>().Explode();
            }
        }
    }
}
