using UnityEngine;

public class HexGridCreator : MonoBehaviour
{
    Vector3[] meshCells;

    public int width = 6;
    public int height = 6;

    private void Start()
    {
        //CreateMesh();
    }

    public void CreateMesh()
    {
        MeshCreator meshCreator = gameObject.GetComponentInChildren<MeshCreator>();
        meshCreator.Init();

        meshCells = new Vector3[height * width];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }

        meshCreator.DrawMesh(meshCells);
    }

    private void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        meshCells[i] = position;
    }
}
