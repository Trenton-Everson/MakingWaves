using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class MeshCustomizer : MonoBehaviour
{
     Mesh myMesh;
     MeshFilter meshFilter;
    

    //Plane Settings
    [SerializeField] Vector2 planeSize = new Vector2(1,1);
    [SerializeField] int planeResolution = 1;

    //Mesh values
    List<Vector3> vertices;
    List<int> triangles;

    void Awake()
    {
        myMesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = myMesh;
    }

    void Update()
    {
        planeResolution = Mathf.Clamp(planeResolution, 1, 50);


        GeneratePlane(planeSize, planeResolution);
        LeftToRightSine(Time.timeSinceLevelLoad);
        AssignMesh();
    }
    
    void GeneratePlane(Vector2 size, int resolution)
    {
        //Create Vertices
        vertices = new List<Vector3>();
        float xPerStep = size.x / resolution;
        float yPerStep = size.y / resolution;

        for (int y = 0; y <= resolution; y++)
        {
            for (int x = 0; x <= resolution; x++)
            {
                vertices.Add(new Vector3(x * xPerStep, 0, y * yPerStep));
            }
        }

        //Create Triangles
        triangles = new List<int>();

        for (int row = 0; row < resolution; row++)
        {
            for (int column = 0; column < resolution; column++)
            {
                int i = (row * resolution) + row + column;
                //First Triangle
                triangles.Add(i);
                triangles.Add(i + (resolution) + 1);
                triangles.Add(i + (resolution) + 2);

                //Second Triangle
                triangles.Add(i);
                triangles.Add(i + resolution + 2);
                triangles.Add(i + 1);
            }
        }

    }
    void AssignMesh()
    {
        myMesh.Clear();
        myMesh.vertices = vertices.ToArray();
        myMesh.triangles = triangles.ToArray();
    }

    void LeftToRightSine(float time)
    {
        for (int i = 0; i < vertices.Count; i++)
        {
            Vector3 vertex = vertices[i];
            vertex.y = Mathf.Sin(time + vertex.x);
            vertices[i] = vertex;
        }
    }


}
