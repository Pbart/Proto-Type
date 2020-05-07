using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    private const int DISTANCE_TO_SPAWN_NEXT_LEVEL_OBJECT = 100; //TODO make this public eventually
    private const float TILE_WIDTH_HALF = 2.40f;    //TODO Update level components so I can remove this magic number
    private const float TILE_HEIGHT_HALF = 0.95f;   //TODO Update level components so I can remove this magic number

    [SerializeField]
    private Transform[] levelPieces; //an array (or List) of all the level sections that can be spawned
    [SerializeField]
    private Transform firstLevelPiece; //the "first" level pieces
    [SerializeField]
    private Transform initialLevelPiece; //the start of the level
    [SerializeField]
    private Transform finalLevelPiece; //the last level piece before level ends
    [SerializeField]
    private GameObject player;

    private Vector3 endPosition;
    private Vector3 spawnOffset = new Vector3(TILE_WIDTH_HALF, TILE_HEIGHT_HALF, 0.0f);
    private Vector3 screenBounds;

    private void Awake()
    {
        endPosition = initialLevelPiece.Find("EndPosition").transform.position;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Debug.Log(screenBounds);
        //Transform startPiece = Instantiate(finalLevelPiece, endPosition + new Vector3(TILE_WIDTH_HALF, TILE_HEIGHT_HALF, 0f), Quaternion.identity);
        //startPiece.SetParent(this.transform);
        SpawnLevelPiece();
        firstLevelPiece = this.transform.GetChild(0);
        Debug.Log(firstLevelPiece.GetChild(firstLevelPiece.childCount - 1));
    }

    private void Update()
    {
        Debug.Log(Vector3.Distance(firstLevelPiece.GetChild(firstLevelPiece.childCount - 1).position, player.transform.position));
        //Spawn a level piece when the player is a certain distance away
        if (Vector3.Distance(player.transform.position, endPosition) <= DISTANCE_TO_SPAWN_NEXT_LEVEL_OBJECT)
        {
            SpawnLevelPiece();
        }
        if (Vector3.Distance(firstLevelPiece.GetChild(firstLevelPiece.childCount - 1).position, player.transform.position) > 25) //TODO remove magic number
        {
            Debug.Log(firstLevelPiece.name + " is off screen");
            DespawnLevelPiece(firstLevelPiece);
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

    private void DespawnLevelPiece(Transform levelPiece)
    {
        firstLevelPiece = this.transform.GetChild(1);
        Debug.Log("Destroying " + levelPiece.name);
        Destroy(levelPiece.gameObject);
    }

    //TODO Add logic to spawn the last level piece when player has accomplished an objective.
}


