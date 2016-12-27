using UnityEngine;

public class EraserController : MonoBehaviour
{
    public Transform topStart, topEnd;
    public AudioSource stompSource;
    private bool isLeft, isHit;
    public GameObject heart;
    private int time;

    void Start()
    {
        isLeft = true;
        isHit = false;
        time = 0;
    }

    void Update()
    {
        if (isLeft)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
            if (transform.position.x < 23)
                isLeft = !isLeft;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
            if (transform.position.x > 29)
                isLeft = !isLeft;
        }

        if (isStomped())
        {
            PlayerPrefs.SetInt("newScore", PlayerPrefs.GetInt("newScore") + 1);
            stompSource.GetComponent<AudioSource>().Play();
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<BoxCollider2D>());
            GetComponent<Animator>().SetInteger("State", 1);
            Vector3 active = new Vector3(0, 5f, 0);
            active = transform.position + active;
            heart.transform.position = active;
            isHit = true;
        }

        if (isHit)
        {
            time++;
            if (time == 50)
                Destroy(gameObject);
        }
    }

    bool isStomped()
    {
        bool isHit = false;
        Debug.DrawLine(topStart.position, topEnd.position, Color.green);

        if (Physics2D.Linecast(topStart.position, topEnd.position, 1 << LayerMask.NameToLayer("Player")))
            isHit = true;

        return isHit;
    }
}