using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioSource deathSource;
    public AudioSource jumpSource;
    public AudioSource landSource;
    public AudioSource runSource;
    public GameObject enterSign;
    public Text ScoreText;
    public Text LivesText;
    public Text TopText;

    public Transform bottomStart, bottomEnd, topStart, topEnd, leftStart, leftEnd, rightStart, rightEnd, horiStart, horiEnd;

    private SpriteRenderer spri;
    private Animator anim;
    private int animState;
    private int time;
    private int jump;
    private float maxSpeed = 10f;

    void Start()
    {
        jump = 0;
        time = 0;
        anim = GetComponent<Animator>();
        spri = GetComponent<SpriteRenderer>();
        PlayerPrefs.SetInt("newScore", 0);
        PlayerPrefs.SetInt("lives", 3);
        ScoreText.text = "Score: " + PlayerPrefs.GetInt("newScore").ToString();
        ScoreText.text = "Lives: " + PlayerPrefs.GetInt("lives").ToString();
    }

    void Update()
    {
        movementController();
        animate();

        ScoreText.text = "Score: " + PlayerPrefs.GetInt("newScore").ToString();
        LivesText.text = "Lives: " + PlayerPrefs.GetInt("lives").ToString();
    }

    void movementController()
    {
        float move = GetComponent<Rigidbody2D>().velocity.x;
        float move2 = GetComponent<Rigidbody2D>().velocity.y;

        if (Input.GetKeyDown(KeyCode.UpArrow) && move2 == 0 && animState != 3)
        {
            jumpSource.GetComponent<AudioSource>().Play();
            jump = 1;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && animState != 3)
        {
            if (move < maxSpeed)
                move += 1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && animState != 3)
        {
            if (move > (-maxSpeed))
                move -= 1f;
        }
        else
        {
            if (move > 0)
                move -= 1f;
            else if(move < 0)
                move += 1f;
        }

        if (Input.GetKeyDown(KeyCode.E) && atDoor())
        {
            SceneManager.LoadScene("Game2Scene");
        }

        if (jump == 1 && move2 < 5)
            move2 += 1f;
        else if (jump == 1 || move2 < 0)
            jump = 2;
        else if(move2 == 0)
            jump = 0;
        
        GetComponent<Rigidbody2D>().velocity = new Vector2(move, move2);
    }

    void animate()
    {
        int state = animState;
        float move = GetComponent<Rigidbody2D>().velocity.x;
        float move2 = GetComponent<Rigidbody2D>().velocity.y;

        if (transform.position.y < -3 || isSawed() || isHit() || state == 3)
            state = 3;
        else if (!isGrounded())
            state = 2;
        else
            state = 0;

        if (state != 3)
        {
            if (state == 0)
            {
                if (move > 1)
                    state = 1;
                else if (move < -1)
                    state = -1;
            }
            else
            {
                if (move > 1)
                    state *= 1;
                else if (move < -1)
                    state *= -1;
            }

            if (atDoor())
            {
                Vector3 active = new Vector3(26f, -1f, 0);
                enterSign.transform.position = active;
            }
            else
            {
                Vector3 active = new Vector3(23f, 40f, 0);
                enterSign.transform.position = active;
            }

            if (isGrounded() && animState == 2)
                landSource.GetComponent<AudioSource>().Play();

            changeAnim(Mathf.Abs(state));
            if (state > 0)
                spri.flipX = false;
            else if (state < 0)
                spri.flipX = true;
        }
        else
        {
            if (time == 0)
                deathSource.GetComponent<AudioSource>().Play();
            if (PlayerPrefs.GetInt("lives") > 0)
                TopText.text = "You died";
            else if (PlayerPrefs.GetInt("lives") == 0)
                TopText.text = "Game Over";
            changeAnim(3);
            time++;

            if (time > 100 && PlayerPrefs.GetInt("lives") > 0)
            {
                transform.position = new Vector3(-28f, 0f, 0);
                TopText.text = "";
                changeAnim(0);
                PlayerPrefs.SetInt("lives", PlayerPrefs.GetInt("lives") - 1);
                time = 0;
            }
            else if (time > 100)
            {
                PlayerPrefs.DeleteKey("newScore");
                SceneManager.LoadScene("ScoreScene");
            }
        }
    }

    bool isGrounded()
    {
        return Physics2D.Linecast(bottomStart.position, bottomEnd.position);
    }

    bool isSawed()
    {
        bool isHit = false;
        
        if (Physics2D.Linecast(leftStart.position, leftEnd.position, 1 << LayerMask.NameToLayer("Eraser")))
            isHit = true;
        else if (Physics2D.Linecast(rightStart.position, rightEnd.position, 1 << LayerMask.NameToLayer("Eraser")))
            isHit = true;

        return isHit;
    }

    bool isHit()
    {
        bool isHit = false;
        Debug.DrawLine(bottomStart.position, bottomEnd.position, Color.green);
        Debug.DrawLine(rightStart.position, rightEnd.position, Color.green);
        Debug.DrawLine(leftStart.position, leftEnd.position, Color.green);
        Debug.DrawLine(horiStart.position, horiEnd.position, Color.green);
        Debug.DrawLine(topStart.position, topEnd.position, Color.green);

        if (Physics2D.Linecast(horiStart.position, horiEnd.position, 1 << LayerMask.NameToLayer("Blade")))
            isHit = true;
        else if (Physics2D.Linecast(topStart.position, topEnd.position, 1 << LayerMask.NameToLayer("Blade")))
            isHit = true;
        else if (Physics2D.Linecast(leftStart.position, leftEnd.position, 1 << LayerMask.NameToLayer("Blade")))
            isHit = true;
        else if (Physics2D.Linecast(rightStart.position, rightEnd.position, 1 << LayerMask.NameToLayer("Blade")))
            isHit = true;
        else if (Physics2D.Linecast(bottomStart.position, bottomEnd.position, 1 << LayerMask.NameToLayer("Blade")))
            isHit = true;

        return isHit;
    }

    bool atCoin()
    {
        bool isHit = false;

        if (Physics2D.Linecast(horiStart.position, horiEnd.position, 1 << LayerMask.NameToLayer("Coin")))
            isHit = true;
        else if (Physics2D.Linecast(topStart.position, topEnd.position, 1 << LayerMask.NameToLayer("Coin")))
            isHit = true;
        else if (Physics2D.Linecast(leftStart.position, leftEnd.position, 1 << LayerMask.NameToLayer("Coin")))
            isHit = true;
        else if (Physics2D.Linecast(rightStart.position, rightEnd.position, 1 << LayerMask.NameToLayer("Coin")))
            isHit = true;
        else if (Physics2D.Linecast(bottomStart.position, bottomEnd.position, 1 << LayerMask.NameToLayer("Coin")))
            isHit = true;

        return isHit;
    }

    bool atDoor()
    {
        bool isHit = false;

        if (Physics2D.Linecast(horiStart.position, horiEnd.position, 1 << LayerMask.NameToLayer("Door")))
            isHit = true;
        else if (Physics2D.Linecast(topStart.position, topEnd.position, 1 << LayerMask.NameToLayer("Door")))
            isHit = true;
        else if (Physics2D.Linecast(leftStart.position, leftEnd.position, 1 << LayerMask.NameToLayer("Door")))
            isHit = true;
        else if (Physics2D.Linecast(rightStart.position, rightEnd.position, 1 << LayerMask.NameToLayer("Door")))
            isHit = true;
        else if (Physics2D.Linecast(bottomStart.position, bottomEnd.position, 1 << LayerMask.NameToLayer("Door")))
            isHit = true;

        return isHit;
    }

    void changeAnim(int state)
    {
        if (state != animState && state == 1)
            runSource.GetComponent<AudioSource>().Play();
        else if (state != animState)
            runSource.GetComponent<AudioSource>().Stop();

        animState = state;
        anim.SetInteger("State", state);
    }
}