using UnityEngine;

public class BlockWave : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] originalVert; 
    private Vector3[] modifiedVert; 
    private float waveSpeed = 2f;  
    private float waveHeight = 2f;  

    private float[] randomPhases; 

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        GetComponent<Renderer>().material.color = Color.blue;
        originalVert = mesh.vertices;
        modifiedVert = new Vector3[originalVert.Length];
        originalVert.CopyTo(modifiedVert, 0);
        randomPhases = new float[originalVert.Length];
        for (int i = 0; i < randomPhases.Length; i++)
        {
            randomPhases[i] = Random.Range(0f, Mathf.PI * 2f); 
        }
    }

    void Update()
    {
        AnimateWave();
    }

    void AnimateWave()
    {
        for (int i = 0; i < modifiedVert.Length; i++)
        {
            Vector3 vertex = originalVert[i];
            float waveOffset = Mathf.Sin(Time.time * waveSpeed + randomPhases[i]) * waveHeight;
            modifiedVert[i] = Vector3.Lerp(vertex, new Vector3(vertex.x, vertex.y + waveOffset, vertex.z), 0.1f);
        }

        mesh.vertices = modifiedVert;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        
    }

        public float GetSurfaceHeight(Vector3 position)
    {
        Vector3 closestVertex = Vector3.zero;
        float minDistance = float.MaxValue;

        foreach (Vector3 vertex in modifiedVert)
        {
            float distance = Vector3.Distance(new Vector3(position.x, 0, position.z), new Vector3(vertex.x, 0, vertex.z));
            if (distance < minDistance)
            {
                closestVertex = vertex;
                minDistance = distance;
            }
        }
        return closestVertex.y;
    }
}
