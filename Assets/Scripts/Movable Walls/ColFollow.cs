using UnityEngine;
using EZCameraShake;

public class ColFollow : MonoBehaviour
{
    [Tooltip("0 = right, 1 = up, 2 = left, 3 = down")]
    [SerializeField] private int dir = 0;

    [SerializeField] private float startWidth = 3.5f;
    [SerializeField] private float endWidth = 7f;
    [SerializeField] private float colOffset = 2.6f;
    [SerializeField] private float blurPosX = 1.9f;

    [Range(0, 1)]
    [SerializeField] private float lerpValue = 0f;
    [SerializeField] private bool isAnim = false;

    private SpriteRenderer sprRen;
    private Collider2D col;
    private Animator animator;
    private Transform blur;

    private AudioSource source;
    [SerializeField] private AudioClip push = null;
    [SerializeField] private AudioClip ready = null;
    private string tempTag;
    private bool tempAnim;

    private void Start()
    {
        sprRen = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        blur = transform.GetChild(0);
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        switch (dir)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    animator.SetTrigger("isCalled");
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    animator.SetTrigger("isCalled");
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    animator.SetTrigger("isCalled");
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    animator.SetTrigger("isCalled");
                }
                break;
        }

        if(lerpValue > 0 && lerpValue < 1 && animator.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
        {
            gameObject.tag = "Moving Wall";
            tempTag = gameObject.tag;
        }
        else
        {
            if(tempTag == "Moving Wall")
            {
                source.clip = push;
                source.Play();

                CameraShaker.Instance.ShakeOnce(2f, 5f, 0f, 0.2f);
            }
            gameObject.tag = "Untagged";
            tempTag = gameObject.tag;
        }

        if(isAnim)
        {
            animator.ResetTrigger("isCalled");
            Vector2 tempSize = sprRen.size;
            tempSize.x = Mathf.Lerp(startWidth, endWidth, lerpValue);
            sprRen.size = tempSize;

            Vector2 tempOff = col.offset;
            tempOff.x = sprRen.size.x - startWidth + colOffset;
            col.offset = tempOff;

            Vector2 blurPos = blur.localPosition;
            blurPos.x = sprRen.size.x - startWidth + blurPosX;
            blur.localPosition = blurPos;

            tempAnim = true;
        }
        else if(!isAnim)
        {
            if (tempAnim == true && lerpValue <= 0f)
            {
                source.clip = ready;
                source.Play();
                tempAnim = false;
            }
        }
    }
}
