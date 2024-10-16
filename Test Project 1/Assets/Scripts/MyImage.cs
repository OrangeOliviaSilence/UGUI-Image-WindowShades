using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyImage : MonoBehaviour
{
    VertexHelper vertexHelper = new();
    Mesh mesh;
    public Color color;

    // MeshFilter 本身不渲染任何内容，它需要与 MeshRenderer 一起使用，后者负责将这些网格数据渲染到屏幕上。
    MeshFilter meshFilter;  // 存储网格数据，负责提供模型的几何形状。它包含一个 Mesh 对象，该对象定义了模型的顶点、三角形、法线、UV坐标等信息。
    MeshRenderer meshRenderer;  // 负责将这些几何形状渲染到屏幕上，并处理相关的视觉效果和材质。
    public Texture2D texture;
    MeshCollider meshCollider;

    MeshFilter MeshFilter
    {
        get
        {
            if (this.meshFilter == null)
                this.meshFilter = GetComponent<MeshFilter>();
            return this.meshFilter;
        }
    }

    MeshRenderer MeshRenderer
    {
        get
        {
            if (this.meshRenderer == null)
                this.meshRenderer = GetComponent<MeshRenderer>();
            return this.meshRenderer;
        }
    }

    MeshCollider MeshCollider
    {
        get
        {
            if (this.meshCollider == null)
                this.meshCollider = GetComponent<MeshCollider>();
            return this.meshCollider;
        }
    }

    void Start()
    {
        InitMesh();
    }

    void Update()
    {
        CheckOnClicked();  // UI的基础交互
    }

    void LateUpdate()
    {
        DoRenderer();  // UI的基础渲染
    }

    void DoRenderer()
    {
        this.MeshRenderer.material.color = color;
        this.MeshRenderer.material.mainTexture = texture;
    }

    void CheckOnClicked()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (Camera.main == null)
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hitInfo))
                return;

            print($"CheckOnClicked: {this.name}");
        }
    }

    void InitMesh()
    {
        this.mesh = new Mesh();
        this.vertexHelper.Clear();

        // 图形绘制流程（添加顶点的顺序和添加三角形的顺序的索引必须一一对应，否则会不对）
        // 添加顶点数据
        this.vertexHelper.AddVert(new Vector2(0, 0), color, new Vector2(0, 0));
        this.vertexHelper.AddVert(new Vector2(0, 1), color, new Vector2(0, 1));
        this.vertexHelper.AddVert(new Vector2(1, 1), color, new Vector2(1, 1));
        this.vertexHelper.AddVert(new Vector2(1, 0), color, new Vector2(1, 0));
        // 按照刚刚添加的顶点数据的顺序的索引，将各个索引连接成一个个三角形
        this.vertexHelper.AddTriangle(0, 1, 2);
        this.vertexHelper.AddTriangle(2, 3, 0);

        this.vertexHelper.FillMesh(this.mesh);
        this.MeshFilter.mesh = this.mesh;  // 将网格数据记录到MeshFilter内
        this.MeshCollider.sharedMesh = this.MeshFilter.mesh;  // 将网格信息记录到meshCollider内
    }
}
