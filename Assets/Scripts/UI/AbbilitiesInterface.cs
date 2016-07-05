using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbbilitiesInterface : MonoBehaviour
{
	public Sprite[] m_ActivitySkins = new Sprite[CharacterAbbilities.s_NumberOfActivities];
    public CharacterAbbilities m_Abbilities;
    public Transform m_HealthBar;
    public Transform m_ManaBar;
	public Image m_Avatar;



    void Start ()
    {
	
	}
	
	void Update ()
    {
        // Change bars
        m_HealthBar.localScale = new Vector3(Mathf.Max(0.0f, m_Abbilities.HealthInPercentage), 1.0f, 1.0f);

        m_ManaBar.localScale = new Vector3(Mathf.Max(0.0f, m_Abbilities.ManaInPercentage), 1.0f, 1.0f);

        // Change avatar
        int SkinID = (int)m_Abbilities.Activity;

        m_Avatar.sprite = m_ActivitySkins[SkinID];
    }
}
