using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class GrabableDetection : MonoBehaviour
{
    [SerializeField]
    public PlayerMovement _playerMovement;
    [SerializeField]
    public bool _isLeft;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Grabbable")
        {
            if (_isLeft)
            {
                _playerMovement.GrabableLeft = collider.gameObject;
            }
            else
            {
                _playerMovement.GrabableRight = collider.gameObject;
            }
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (_isLeft)
        {
            if (collider.gameObject == _playerMovement.GrabableLeft)
            {
                _playerMovement.GrabableLeft = null;
            }
        }
        else
        {
            if (collider.gameObject == _playerMovement.GrabableRight)
            {
                _playerMovement.GrabableRight = null;
            }
        }

        
    }
}
