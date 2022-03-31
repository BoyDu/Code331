using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreator : MonoBehaviour
{
	Mesh hexMesh;
	List<Vector3> vertices;
	List<int> indicies;

    public void Init()
    {
		GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
		vertices = new List<Vector3>();
		indicies = new List<int>();
	}

    public void DrawMesh(Vector3[] cells)
	{
		hexMesh.Clear();
		vertices.Clear();
		indicies.Clear();

		for (int i = 0; i < cells.Length; i++)
		{
			Triangulate(cells[i]);
		}
		hexMesh.vertices = vertices.ToArray();
		hexMesh.SetIndices(indicies.ToArray(), MeshTopology.Lines, 0);
	}

	void Triangulate(Vector3 position)
	{
		Vector3 center = position;
		for (int i = 0; i < 6; i++)
		{
			AddTriangle(
				center + HexMetrics.corners[i],
				center + HexMetrics.corners[i + 1]
			);
		}
	}

	void AddTriangle(Vector3 v1, Vector3 v2)
	{
		int vertexIndex = vertices.Count;
		vertices.Add(v1);
		vertices.Add(v2);
		indicies.Add(vertexIndex);
		indicies.Add(vertexIndex + 1);
	}
}
