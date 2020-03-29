using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIObjects : MonoBehaviour
{
    public GameObject cubePrefab;
    // Start is called before the first frame update
    void Start()
    {
        //SpawnCube();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnCube() 
    {

        foreach(GameObjects gameObject in XMLManager.instance.gameObjectsDB.gameObjectList)
        {
            GameObject newCube = Instantiate(cubePrefab,gameObject.position, cubePrefab.transform.rotation);
            newCube.GetComponent<Renderer>().material.SetColor("_Color", gameObject.color);
        }
    }
}
