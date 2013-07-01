using UnityEngine;
using System.Collections;

public class BaseHealth : MonoBehaviour
{
    private static BaseHealth instance;
    [SerializeField]
    private int lives = 20;

	void Awake()
    {
        instance = this;
	}
    
    public static void ApplyDamage(int lives)
    {
        instance.lives -= lives;
        if(instance.lives <= 0)
        {
            GameOver();
        }
    }
    
    private static void GameOver()
    {
        Time.timeScale = 0;
        print("game over");
    }
}
