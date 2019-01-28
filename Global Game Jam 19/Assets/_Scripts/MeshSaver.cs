using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MeshSaver : MonoBehaviour
{
#if UNITY_EDITOR
    [ContextMenu("Save Mesh")]
    public void SaveMesh()
    {
        Debug.Log("Saving Mesh");
        AssetDatabase.CreateAsset(GetComponent<MeshFilter>().sharedMesh, "Assets/New Mesh.asset");
        AssetDatabase.SaveAssets();
    }
#endif
}
