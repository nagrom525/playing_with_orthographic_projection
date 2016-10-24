using UnityEngine;
using System.Collections;


// Code borrowed from http://answers.unity3d.com/questions/356180/how-to-make-a-half-cube.html

public class Triangle : MonoBehaviour {
    private Mesh mesh;
    private MeshFilter meshFilter;
    private Vector3[] vertices = {
        new Vector3(0,1,0),
        new Vector3(1,1,0),
        new Vector3(0,0,0),
        new Vector3(1,0,0)
    };

    private int[] triangles = {
        0, 3, 2,
        0, 1, 1
    };

    private


    // Use this for initialization
    void Start() {
        mesh = new Mesh();
        meshFilter = gameObject.GetComponent<MeshFilter>();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        meshFilter.mesh = mesh;
    }
    //called once per frame
	void Update () {
	
	}
}