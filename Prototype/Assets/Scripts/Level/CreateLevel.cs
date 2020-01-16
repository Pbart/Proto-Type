using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    public GameObject background;
    public List<GameObject> backgroundObjs;
    private Camera mainCamera;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadBackgroundObject(GameObject backgroundObject)
    {
        Debug.Log("Loading Object");
    }
}
