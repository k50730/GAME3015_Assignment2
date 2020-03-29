using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;


public class XMLManager : MonoBehaviour
{
    public static XMLManager instance;

    public GameObjectDataBase gameObjectsDB;
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnCube();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveXML(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameObjectDataBase));
        FileStream stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, gameObjectsDB);
        stream.Close();
    }

    public void LoadXML(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameObjectDataBase));
        FileStream stream = new FileStream(path, FileMode.Open);
        gameObjectsDB = serializer.Deserialize(stream) as GameObjectDataBase;
        stream.Close();

        SpawnCube();
    }

    public GameObject cubePrefab;


    void SpawnCube()
    {
        foreach(GameObject o in GameObject.FindGameObjectsWithTag("Cube"))
        {
            Destroy(o);
        }
        foreach (GameObjects gameObject in gameObjectsDB.gameObjectList)
        {
            GameObject newCube = Instantiate(cubePrefab, gameObject.position, cubePrefab.transform.rotation);
            newCube.tag = "Cube";
            newCube.GetComponent<Renderer>().material.SetColor("_Color", gameObject.color);
        }
    }

}

[System.Serializable]
public class GameObjects
{
    public int ObjectID;
    //public float positionX;
    //public float positionY;
    //public float positionZ;

    public Vector3 position;

    //public float colorR;
    //public float colorG;
    //public float colorB;

    public Color color;
}

[System.Serializable]
public class GameObjectDataBase
{
    public List<GameObjects> gameObjectList = new List<GameObjects>();
}

