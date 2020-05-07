using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This class is responsible for handling ALL level based events such as setting the ending
 * of the level when the player collect enough items, Game Over, Score, etc
 */
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int numCollectiblesGathered;

    public Text gemsCollected;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCollision.OnCollectableCollision += UpdateCollectibleAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateCollectibleAmount()
    {
        numCollectiblesGathered++;
        gemsCollected.text = " " + numCollectiblesGathered;
    }
}
