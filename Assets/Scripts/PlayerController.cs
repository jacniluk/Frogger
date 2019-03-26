using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Controller for player, update properties, movement, time, HUD
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
    // Is base achieved
    bool isBaseAchieved;

    // Max time
    float maxTime;
    // Current time
    float currentTime;

    // References
    // Player spawn
    public GameObject Spawn;
    // HUD text level
    public Text textLevel;
    // HUD text bases
    public Text textBases;
    // HUD text current time
    public Text textCurrentTime;
    // HUD slider current time
    public Slider sliderCurrentTime;
    // HUD text lives
    public Text textLives;
    // HUD text score
    public Text textScore;
    // Message for level completing or player dead
    public Text textMessage;

    // Is player floating
    public bool isFloating;
    // Max left position of player
    public float maxLeftPosition = -8.9f;
    // Max right position of player
    public float maxRightPosition = 8.9f;
    // Max down position of player
    public float maxDownPosition = -5.9f;

    // Called before the first frame update
    void Start()
    {
        // Initialization of data
        rb = GetComponent<Rigidbody>();
        lives = maxLives;
        score = 0;
        bases = 0;
        textLevel.text = "Level " + SceneManager.GetActiveScene().buildIndex + "/" + (SceneManager.sceneCountInBuildSettings - 1);
        textBases.text = "Bases: 0/3";
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
        if (!isBaseAchieved)
        {
            currentTime -= Time.deltaTime;
        }
        System.TimeSpan time = System.TimeSpan.FromSeconds(currentTime);
        if (currentTime > 0.0f)
        {
            textCurrentTime.text = time.ToString("m':'ss");
            sliderCurrentTime.value = currentTime / maxTime;
        }
        if (currentTime <= 0.0f)
        {
            // Time is up
            TimeUp();
        }

        // Update position
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            move.z = 1.0f;
        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && !IsAtMapDownEdge())
        {
            move.z = -1.0f;
        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !IsAtMapLeftEdge())
        {
            move.x = -1.0f;
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !IsAtMapRightEdge())
        {
            move.x = 1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMainMenu();
        }
        if (isFloating && move != Vector3.zero)
        {
            isFloating = false;
        }
    }

    // Called every fixed frame-rate frame
    void FixedUpdate()
    {
        if (!isBaseAchieved)
        {
            rb.MovePosition(transform.position + move);
        }
        move = Vector3.zero;
    }

    // Called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target Base"))
        {
            // Achieve the base
            transform.position = other.gameObject.transform.position;
            bases++;
            score += System.Convert.ToInt32(currentTime);
            textBases.text = "Bases: " + bases.ToString() + "/3";
            textScore.text = "Score: " + score.ToString();
            if (bases < maxBases)
            {
                textMessage.text = "Base Achieved!";
                // Respawn player
                Invoke("Respawn", 2.0f);
            }
            else
            {
                textMessage.text = "Level Complete!\nYour Score: " + score.ToString();
                Invoke("LevelUp", 2.0f);
            }
            isBaseAchieved = true;
            textMessage.color = new Color(152.0f / 255.0f, 46.0f / 255.0f, 188.0f / 255.0f);
            textMessage.enabled = true;
            // Disable text message
            Invoke("DisableTextMessage", 2.0f);
        }
        else if (other.gameObject.CompareTag("Car"))
        {
            // Hit by car
            Dead("You got hit by a car!");
        }
    }

    // Respawn player and reset data
    void Respawn()
    {
        transform.rotation = Spawn.transform.rotation;
        transform.position = Spawn.transform.position;
        move = Vector3.zero;
        isBaseAchieved = false;
        isFloating = false;
        gameObject.SetActive(true);
    }

    // Go to MainMenu
    void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Cursor.visible = true;
    }

    // Disable message after some time
    void DisableTextMessage()
    {
        textMessage.enabled = false;
    }

    // Go to next level
    void LevelUp()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            GoToMainMenu();
        }
    }

    // Determine if player stands at the left edge of map
    bool IsAtMapLeftEdge()
    {
        return (transform.position.x < maxLeftPosition) ? true : false;
    }

    // Determine if player stands at the right edge of map
    bool IsAtMapRightEdge()
    {
        return (transform.position.x > maxRightPosition) ? true : false;
    }

    // Determine if player stands at the right edge of map
    bool IsAtMapDownEdge()
    {
        return (transform.position.z < maxDownPosition) ? true : false;
    }

    // Called when time is up
    void TimeUp()
    {
        gameObject.SetActive(false);
        textMessage.text = "Time is Up!\nGame Over!";
        textMessage.color = new Color(207.0f / 255.0f, 17.0f / 255.0f, 17.0f / 255.0f);
        textMessage.enabled = true;
        // Disable text message
        Invoke("DisableTextMessage", 2.0f);
        // Go to Main Menu
        Invoke("GoToMainMenu", 2.0f);
    }

    // Player is dead
    public void Dead(string message)
    {
        gameObject.SetActive(false);
        lives--;
        textLives.text = "Lives: " + lives.ToString();
        textMessage.text = message;
        if (lives == 0)
        {
            textMessage.text += "\nGame Over!";
        }
        textMessage.color = new Color(207.0f / 255.0f, 17.0f / 255.0f, 17.0f / 255.0f);
        textMessage.enabled = true;
        // Disable text message
        Invoke("DisableTextMessage", 2.0f);
        if (lives > 0)
        {
            // Respawn player
            Invoke("Respawn", 2.0f);
        }
        else
        {
            // Go to Main Menu
            Invoke("GoToMainMenu", 2.0f);
        }
    }

    // Move floating player
    public void MoveSwimming(Vector3 newPosition)
    {
        if (!IsAtMapLeftEdge() && !IsAtMapRightEdge())
        {
            transform.position = newPosition;
        }
        else
        {
            isFloating = false;
        }
    }

    // Set time on current level, called by Level Manager
    public void SetMaxTime(float _maxTime)
    {
        maxTime = _maxTime;
        currentTime = maxTime;
    }
}
