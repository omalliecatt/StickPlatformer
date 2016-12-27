using UnityEngine;

public class Obstacle2Controller : MonoBehaviour
{
    public GameObject moveCrayon1, blade1, blade2, blade3, blade4, blade5;
    private bool isRight, isUp;

    void Start()
    {
        isRight = false;
        isUp = true;
    }

    void Update()
    {
        blade1.transform.Rotate(Vector3.forward * -15);
        blade2.transform.Rotate(Vector3.forward * -15);
        blade3.transform.Rotate(Vector3.forward * -15);
        blade4.transform.Rotate(Vector3.forward * -15);
        blade5.transform.Rotate(Vector3.forward * -15);

        if (isRight)
        {
            moveCrayon1.transform.Translate(3 * Time.deltaTime, 0, 0, 0);

            if (moveCrayon1.transform.position.x > -10)
                isRight = !isRight;
        }
        else
        {
            moveCrayon1.transform.Translate(-3 * Time.deltaTime, 0, 0, 0);

            if (moveCrayon1.transform.position.x < -23)
                isRight = !isRight;
        }

        if (isUp)
        {
            blade4.transform.Translate(0, 3 * Time.deltaTime, 0, 0);
            blade5.transform.Translate(0, 3 * Time.deltaTime, 0, 0);

            if (blade4.transform.position.y > 2)
                isUp = !isUp;
        }
        else
        {
            blade4.transform.Translate(0, -3 * Time.deltaTime, 0, 0);
            blade5.transform.Translate(0, -3 * Time.deltaTime, 0, 0);

            if (blade4.transform.position.y < -5)
                isUp = !isUp;
        }
    }
}