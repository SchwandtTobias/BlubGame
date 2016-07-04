using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform m_FollowObject;

	void Start ()
    {
	
	}

	void Update ()
    {
        if (m_FollowObject != null)
        {
            transform.position = new Vector3(m_FollowObject.position.x, m_FollowObject.position.y, transform.position.z);
        }
	}
}
