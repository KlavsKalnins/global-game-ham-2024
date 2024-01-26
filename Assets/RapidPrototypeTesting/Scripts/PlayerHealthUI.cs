using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] List<PlayerHealthPointUI> playerHealthPoints = new List<PlayerHealthPointUI>();
    [SerializeField] int health = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
    }

    public void UpdateHealth(int playerHealth)
    {
        if (playerHealth > 0)
        {
            for (int i = 0; i < playerHealthPoints.Count; i++)
            {
                Debug.Log($"KK: {playerHealth} {i}");
                if (playerHealth > i)
                {
                    Debug.Log($"KK: true {i}");
                    playerHealthPoints[i].UpdateVisual(true);
                }
                else
                {
                    Debug.Log($"KK: false {i}");
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
