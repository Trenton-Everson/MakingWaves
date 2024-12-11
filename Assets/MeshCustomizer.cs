using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeshCustomizer : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshCollider meshCollider;



    void Start()
    {
        getMeshVertices();
    }
    void getMeshVertices()
    {
        Vector3[] meshVertices = this.meshFilter.mesh.vertices;
    }
}
