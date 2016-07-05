using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{ 
    private Rigidbody m_RigidBody;
    private CharacterAbbilities m_Abbilities;

	void Start ()
    {
        m_RigidBody  = GetComponent<Rigidbody>();
        m_Abbilities = GetComponent<CharacterAbbilities>();
    }

	void Update ()
    {
        if (Input.GetButton("Jump") && m_Abbilities.CanJump)
        {
            m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, m_Abbilities.ReduceFactor * m_Abbilities.JumpSpeed, m_RigidBody.velocity.z);
        }

        if (Input.GetButton("Move"))
        {
            Vector3 Direction = Vector3.zero;

            if      (Input.GetAxisRaw("Move") < 0) Direction.x =  1;
            else if (Input.GetAxisRaw("Move") > 0) Direction.x = -1;

            Vector3 NewVelocity = m_RigidBody.velocity;

            NewVelocity.x = Mathf.Clamp(NewVelocity.x - Direction.x * m_Abbilities.ReduceFactor, -m_Abbilities.AverageSpeed, m_Abbilities.AverageSpeed);

            m_RigidBody.velocity = NewVelocity;
        }
    }
}
