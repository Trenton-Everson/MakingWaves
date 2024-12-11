using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    [SerializeField] int xSize = 20;
    [SerializeField] int zSize = 20;
    [SerializeField] bool addNoise = false;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        createShape();
        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void createShape()
    {
        //A plane of a given size needs x + 1 vertices and z + 1 vertices, x and z being the width and height
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];


        //Now we assign the vertices left to right, bottom to top until we complete a grid of our given size
        for (int i = 0, z = 0; z <= zSize; z++) //loop over the z until we hit the zSize
        {
          for (int x = 0; x <= xSize; x++)//loop over the x until we hit the xSize
          {
            if (addNoise == true)
            {
            float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
            vertices[i] = new Vector3(x, y, z);
            }
            else
            {
            vertices[i] = new Vector3(x, 0, z);
            }
            
            i++;
          }  
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0; //Variable to keep track of the variable we are currently at
        int tris = 0; //Variable to keep track of the triangle we are currently at

        for (int z = 0; z < zSize; z++)
        {
           for (int x = 0; x < xSize; x++)
            {

                //First Triangle  | Drawn from (0,0) to (1, 0) to (0, 1)
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                //Second Triangle | Drawn from (0, 1) to (0, 1) to (1, 1)
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;


            } 
            vert++;
        }
        


        
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    
}
