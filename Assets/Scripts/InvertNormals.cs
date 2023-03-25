using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
//скрипт инвертирует фигуру вовнутр
//так как простые фигуры в юнити состоят из многочисленных треугольников,
//достаточно инвертировать нормаль каждого из них
public class InvertNormals : MonoBehaviour
{
    
    void Start()
    {
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
            normals[i] = -1 * normals[i];
        mesh.normals = normals;
        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            int[] tris = mesh.GetTriangles(i);
            for (int j = 0; j < tris.Length; j += 3) 
            {
                int temp = tris[j];
                tris[j] = tris[j + 1];
                tris[j + 1] = temp;
            }
            mesh.SetTriangles(tris, i);
        }
    }
}
