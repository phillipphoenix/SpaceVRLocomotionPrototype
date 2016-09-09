using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class GrabTest : MonoBehaviour
{

    public SteamVR_TrackedController Controller;
    public Transform CameraRig;

    private AnchorUpdate anchorUpdater;
    private Rigidbody _jointTarget;
    private CharacterJoint _jointTargetCharacterJoint;
    [SerializeField] private GameObject armJointPrefab;
    [SerializeField] private Rigidbody shoulder;

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
        anchorUpdater = CameraRig.GetComponent<AnchorUpdate>();
    }

    // Update is called once per frame
    void Update()
    {
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
            GameObject arm = Instantiate(armJointPrefab, Vector3.Lerp(shoulder.transform.position, _jointTarget.transform.position, 0.5f), Quaternion.identity) as GameObject;
            arm.transform.LookAt(shoulder.transform);
            CharacterJoint arm_shoulderJoint = arm.GetComponent<CharacterJoint>();
            arm_shoulderJoint.autoConfigureConnectedAnchor = true;
            arm_shoulderJoint.connectedBody = CameraRig.GetComponent<Rigidbody>();
            if (shoulder.gameObject.name.Contains("left"))
                arm_shoulderJoint.connectedAnchor = anchorUpdater.leftShoulderLocalPos;
            else
                arm_shoulderJoint.connectedAnchor = anchorUpdater.rightShoulderLocalPos;


            _jointTargetCharacterJoint = _jointTarget.GetComponent<CharacterJoint>();
            _jointTargetCharacterJoint.connectedBody = arm.GetComponent<Rigidbody>();

        }
    }

    private void TriggerUnclicked(object sender, ClickedEventArgs e)
    {
        
    }

    private void OnDrawGizmos()
    {
        
    }
}
