using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyImage : MonoBehaviour
{
    VertexHelper vertexHelper = new();
    Mesh mesh;
    public Color color;

    // MeshFilter ������Ⱦ�κ����ݣ�����Ҫ�� MeshRenderer һ��ʹ�ã����߸�����Щ����������Ⱦ����Ļ�ϡ�
    MeshFilter meshFilter;  // �洢�������ݣ������ṩģ�͵ļ�����״��������һ�� Mesh ���󣬸ö�������ģ�͵Ķ��㡢�����Ρ����ߡ�UV�������Ϣ��
    MeshRenderer meshRenderer;  // ������Щ������״��Ⱦ����Ļ�ϣ���������ص��Ӿ�Ч���Ͳ��ʡ�
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
        CheckOnClicked();  // UI�Ļ�������
    }

    void LateUpdate()
    {
        DoRenderer();  // UI�Ļ�����Ⱦ
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

        // ͼ�λ������̣���Ӷ����˳�����������ε�˳�����������һһ��Ӧ������᲻�ԣ�
        // ��Ӷ�������
        this.vertexHelper.AddVert(new Vector2(0, 0), color, new Vector2(0, 0));
        this.vertexHelper.AddVert(new Vector2(0, 1), color, new Vector2(0, 1));
        this.vertexHelper.AddVert(new Vector2(1, 1), color, new Vector2(1, 1));
        this.vertexHelper.AddVert(new Vector2(1, 0), color, new Vector2(1, 0));
        // ���ոո���ӵĶ������ݵ�˳����������������������ӳ�һ����������
        this.vertexHelper.AddTriangle(0, 1, 2);
        this.vertexHelper.AddTriangle(2, 3, 0);

        this.vertexHelper.FillMesh(this.mesh);
        this.MeshFilter.mesh = this.mesh;  // ���������ݼ�¼��MeshFilter��
        this.MeshCollider.sharedMesh = this.MeshFilter.mesh;  // ��������Ϣ��¼��meshCollider��
    }
}
