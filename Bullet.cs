using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float speed;
    
    public GameObject muzzle;
    public GameObject hit;


    private void Start()
    {
        SpawnVFX(muzzle);
        Destroy(gameObject, 4f);
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        SpawnVFX(hit);
        Destroy(gameObject);
    }

    private void SpawnVFX(GameObject vfxObject)
    {
        var vfx = Instantiate(vfxObject, transform.position, Quaternion.identity);
        vfx.transform.forward = gameObject.transform.forward;
        var psVfx = vfx.GetComponent<ParticleSystem>();
        if (psVfx != null)
        {
            Destroy(vfx, psVfx.main.duration);
        }
        else
        {
            var psChild = vfx.transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(vfx, psChild.main.duration);
        }
    }
}
