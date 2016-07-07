using UnityEngine;
using System.Collections;

public class SpawnPointBehaviour : MonoBehaviour 
{
	public SpawnPointBehaviour m_PreviousSpawnPoint;

	private bool m_IsChecked = false;
	private bool m_HasPreviousSpawnPoint = false;

	void Start () 
	{
		m_HasPreviousSpawnPoint = m_PreviousSpawnPoint != null;
	}

	void Update () 
	{
		
	}

	void OnTriggerEnter(Collider _Other)
	{
		if (m_IsChecked == false && (m_HasPreviousSpawnPoint == false || m_PreviousSpawnPoint.Checked == true))
		{
			GetComponent<Animation>().Play();

			m_IsChecked = true;
		}
	}

	public bool Checked
	{
		get 
		{
			return m_IsChecked;
		}
	}
}
