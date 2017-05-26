using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    
    public float restartDelay = 5f;         // Time to wait before restarting the level


    Animator anim;                          // Reference to the animator component.
    float restartTimer;                     // Timer to count up to restarting the level

    void Start()
    {
        
    }

    void Awake()
    {
        // Set up the reference.
        anim = GetComponent<Animator>();
        
    }


    void Update()
    {
        // If the player has run out of health...
        if (GameObject.FindGameObjectsWithTag("Minion").Length == 0)
        {
            // ... tell the animator the game is over.
            anim.SetTrigger("Game Over: You saved all the kittens!");

            // .. increment a timer to count up to restarting.
            restartTimer += Time.deltaTime;

            // .. if it reaches the restart delay...
            if (restartTimer >= restartDelay)
            {
                // .. then reload the currently loaded level.
                SceneManager.LoadScene("menu");
            }
        }
    }
}