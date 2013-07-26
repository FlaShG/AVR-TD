using UnityEngine;
using System.Collections;

public class BaseHealth : MonoBehaviour
{
    private static BaseHealth instance;
    [SerializeField]
    private int lives = 20;

    /// <summary>
    /// Instantiate the BaseHealth.
    /// </summary>
	void Awake()
    {
        instance = this;
	}
    
    /// <summary>
    /// Apply damage to the base.
    /// </summary>
    /// <param name="lives">Amount of lives the base should loose</param>
    public static void ApplyDamage(int lives)
    {
        instance.lives -= lives;
        if(instance.lives <= 0)
        {
            GameOver();
        }
    }
    
    /// <summary>
    /// Finish the game on zero lives left.
    /// </summary>
    private static void GameOver()
    {
        Time.timeScale = 0;
        print("game over");
    }
}
