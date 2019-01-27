using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MeshSaver : MonoBehaviour
{

    [ContextMenu("Save Mesh")]
    public void SaveMesh()
    {
        Debug.Log("Saving Mesh");
        AssetDatabase.CreateAsset(GetComponent<MeshFilter>().sharedMesh, "Assets/New Mesh.asset");
        AssetDatabase.SaveAssets();
    }
}
