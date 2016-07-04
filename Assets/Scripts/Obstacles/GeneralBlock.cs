using UnityEngine;
using System.Collections;

public class GeneralBlock : MonoBehaviour
{
    public enum ETypes
    {
        NORMAL,
        FIRE,
        GRAVITY,
        ICE,
        POISON
    }

    public ETypes m_Type = ETypes.NORMAL;
    public float m_Mana = 0.0f;
    public float m_Damage = 0.0f;

    void Start ()
    {
	
	}

	void Update ()
    {
	
	}

    public ETypes Type
    {
        get
        {
            return m_Type;
        }
    }

    public float Damage
    {
        get
        {
            return m_Damage;
        }
    }

    public float Mana
    {
        get
        {
            return m_Mana;
        }
    }

}
