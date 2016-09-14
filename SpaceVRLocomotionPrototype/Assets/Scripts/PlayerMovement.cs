using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private SteamVR_TrackedController _controllerLeft;
    [SerializeField]
    private SteamVR_TrackedController _controllerRight;

    private Rigidbody _rigidbody;

    public bool UseRotation;

    [SerializeField]
    public GameObject GrabableLeft, GrabableRight;

    private bool _isGrabbing;
    private Vector3 _startPosition, _startControllerPosition;
    private Vector3 _startRotation, _startControllerRotation;
    private Transform _grabbingController;
    private Vector3? _lastPosition;

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {
	    _controllerLeft.TriggerClicked += OnTriggerClicked;
        _controllerRight.TriggerClicked += OnTriggerClicked;
        _controllerLeft.TriggerUnclicked += OnTriggerUnclicked;
        _controllerRight.TriggerUnclicked += OnTriggerUnclicked;

        _controllerLeft.TriggerUsed += OnTriggerUsed;
        _controllerRight.TriggerUsed += OnTriggerUsed;
        _controllerLeft.Gripped += OnGripped;
        _controllerRight.Gripped += OnGripped;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_isGrabbing)
        {
            transform.position = _startPosition - (GetPosition(_grabbingController) - _startControllerPosition);
            // Rotation doesn't work properly...
            if (UseRotation)
            {
                transform.eulerAngles = _startRotation - (_grabbingController.eulerAngles - _startControllerRotation);
            }
        }
    }

    // Some magic function from the VR toolkit...
    private Vector3 GetPosition(Transform objTransform)
    {
        return transform.localRotation * objTransform.localPosition;
    }

    private void OnTriggerClicked(object sender, ClickedEventArgs e)
    {
        if (!_isGrabbing && (GrabableLeft != null || GrabableRight != null))
        {
            _startPosition = transform.position;
            _startRotation = transform.eulerAngles;
            _grabbingController = ((SteamVR_TrackedController)sender == _controllerLeft ? _controllerLeft : _controllerRight).transform;
            _startControllerPosition = GetPosition(_grabbingController);
            _startControllerRotation = _grabbingController.eulerAngles;
            _isGrabbing = true;
        }
    }

    private void OnTriggerUnclicked(object sender, ClickedEventArgs e)
    {
        if (_isGrabbing)
        {
            _isGrabbing = false;

            // Trying to apply velocity after releasing.
            //_rigidbody.velocity = SteamVR_Controller.Input((int) _grabbingController.GetComponent<SteamVR_TrackedController>().controllerIndex).velocity + new Vector3(0.0f, -0.001f, 0.0f); ;
        }
    }

    private void OnTriggerUsed(object sender, TriggerValueArgs e)
    {
        
    }

    private void OnGripped(object sender, ClickedEventArgs e)
    {

    }
}
