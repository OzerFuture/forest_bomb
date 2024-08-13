using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeExplosion : MonoBehaviour
{
    public ParticleSystem explosionEffect;
    public MeshRenderer treeMesh;
    public IEnumerator Explosion()
    {
        treeMesh.enabled = false;
        explosionEffect.Play();

        yield return new WaitForSeconds(0.5f);

        transform.position += 50 * Vector3.down;

        yield return new WaitForSeconds(0.5f);
       
        Destroy(gameObject);
    }
 
}
