using UnityEngine;
using System.Collections;

public class AbbilitiesInterface : MonoBehaviour
{
	public Texture[] m_ActivitySkins = new Texture[CharacterAbbilities.s_NumberOfActivities];
    public CharacterAbbilities m_Abbilities;
    public Transform m_HealthBar;
    public Transform m_ManaBar;
	public GameObject m_Avatar;

    void Start ()
    {
	
	}
	
	void Update ()
    {
        // Change bars
        m_HealthBar.localScale = new Vector3(Mathf.Max(0.0f, m_Abbilities.HealthInPercentage), 1.0f, 1.0f);

        m_ManaBar.localScale = new Vector3(Mathf.Max(0.0f, m_Abbilities.ManaInPercentage), 1.0f, 1.0f);

		// Change avatar

    }
}
