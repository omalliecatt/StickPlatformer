using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public GameObject blade1, blade2;
    private bool isUp;

    void Start()
    {
        isUp = false;
    }

    void Update()
    {
        blade1.transform.Rotate(Vector3.forward * -15);
        blade2.transform.Rotate(Vector3.forward * -15);

        if(isUp)
        {
            blade2.transform.Translate(0, 3 * Time.deltaTime, 0, 0);

            if (blade2.transform.position.y > 5)
                isUp = !isUp;
        }
        else
        {
            blade2.transform.Translate(0, -3 * Time.deltaTime, 0, 0);

            if (blade2.transform.position.y < -2)
                isUp = !isUp;
        }
    }
}
