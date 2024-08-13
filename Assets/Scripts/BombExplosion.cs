using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BombExplosion: MonoBehaviour
{
    public float explosionFactor = 2f;
    public Color startColor = Color.green;
    public Color endColor = Color.yellow;
    public GameObject explosionEffect;
    public MeshRenderer bombMesh;
    private float _explosionRadius;
    private float _objectLifetime = 1f;

    private void Awake()
    {
           StartCoroutine(DestroyObjectAfterTime(3));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            StartCoroutine(DestroyObjectAfterTime(_objectLifetime));
            ColorChange();
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            _explosionRadius = explosionFactor * transform.localScale.x;
        }
    }

    private void ColorChange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in colliders)
        {
       
            if (hit.CompareTag("Tree"))
            {
                Color newColor = Color.Lerp(startColor, endColor, 2);
                hit.gameObject.GetComponent<Renderer>().material.color = newColor;
                

            }
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

  
        foreach (Collider hit in colliders)
        {
            if (hit.CompareTag("Tree"))
            {
                StartCoroutine(hit.gameObject.GetComponent<TreeExplosion>().Explosion());
            }
            bombMesh.enabled = false;
            explosionEffect.GetComponent<ParticleSystem>().Play();
            explosionEffect.GetComponent<AudioSource>().Play();
        }

        
    }

    private IEnumerator DestroyObjectAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Explode();

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);

    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))

        {
            Destroy(gameObject);
        }
    }


}