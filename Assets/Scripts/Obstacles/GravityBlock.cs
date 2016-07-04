using UnityEngine;
using System.Collections;

public class GravityBlock : GeneralBlock
{
	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider _Other)
    {
        _Other.GetComponent<Rigidbody>().useGravity = false;
    }

    void OnTriggerExit(Collider _Other)
    {
        _Other.GetComponent<Rigidbody>().useGravity = true;
    }
}
