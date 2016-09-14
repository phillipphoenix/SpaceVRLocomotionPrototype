using UnityEngine;
using System.Collections;

public class CameraRig : MonoBehaviour
{

    [SerializeField] private Rigidbody rBody;

    private Vector3 lastPosition;
    private Vector3 currentVelocity;

    void Start()
    {
        lastPosition = transform.position;
    }

	void Update ()
	{
	    rBody.centerOfMass = new Vector3(0, 1, 0);

        currentVelocity = (lastPosition - transform.position) / Time.deltaTime;
	    lastPosition = transform.position;

        Debug.Log(rBody.velocity);
    }

    public void Release()
    {
        rBody.velocity = currentVelocity;

    }
}
