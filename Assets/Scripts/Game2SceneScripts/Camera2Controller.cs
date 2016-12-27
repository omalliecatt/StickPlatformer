using UnityEngine;

public class Camera2Controller : MonoBehaviour
{
    public GameObject player;
    public GameObject backLayer;
    private Vector3 cameraPos;
    private Vector3 offset;


    void Start()
    {
        offset = transform.position;
        cameraPos = player.transform.position;
        cameraPos.y = 0;
        transform.position = cameraPos;
    }

    void LateUpdate()
    {
        cameraPos = player.transform.position + offset;
        cameraPos.y = 0;

        if (cameraPos.x >= -28.62282 && cameraPos.x <= 28.89358)
        {
            transform.position = cameraPos;
            backLayer.transform.position = new Vector3(cameraPos.x / 2.5f, 1.12f, 0);
        }
    }
}