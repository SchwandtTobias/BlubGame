using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterAbbilities : MonoBehaviour
{
	public static int s_NumberOfActivities = 6;

    public enum EActivities
    {
        NORMAL,
        INAIR,
        BURN,
        FLOAT,
        FREEZE,
        POISON,
    }

    public EActivities m_Activity = EActivities.NORMAL;

	public Texture[] m_ActivitySkins = new Texture[s_NumberOfActivities];

    public float m_Health = 100.0f;

    public float m_Mana = 100.0f;

    public float m_Reducefac = 0.5f;

    public float m_AvgSpeed = 10.0f;

    public float m_JumpSpeed = 10.0f;

    public float m_TimeBetweenJumps = 0.5f;

    public int m_MaxNumberOfJumps = 2;

    private float m_TimeOnGround = 0.0f;

    private float m_TimeInAir = 0.0f;

    private float m_TimeScinceLastJump = 0.0f;

    private int m_NumberOfJumps = 0;

    private bool m_CanJump = false;

    private float m_DamageIndicator = 0.0f;

    private float m_ManaIndicator = 0.0f;

    private Rigidbody m_RigidBody;

    private Renderer m_Renderer;

    void Start ()
    {
        m_RigidBody = GetComponent<Rigidbody>();

        m_Renderer = GetComponent<Renderer>();
    }

	void Update ()
    {
        // Damage + Mana
        m_Health -= m_DamageIndicator * Time.deltaTime;
		m_Mana -= m_ManaIndicator * Time.deltaTime;

        // Air behavior
		if (Input.GetButton("Jump") && m_CanJump)
		{
			m_TimeOnGround = 0.0f;

			m_TimeScinceLastJump = 0.0f;

			m_NumberOfJumps++;
		}

        if (Mathf.Approximately(m_RigidBody.velocity.y, 0.0f))
        {
            m_NumberOfJumps = 0;

            m_TimeOnGround += Time.deltaTime;

            m_TimeInAir = 0.0f;

            m_CanJump = true;

			if (Activity == EActivities.INAIR) Activity = EActivities.NORMAL;
        }
        else
        {
            m_TimeInAir += Time.deltaTime;

            m_TimeScinceLastJump += Time.deltaTime;

            m_CanJump = false;

			if (Activity == EActivities.NORMAL) Activity = EActivities.INAIR;
        }

        if (m_NumberOfJumps < m_MaxNumberOfJumps && m_TimeScinceLastJump > m_TimeBetweenJumps)
        {
            m_CanJump = true;
        }
    }

    void OnTriggerEnter(Collider _Other)
    {
        if (_Other.tag == "Block")
        {
			// Every collision with a block means a new jump is possible
			m_NumberOfJumps = 0;

			// Get default values from block
            m_DamageIndicator = _Other.GetComponent<GeneralBlock>().Damage;
            m_ManaIndicator   = _Other.GetComponent<GeneralBlock>().Mana;

			// Depending on block type we have to change our activity
            switch (_Other.GetComponent<GeneralBlock>().Type)
            {
                case GeneralBlock.ETypes.GRAVITY:
                {
                    Activity = EActivities.FLOAT;
                }
                break;

                case GeneralBlock.ETypes.FIRE:
                {
                    Activity = EActivities.BURN;
                }
                break;
            } 
        }
    }

    void OnTriggerExit(Collider _Other)
    {
        if (_Other.tag == "Block")
        {
            Activity = EActivities.NORMAL;
        }
    }

    void OnCollisionEnter(Collision _Other)
	{
        GameObject GameObject = _Other.gameObject;

        if (GameObject.tag == "Block")
        {
            // Every collision with a block means a new jump is possible
            m_NumberOfJumps = 0;

            // Get default values from block
            m_DamageIndicator = GameObject.GetComponent<GeneralBlock>().Damage;
            m_ManaIndicator   = GameObject.GetComponent<GeneralBlock>().Mana;

            // Depending on block type we have to change our activity
            switch (GameObject.GetComponent<GeneralBlock>().Type)
            {
                case GeneralBlock.ETypes.NORMAL:
                {
                    Activity = EActivities.NORMAL;
                }
                break;

                case GeneralBlock.ETypes.ICE:
                {
                    Activity = EActivities.FREEZE;
                }
                break;

				case GeneralBlock.ETypes.POISON:
				{
					Activity = EActivities.POISON;
				}
				break;
            }
        }
    }

    public EActivities Activity
    {
        get
        {
            return m_Activity;
        }
        set
        {
            m_Activity = value;

            m_Renderer.material.mainTexture = m_ActivitySkins[(int)m_Activity];
        }
    }

    public float HealthInPercentage
    {
        get
        {
            return m_Health / 100.0f;
        }
    }

    public float ManaInPercentage
    {
        get
        {
            return m_Mana / 100.0f;
        }
    }

    public float ReduceFactor
    {
        get
        {
            return m_Reducefac;
        }
    }

    public float AverageSpeed
    {
        get
        {
            return m_AvgSpeed;
        }
    }

    public float JumpSpeed
    {
        get
        {
            return m_JumpSpeed;
        }
    }

    public bool CanJump
    {
        get
        {
            return m_CanJump;
        }
    }
}
