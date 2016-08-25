using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class GrabMovement : MonoBehaviour
{

    public SteamVR_TrackedController Controller;
    public Transform CameraRig;

    public float Distance;

    private Rigidbody _jointTarget;
    private SpringJoint _springJoint;

    private bool _isHolding;

    public void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        Controller.TriggerClicked += TriggerClicked;
        Controller.TriggerUnclicked += TriggerUnclicked;
        CameraRig = transform.root;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isHolding)
        {
            Distance = Vector3.Distance(_springJoint.transform.position + _springJoint.anchor, transform.position);
            _springJoint.minDistance = Distance;
            _springJoint.maxDistance = Distance;
        }
        else
        {
            Distance = 0;
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.isTrigger)
        {
            var rigidBody = collider.GetComponent<Rigidbody>();
            if (rigidBody != null)
            {
                _jointTarget = rigidBody;
            }
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.isTrigger)
        {
            var rigidBody = collider.GetComponent<Rigidbody>();
            if (rigidBody == _jointTarget)
            {
                _jointTarget = null;
            }
        }
    }

    private void TriggerClicked(object sender, ClickedEventArgs e)
    {
        if (_jointTarget != null)
        {
            _springJoint = CameraRig.gameObject.AddComponent<SpringJoint>();
            _springJoint.spring = 50;
            _springJoint.damper = 50;
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.connectedAnchor = new Vector3(0, 0, 0);

            _springJoint.connectedBody = _jointTarget;
            _isHolding = true;
        }
    }

    private void TriggerUnclicked(object sender, ClickedEventArgs e)
    {
        Destroy(_springJoint);
        _springJoint = null;
        _isHolding = false;
    }

    private void OnDrawGizmos()
    {
        if (_springJoint != null)
        {
            Gizmos.color = Color.red;
            var start = _springJoint.transform.localPosition + _springJoint.anchor;
            var dir = _springJoint.connectedBody.transform.position - (_springJoint.transform.position + _springJoint.anchor);
            Gizmos.DrawLine(start, start + (Vector3.Normalize(dir) * Distance));
        }
    }
}
