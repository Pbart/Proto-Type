using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    private const int DISTANCE_TO_SPAWN_NEXT_LEVEL_OBJECT = 100; //TODO make this public eventually
    private const float TILE_WIDTH_HALF = 2.405f;    //TODO Update level components so I can remove this magic number
    private const float TILE_HEIGHT_HALF = 0.945f;   //TODO Update level components so I can remove this magic number

    [SerializeField]
    private Transform[] levelPieces; //an array (or List) of all the level sections
    [SerializeField]
    private Transform initialLevelPiece; //the start of the level
    [SerializeField]
    private Transform finalLevelPiece; //the last level piece before level ends
    [SerializeField]
    private GameObject player;

    private Vector3 endPosition;
    private Vector3 spawnOffset = new Vector3(TILE_WIDTH_HALF, TILE_HEIGHT_HALF, 0.0f);

    public Sprite item;

    private void Awake()
    {
        Debug.Log("Width: " + item.rect.width + " Height:" + item.rect.height);

        endPosition = initialLevelPiece.Find("EndPosition").transform.position;
        //Transform startPiece = Instantiate(finalLevelPiece, endPosition + new Vector3(TILE_WIDTH_HALF, TILE_HEIGHT_HALF, 0f), Quaternion.identity);
        //startPiece.SetParent(this.transform);
        SpawnLevelPiece();
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, endPosition) <= DISTANCE_TO_SPAWN_NEXT_LEVEL_OBJECT)
        {
            SpawnLevelPiece();
        }
        
    }

    private void SpawnLevelPiece()
    {
        Transform nextLevelPiece = SpawnLevelPiece(endPosition);
        nextLevelPiece.SetParent(this.transform);
        endPosition = nextLevelPiece.Find("EndPosition").position;
        Debug.Log("Current EndPosition is: " + nextLevelPiece.name + " " + nextLevelPiece.Find("EndPosition").name);
    }

    private Transform SpawnLevelPiece(Vector3 spawnPoint)
    {
        Transform nextlevelPiece = Instantiate(levelPieces[Random.Range(0, levelPieces.Length-1)], spawnPoint+spawnOffset, Quaternion.identity);
        return nextlevelPiece;
    }

    //TODO Add logic to spawn the last level piece when player has accomplished an objective.
}


