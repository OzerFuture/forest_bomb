using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathCleared : MonoBehaviour
{
    public GameObject bomb;
    public GameObject house;
    private string _targetTag = "Tree";
    private int objectsInTriggerCount = 0;
    private Vector3 _way;
    private Rigidbody RB => bomb.GetComponent<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_targetTag))
        {
            objectsInTriggerCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_targetTag))
        {
            objectsInTriggerCount--;
            CheckObjectsInTrigger();
        }
    }

    private void CheckObjectsInTrigger()
    {
       
        if (objectsInTriggerCount <= 0)
        {
            _way = house.transform.position - bomb.transform.position;
            RB.isKinematic = false;
            RB.velocity = _way / 4;
            RB.AddTorque(Vector3.right);
        }
    }
}

