﻿using UnityEngine;
using System.Collections;

public class CharacterAbbilities : MonoBehaviour
{
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

    public float m_Health = 100.0f;

    public float m_Mana   = 100.0f;

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

    private Rigidbody m_RigidBody;

    void Start ()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

	void Update ()
    {
        // Damage
        if (m_Activity == EActivities.BURN)
        {
            m_Health -= m_DamageIndicator * Time.deltaTime;
        }

        // Timing and air behaviour
        if (Mathf.Approximately(m_RigidBody.velocity.y, 0.0f))
        {
            m_NumberOfJumps = 0;

            m_TimeOnGround += Time.deltaTime;

            m_TimeInAir = 0.0f;

            m_CanJump = true;
        }
        else
        {
            m_TimeInAir += Time.deltaTime;

            m_TimeScinceLastJump += Time.deltaTime;

            m_CanJump = false;
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
            m_DamageIndicator = _Other.GetComponent<GeneralBlock>().Damage;

            switch (_Other.GetComponent<GeneralBlock>().Type)
            {
                case GeneralBlock.ETypes.GRAVITY:
                {
                    m_Activity = EActivities.FLOAT;
                }
                break;

                case GeneralBlock.ETypes.FIRE:
                {
                    m_Activity = EActivities.BURN;
                }
                break;
            } 
        }
    }

    void OnTriggerExit(Collider _Other)
    {
        if (_Other.tag == "Block")
        {
            m_Activity = EActivities.NORMAL;
        }
    }

    public void ResetTimeOnGround()
    {
        m_TimeOnGround = 0.0f;
    }

    public void ResetTimeSinceLastJump()
    {
        m_TimeScinceLastJump = 0.0f;
    }
    
    public void IncreaseJump()
    {
        m_NumberOfJumps++;
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
