using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class CubeMovement : NetworkBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField]
    private bool _startWithMovement;
    [SerializeField]
    private Vector3 _initialForce;
    [SerializeField]
    private Vector3 _initialTorque;
    [SerializeField]
    private bool _startWithRandomMovement; // When starting with random movement, _initialForce and _initialTorque are max values.
    [SerializeField]
    private Vector3 _initialForceMin;
    [SerializeField]
    private Vector3 _initialTorqueMin;

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

	// Use this for initialization
	void Start () {
	    if (_startWithMovement)
	    {
	        if (_startWithRandomMovement)
	        {
                _rigidbody.AddForce(RandomRangeVector(_initialForceMin, _initialForce));
                _rigidbody.AddTorque(RandomRangeVector(_initialTorqueMin, _initialTorque));
            }
	        else
	        {
	            _rigidbody.AddForce(_initialForce);
	            _rigidbody.AddTorque(_initialTorque);
	        }
	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private Vector3 RandomRangeVector(Vector3 min, Vector3 max)
    {
        return new Vector3
        {
            x = Random.Range(min.x, max.x),
            y = Random.Range(min.y, max.y),
            z = Random.Range(min.z, max.z)
        };
    }
}
