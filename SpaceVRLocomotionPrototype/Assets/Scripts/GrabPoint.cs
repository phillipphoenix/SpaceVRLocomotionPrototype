using UnityEngine;
using System.Collections;

public class GrabPoint : MonoBehaviour
{
    private ConfigurableJoint joint;
    private Rigidbody playerJoint;
    private Rigidbody rig;

    private float distance;
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
        this.distance = distance;
        direction = (playerJoint.position - transform.position).normalized;
        Vector3 anchorPoint = Vector3.MoveTowards(transform.position, playerJoint.position, distance);
        joint.anchor = transform.InverseTransformPoint(anchorPoint);
        joint.connectedAnchor = anchorPoint;

    }

    public void Release()
    {
        joint.connectedBody = null;
        draw = false;

    }

    void OnDrawGizmos() {
        if (!draw) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(joint.anchor, 0.2f);
    }
}
