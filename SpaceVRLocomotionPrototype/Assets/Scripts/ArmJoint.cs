using UnityEngine;
using System.Collections;

public class ArmJoint : MonoBehaviour {

    private Rigidbody shoulder;

    private CharacterJoint shoulderJoint;
    private CharacterJoint wristJoint;

	// Use this for initialization
	void Start ()
	{
	    return;
	    shoulderJoint = GetComponentsInChildren<CharacterJoint>()[0];
        wristJoint = GetComponentsInChildren<CharacterJoint>()[1];
    }

    public void Attach(Rigidbody newShoulder, Rigidbody joinTarget)
    {
        shoulder = newShoulder;
        shoulderJoint.connectedBody = shoulder;
        shoulderJoint.connectedAnchor = Vector3.zero;

        wristJoint.connectedBody = joinTarget;
        wristJoint.connectedAnchor = Vector3.zero;
    }
}
