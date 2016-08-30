using UnityEngine;
using System.Collections;

public class PseudoFixedJoin : MonoBehaviour
{

    [SerializeField] private Transform followTarget;
    [SerializeField] private Vector3 rotationOffset = Vector3.zero;
    
	// Update is called once per frame
	void Update ()
	{
	    transform.eulerAngles = followTarget.eulerAngles + rotationOffset;
	    transform.position = followTarget.position;
	}
}
