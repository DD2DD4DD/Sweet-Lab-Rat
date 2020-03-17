using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private GameObject losePanel = null;

    public bool playMusic = true;
    public bool playSfx = true;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        if (GameObject.Find("Music"))
        {
            DontDestroyOnLoad(GameObject.Find("Music"));
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

        if(Input.GetKeyDown(KeyCode.Period) || (SceneManager.GetActiveScene().buildIndex == 0 && Input.anyKey))
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator OnLose()
    {
        yield return new WaitForSeconds(2);
        losePanel.SetActive(true);
    }
}
