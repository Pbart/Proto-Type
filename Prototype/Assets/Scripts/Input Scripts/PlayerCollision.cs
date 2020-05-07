using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class handles all the collsions for the player
 *
 * The player can collide with obstacles, collectibles and "pitfalls"
 */

public class PlayerCollision : MonoBehaviour
{
    //TODO: May need to set a parameter/return type for the events and delegates
    public delegate void PlayerCollsionDelegate();

    public static event PlayerCollsionDelegate OnCollectableCollision;
    public static event PlayerCollsionDelegate OnObstacleCollision;
    public static event PlayerCollsionDelegate OnPitfallEnter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectible")
        {
            //Fire the OnCollectableCollision event
            OnCollectableCollision?.Invoke();
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Obstacle")
        {
            //Fire the OnObstacleCollision event
            OnObstacleCollision?.Invoke();
        }
    }
}
