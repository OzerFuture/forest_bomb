using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHouse : MonoBehaviour
{

    public List<Rigidbody> rb = new List<Rigidbody>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            rb[i].isKinematic = true;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
