using UnityEngine;

public class CamControl : MonoBehaviour
{
    private Camera cam;
    private int curCam = 1;
    [SerializeField] private int camMax = 0;

    [SerializeField] private Vector3 camPos1 = default;
    [SerializeField] private float camSize1 = 0;
    [SerializeField] private Vector3 camPos2 = default;
    [SerializeField] private float camSize2 = 0;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            if(curCam < camMax)
            {
                curCam += 1;
            }
        }
        else if(Input.GetKeyDown(KeyCode.Z))
        {
            if (curCam > 1)
            {
                curCam -= 1;
            }
        }

        switch (curCam)
        {
            case 1:
                transform.position = camPos1;
                cam.orthographicSize = camSize1;
                break;
            case 2:
                transform.position = camPos2;
                cam.orthographicSize = camSize2;
                break;
        }
    }
}
