using UnityEngine;
using System.Collections;

public class AnchorUpdate : MonoBehaviour
{

    private SpringJoint _jointLeft;
    private SpringJoint _joinRight;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    SpringJoint[] joints = transform.root.GetComponents<SpringJoint>();
	    foreach (var joint in joints)
	    {
	        joint.anchor = transform.localPosition;
	    }
    }
}
