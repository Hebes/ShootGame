using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 迷雾功能
/// Unity 中的视场效果（视线、视锥）
/// 参考：https://www.youtube.com/watch?v=CSeUMTaNFYk&ab_channel=CodeMonkey
/// </summary>
public class FieldOfView : MonoBehaviour
{
    [Header("视觉宽度")]
    [SerializeField]
    private float fov = 90f;
    [Header("视觉距离")]
    [SerializeField]
    private float viewDistance = 50f;

    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    private Vector3 origin = Vector3.zero;
    private float startingAngle;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        //fov = 90f;//前方距离
        //viewDistance = 50f;//视图的距离
        //origin ;
    }
    private void Update()
    {
        int rayCount = 50;//密度
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;
        

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uV = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];
        vertices[0] = origin;


        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;//三角形的位置
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                // No hit
                vertex = origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                // Hit object
                vertex = raycastHit2D.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)//生成三角形
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uV;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }
    public void SetAimDirection(float aimDirection)
    {
        startingAngle = aimDirection + fov / 2f;
    }

    public void SetFoV(float fov)
    {
        this.fov = fov;
    }

    public void SetViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }
}
