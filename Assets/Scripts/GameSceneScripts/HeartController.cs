using UnityEngine;

public class HeartController : MonoBehaviour
{
    public AudioSource coinSource;
    public Transform vertStart, vertEnd, horiStart, horiEnd;
    private bool collect;
    private int time;

    void Start()
    {
        collect = false;
        time = 0;
    }

    void Update()
    {
        if (isCollect())
        {
            PlayerPrefs.SetInt("lives", PlayerPrefs.GetInt("lives") + 1);
            coinSource.GetComponent<AudioSource>().Play();
            transform.position = new Vector3(0, 40, 0);
            collect = true;
        }

        if (collect)
        {
            time++;
            if (time == 50)
                Destroy(gameObject);
        }
    }


    bool isCollect()
    {
        bool isCollect = false;

        Debug.DrawLine(horiStart.position, horiEnd.position, Color.green);
        Debug.DrawLine(vertStart.position, vertEnd.position, Color.green);

        if (Physics2D.Linecast(horiStart.position, horiEnd.position, 1 << LayerMask.NameToLayer("Player")))
            isCollect = true;
        else if (Physics2D.Linecast(vertStart.position, vertEnd.position, 1 << LayerMask.NameToLayer("Player")))
            isCollect = true;

        return isCollect;
    }
}
