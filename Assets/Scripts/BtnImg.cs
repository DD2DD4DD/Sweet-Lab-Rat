using UnityEngine;
using UnityEngine.UI;

public class BtnImg : MonoBehaviour
{
    [SerializeField] private Sprite icon1 = null;
    [SerializeField] private Sprite icon2 = null;

    private bool isChange = false;

    private void Start()
    {
        switch(gameObject.name)
        {
            case "Music Btn":
                if(GameManager.instance.playMusic == false)
                {
                    ChangeIcon();
                }
                break;

            case "SFX Btn":
                if(GameManager.instance.playSfx == false)
                {
                    ChangeIcon();
                }
                break;
        }
    }

    public void ChangeIcon()
    {
        isChange = !isChange;

        if(isChange == false)
        {
            GetComponent<Image>().sprite = icon1;

            switch(gameObject.name)
            {
                case "Music Btn":
                    GameManager.instance.playMusic = true;
                    GameObject.Find("Music").GetComponent<AudioSource>().mute = false;
                    break;

                case "SFX Btn":
                    GameManager.instance.playSfx = true;
                    foreach(AudioSource source in FindObjectsOfType<AudioSource>())
                    {
                        if(source.name != "Music")
                        {
                            source.mute = false;
                        }
                    }
                    break;
            }
        }
        else if(isChange == true)
        {
            GetComponent<Image>().sprite = icon2;

            switch (gameObject.name)
            {
                case "Music Btn":
                    GameManager.instance.playMusic = false;
                    GameObject.Find("Music").GetComponent<AudioSource>().mute = true;
                    break;

                case "SFX Btn":
                    GameManager.instance.playSfx = false;
                    foreach (AudioSource source in FindObjectsOfType<AudioSource>())
                    {
                        if (source.name != "Music")
                        {
                            source.mute = true;
                        }
                    }
                    break;
            }
        }
    }
}
