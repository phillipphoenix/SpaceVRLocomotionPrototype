using UnityEngine;
using System.Collections;

public class AnchorUpdate : MonoBehaviour
{
    [SerializeField] private Transform leftShoulder;
    [SerializeField] private Transform rightShoulder;

    public Vector3 leftShoulderLocalPos;
    public Vector3 rightShoulderLocalPos;
    
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update ()
	{
	    leftShoulderLocalPos = leftShoulder.position - transform.position;
        rightShoulderLocalPos = rightShoulder.position - transform.position;
	}
}
