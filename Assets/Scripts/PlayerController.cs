using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Components
    // Rigidbody
    Rigidbody rb;

    // Player data
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
    }

    // Called once per frame
    void Update()
    {
        // Update current time
        currentTime -= Time.deltaTime;
        System.TimeSpan time = System.TimeSpan.FromSeconds(currentTime);
        textCurrentTime.text = time.ToString("m':'ss");
    }

    // Called every fixed frame-rate frame
    void FixedUpdate()
    {
        // Update position
        Vector3 move = new Vector3(0.0f, 0.0f, 0.0f);
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            move.z = 1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            move.z = -1.0f;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            move.x = -1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            move.x = 1.0f;
        }
        rb.MovePosition(transform.position + move);
    }

    // Called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
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
            }
        }
        else if (other.gameObject.CompareTag("Car"))
        {
            // Hit by car
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
            transform.position = Spawn.transform.position;
        }
    }
}
