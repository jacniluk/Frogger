using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Components
    // Rigidbody
    Rigidbody rb;

    // Max num of lives of player
    const int maxLives = 3;
    // Current num of lives of player
    int lives;
    // Num of points which player obtained on current level
    int score;
    // Max num of bases which player must achieve
    const int maxBases = 3;
    // Current num of bases which player achieved
    int bases;
    // Movement step
    Vector3 move;
    // Is player swimming
    public bool isSwimming;

    // Max time of Level 1
    const float maxTimeLevel1 = 45.0f;
    // Current time
    float currentTime;

    // References
    // Player spawn
    public GameObject Spawn;
    // HUD lives
    public Text textLives;
    // HUD current time
    public Text textCurrentTime;
    // HUD score
    public Text textScore;
    // Message for level completing or player dead
    public Text textMessage;

    // Called before the first frame update
    void Start()
    {
        // Initialization of data
        rb = GetComponent<Rigidbody>();
        lives = maxLives;
        score = 0;
        bases = 0;
        currentTime = maxTimeLevel1;
        textLives.text = "Lives: " + lives.ToString();
        textScore.text = "Score: 0";
        textMessage.enabled = false;

        // Respawn player
        Respawn();
    }

    // Called once per frame
    void Update()
    {
        // Update current time
        currentTime -= Time.deltaTime;
        System.TimeSpan time = System.TimeSpan.FromSeconds(currentTime);
        textCurrentTime.text = time.ToString("m':'ss");

        // Update position
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            move.z = 1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            move.z = -1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            move.x = -1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            move.x = 1.0f;
        }
        if (isSwimming && move != Vector3.zero)
        {
            isSwimming = false;
        }
    }

    // Called every fixed frame-rate frame
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + move);
        move = Vector3.zero;
    }

    // Called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target Base"))
        {
            // Achieve the base
            bases++;
            score += System.Convert.ToInt32(currentTime);
            textScore.text = "Score: " + score.ToString();
            if (bases < maxBases)
            {
                transform.position = Spawn.transform.position;
            }
            else
            {
                // 2do change level
                textMessage.text = "Level Complete!";
                textMessage.color = new Color(22.0f / 255.0f, 124.0f / 255.0f, 241.0f / 255.0f);
                textMessage.enabled = true;
                Invoke("DisableTextMessage", 2.0f);
            }
        }
        else if (other.gameObject.CompareTag("Car"))
        {
            // Hit by car
            Dead();
        }
    }

    // Respawn player and reset data
    void Respawn()
    {
        transform.rotation = Spawn.transform.rotation;
        transform.position = Spawn.transform.position;
        move = Vector3.zero;
        isSwimming = false;
    }

    // Disable message after some time
    void DisableTextMessage()
    {
        textMessage.enabled = false;
    }

    // Player is dead
    public void Dead()
    {
        lives--;
        if (lives > 0)
        {
            textMessage.text = "You are dead!";
        }
        else
        {
            textMessage.text = "Game Over!";
        }
        textLives.text = "Lives: " + lives.ToString();
        textMessage.color = new Color(207.0f / 255.0f, 17.0f / 255.0f, 17.0f / 255.0f);
        textMessage.enabled = true;
        Invoke("DisableTextMessage", 2.0f);

        // Respawn player
        Respawn();
    }
}
