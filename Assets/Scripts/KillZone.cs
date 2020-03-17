using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Candy" || collision.name == "Mouse")
        {
            Destroy(collision.gameObject);
            GameObject.Find("Game Manager").GetComponent<GameManager>().OnLose();
        }
    }
}
