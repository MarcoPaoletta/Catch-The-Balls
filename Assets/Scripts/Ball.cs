using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    
    void Update()
    {
        BottomLimit();
    }

    void BottomLimit()
    {
        if(transform.position.y < -6f)
        {
            Destroy(gameObject);
            GameManager.lives -= 1;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        GameManager.score += 1;
    }
}
