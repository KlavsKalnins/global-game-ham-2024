using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    public static PlayerHealthUI Instance;
    [SerializeField] List<PlayerHealthPointUI> playerHealthPoints = new List<PlayerHealthPointUI>();

    private void Awake()
    {
        Instance = this;
    }

/*    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            health -= 1;
            UpdateHealth(health);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            health += 1;
            UpdateHealth(health);
        }
    }*/

    public void UpdateHealth(int playerHealth)
    {
        if (playerHealth > 0)
        {
            for (int i = 0; i < playerHealthPoints.Count; i++)
            {
                if (playerHealth > i)
                {
                    playerHealthPoints[i].UpdateVisual(true);
                }
                else
                {
                    playerHealthPoints[i].UpdateVisual(false);
                }
            }
        } 
        else
        {
            playerHealthPoints.ForEach(p =>
            {
                p.UpdateVisual(false);
            });
        }
    }
}
