using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
    public Vector2 m_MapSize = new Vector2(100.0f, 100.0f);

    public Color m_GridColor = Color.white;

    public GameObject[] m_Tiles = new GameObject[GeneralBlock.s_NumberOfBlocks];

    public GeneralBlock.ETypes m_Selected = GeneralBlock.ETypes.NORMAL;

    private float m_Width  = 1.0f;
    private float m_Height = 1.0f;

    void OnDrawGizmos()
    {
        Vector2 HalfMapSize;
		Gizmos.color = this.m_GridColor;

        HalfMapSize.x = Mathf.Floor(m_MapSize.x / 2.0f);
        HalfMapSize.y = Mathf.Floor(m_MapSize.y / 2.0f);

        for (float IndexOfY = -HalfMapSize.y; IndexOfY < HalfMapSize.y + 1; IndexOfY += this.m_Height)
        {
            Gizmos.DrawLine(new Vector3(-HalfMapSize.x, Mathf.Floor(IndexOfY / this.m_Height) * this.m_Height, 0.0f), new Vector3(HalfMapSize.x, Mathf.Floor(IndexOfY / this.m_Height) * this.m_Height, 0.0f));
        }

        for (float IndexOfX = -HalfMapSize.x; IndexOfX < HalfMapSize.x + 1; IndexOfX += this.m_Width)
        {
			Gizmos.DrawLine(new Vector3(Mathf.Floor(IndexOfX / this.m_Width) * this.m_Width, -HalfMapSize.y, 0.0f), new Vector3(Mathf.Floor(IndexOfX / this.m_Width) * this.m_Width, HalfMapSize.y, 0.0f));
		}
    }

    public float Width
    {
        get
        {
            return m_Width;
        }
    }

    public float Height
    {
        get
        {
            return m_Height;
        }
    }

    public GameObject SelectedPrefab
    {
        get
        {
            return m_Tiles[(int)m_Selected];
        }
    }
}
