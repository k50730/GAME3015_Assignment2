using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

[ExecuteInEditMode]
public class XMLManager 
{
    public static XMLManager instance;
  

    public static GameObjectDataBase gameObjectsDB = new GameObjectDataBase();
    //void Awake()
    //{
        
    //}

    //// Start is called before the first frame update
    //void Start()
    //{
    //    //SpawnCube();

    //    instance = this;
    //    Debug.Log(instance);

    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public static void SaveXML(string path)
    {
 
        
        var allGameObjects = Object.FindObjectsOfType<GameObject>();

        foreach (GameObject o in allGameObjects)
        {
            if (o.tag == "Untagged")
            {
                Debug.Log("Found a game object");
                GameObjects info = new GameObjects();
                info.name = o.name;
                info.position = o.transform.position;
                info.rotation = o.transform.rotation;
                info.scale = o.transform.localScale;
                info.mesh = o.GetComponent<MeshFilter>().sharedMesh;
                //info.shader = o.GetComponent<Renderer>().sharedMaterial.shader;
                info.color = o.GetComponent<Renderer>().sharedMaterial.GetColor("_Color");
                gameObjectsDB.gameObjectList.Add(info);
            }
        }

        XmlSerializer serializer = new XmlSerializer(typeof(GameObjectDataBase));
        FileStream stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, gameObjectsDB);
        stream.Close();
        
        gameObjectsDB.gameObjectList.Clear();
        Debug.Log(gameObjectsDB.gameObjectList.Count);

    }

    public static void LoadXML(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameObjectDataBase));
        FileStream stream = new FileStream(path, FileMode.Open);
        gameObjectsDB = serializer.Deserialize(stream) as GameObjectDataBase;
        stream.Close();

        SpawnCube();

    }

    //public GameObject cubePrefab;


    static void SpawnCube()
    {
        foreach(GameObject o in Object.FindObjectsOfType<GameObject>())
        {
            if (o.tag == "Untagged")
                Object.DestroyImmediate(o);
        }
        
        foreach (GameObjects gameObject in gameObjectsDB.gameObjectList)
        {
            Debug.Log("Adding A GameObject");
            GameObject newCube = new GameObject(gameObject.name);
            //newCube = Object.Instantiate(new GameObject(), gameObject.position, new Quaternion(0, 0, 0, 1));
            newCube.transform.position = gameObject.position;
            newCube.transform.rotation = gameObject.rotation;
            newCube.transform.localScale = gameObject.scale;
            newCube.AddComponent<MeshFilter>().sharedMesh = gameObject.mesh;
            newCube.AddComponent<MeshRenderer>();
            //newCube.AddComponent<Renderer>();
            newCube.name = gameObject.name;
            newCube.tag = "Untagged";
            newCube.GetComponent<Renderer>().sharedMaterial = new Material(Shader.Find("Standard"));
            newCube.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", gameObject.color);
        }
    }

}

[System.Serializable]
public class GameObjects
{
    public string name;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public Mesh mesh;
    public Color color;
}

[System.Serializable]
public class GameObjectDataBase
{
    public List<GameObjects> gameObjectList = new List<GameObjects>();
}

