using UnityEngine;
using System.Collections;

public class DistanceGrab : MonoBehaviour
{

    public GameObject CameraRig;

    public Transform EyeTransform;
    public Transform RightHandTransform;

    public Vector3 EyePosition, RightHandPosition;

    [Range(0, 20)]
    public float Distance;

    public float Spring;
    public float Damper;

    private SpringJoint _joint;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	    EyePosition = EyeTransform.position;
	    RightHandPosition = RightHandTransform.position;
        Distance = Vector3.Distance(EyeTransform.position, RightHandTransform.position)-.2f;
        _joint.spring = Spring;
        _joint.damper = Damper;
        _joint.minDistance = Distance;
        _joint.maxDistance = Distance;
	    //_joint.anchor = EyeTransform.position - transform.position;
	}

    public void Start(Rigidbody target)
    {
        _joint = CameraRig.AddComponent<SpringJoint>();
        _joint.autoConfigureConnectedAnchor = false;
        _joint.connectedBody = target;
        _joint.connectedAnchor = new Vector3();
        _joint.spring = Spring;
        _joint.damper = Damper;
        enabled = true;
    }

    public void Stop()
    {
        Destroy(_joint);
        enabled = false;
    }
}
