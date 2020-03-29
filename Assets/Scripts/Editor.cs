using UnityEditor;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;



public class Editor
{

    [MenuItem("Scene Loader/Save Scene")]
    static void SaveScene() 
    {
        string path = EditorUtility.SaveFilePanel("Save Scene", Application.dataPath, "scene", "xml");
        if(path.Length != 0)
        {
            XMLManager.instance.SaveXML(path);
        }
    }

    [MenuItem("Scene Loader/Load Scene")]
    static void LoadScene() 
    {
        string path = EditorUtility.OpenFilePanel("Load Scene", Application.dataPath, "xml");
        if(path.Length != 0)
        {
            XMLManager.instance.LoadXML(path);
        }

    }


}
