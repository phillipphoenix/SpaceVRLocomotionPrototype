using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class Player : NetworkBehaviour
{

    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _clickSound;

    public void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetMouseButtonDown(0))
	    {
	        RaycastHit hit;
	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (Physics.Raycast(ray, out hit))
	        {
	            var cubeMouseInteraction = hit.transform.GetComponent<CubeMouseInteraction>();
	            if (cubeMouseInteraction != null)
	            {
	                _audioSource.clip = _clickSound;
	                _audioSource.Play();
	                CmdAddForceToCube(cubeMouseInteraction.GetComponent<NetworkIdentity>(),
	                    cubeMouseInteraction.ClickForceAdd);
	            }
	        }
	    }
	}

    [Command]
    public void CmdAddForceToCube(NetworkIdentity cubeNid, Vector3 force)
    {
        cubeNid.GetComponent<Rigidbody>().AddForce(force);
    }
}
