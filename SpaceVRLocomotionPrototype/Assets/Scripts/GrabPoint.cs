using UnityEngine;
using System.Collections;

public class GrabPoint : MonoBehaviour
{
    private ConfigurableJoint joint;
    private Rigidbody playerJoint;
    private Rigidbody rig;

    private float orgDistance;
    private Vector3 direction;

    private bool draw;
    
	// Use this for initialization
    void OnEnable()
    {
    }

    void Start ()
	{
        joint = GetComponent<ConfigurableJoint>();
        playerJoint = GameObject.FindGameObjectWithTag("PlayerJoint").GetComponent<Rigidbody>();
        rig = GameObject.FindGameObjectWithTag("CameraRig").GetComponent<Rigidbody>();
	}

	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Grab(float distance)
    {
        draw = true;
        joint.connectedBody = playerJoint;
        orgDistance = distance;
        //direction = (playerJoint.position - transform.position).normalized;
        //Vector3 anchorPoint = Vector3.MoveTowards(transform.position, playerJoint.position, distance);
        Vector3 fixedVector = rig.transform.InverseTransformPoint(transform.position);
        fixedVector = new Vector3(fixedVector.x * 100, fixedVector.y * 100 - 99, fixedVector.z * 100);
        joint.connectedAnchor = fixedVector;

    }

    public void UpdateGrab(float distance)
    {
        float deltaDistance = orgDistance - distance;
        Vector3 newAnchorPoint = Vector3.MoveTowards(joint.connectedAnchor, playerJoint.position, deltaDistance * 100);
        joint.connectedAnchor = newAnchorPoint;
        orgDistance = distance;
    }

    public void Release()
    {
        joint.connectedBody = null;
        draw = false;

    }

    void OnDrawGizmos() {
        if (!draw) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(joint.connectedAnchor, 0.2f);
    }
}
