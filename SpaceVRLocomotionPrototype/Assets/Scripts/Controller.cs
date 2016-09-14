using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Controller : MonoBehaviour {

    [SerializeField] private SteamVR_TrackedController controller;
    private CameraRig cameraRig;
    private GrabPoint hoveredHandle;
    private CharacterJoint _jointTargetCharacterJoint;
    private Transform player;
    private Transform playerJoint;

    private bool _isHolding;
    private bool triggerClicked;
    private GrabPoint currentlyGrabbing;

    public void Awake() {
    }

    // Use this for initialization
    void Start()
    {
        controller.TriggerClicked += TriggerClicked;
        controller.TriggerUnclicked += TriggerUnclicked;
        controller.TriggerUsed += OnTriggerUsed;
        controller.Gripped += GripPressed;
        controller.MenuButtonClicked += MenuButtonPressed;
        cameraRig = GameObject.FindGameObjectWithTag("CameraRig").GetComponent<CameraRig>();
        player = Camera.main.transform;
        playerJoint = GameObject.FindGameObjectWithTag("PlayerJoint").transform;
    }

    // Update is called once per frame
    void Update() {
    }

    void OnDisable()
    {
        controller.TriggerClicked -= TriggerClicked;
        controller.TriggerUnclicked -= TriggerUnclicked;
        controller.TriggerUsed -= OnTriggerUsed;
        controller.Gripped -= GripPressed;
        controller.MenuButtonClicked -= MenuButtonPressed;
    }

    public void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Grabbable")
        {
            GrabPoint grabPoint = collider.GetComponent<GrabPoint>();
            if (grabPoint != null)
            {
                hoveredHandle = grabPoint;
            }
        }

    }

    public void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "Grabbable")
        {
            GrabPoint grabPoint = collider.GetComponent<GrabPoint>();
            if (grabPoint != null) {
                if (grabPoint == hoveredHandle) {
                    hoveredHandle = null;
                }
            }
        }
    }

    private void MenuButtonPressed(object sender, ClickedEventArgs e)
    {
        cameraRig.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerJoint.transform.position = new Vector3(0, 1, 0);
    }

    private void GripPressed(object sender, ClickedEventArgs e)
    {
    }

    private void TriggerClicked(object sender, ClickedEventArgs e)
    {
        triggerClicked = true;
        if (hoveredHandle != null) {
         

        }
    }

    private void TriggerUnclicked(object sender, ClickedEventArgs e)
    {
        triggerClicked = false;
    }

    private void OnTriggerUsed(object sender, TriggerValueArgs e)
    {
        if (e.value > 0.3f && !triggerClicked)
        {
            if (_isHolding)
            {
                UpdateGrab(currentlyGrabbing);
            }
            else if (hoveredHandle != null)
                Grab(hoveredHandle);
        }
        else
        {
            if (currentlyGrabbing != null)
                Release();
        }
    }

    void Grab(GrabPoint target)
    {
        target.Grab(Vector3.Distance(transform.position, playerJoint.position));
        currentlyGrabbing = target;
        _isHolding = true;
    }

    void UpdateGrab(GrabPoint target)
    {
        target.UpdateGrab(Vector3.Distance(transform.position, playerJoint.position));
    }

    void Release()
    {
        currentlyGrabbing.Release();
        cameraRig.Release();
        currentlyGrabbing = null;
        _isHolding = false;
    }
}
