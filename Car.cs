using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float totalHealth;
    public float health;
    public bool inCollision = false;
    public bool isTurned = false;

    public Rigidbody rigidbody;
    public Hull hull;

    private void Start()
    {
        totalHealth = 0;
        foreach (AdditionalComponent component in GetComponentsInChildren<AdditionalComponent>())
        {
            totalHealth += component.health;
        }

        totalHealth += GetComponentInChildren<Hull>().health;
        health = totalHealth;
    }


    private void OnCollisionEnter(Collision other)
    {
        hull.impulseMagnitude = other.impulse.magnitude;
        inCollision = true;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name == "Ground" && Mathf.Abs(transform.rotation.z) >= 0.5f &&
            rigidbody.velocity.magnitude <= 1f)
        {
            if (!isTurned)
            {
                isTurned = true;
                Debug.Log("Wait for 3 seconds!");
                StartCoroutine(Flip());
            }
        }
    }

    private IEnumerator Flip()
    {
        yield return new WaitForSeconds(3f);
        if (rigidbody.velocity.magnitude <= 1f)
        {
            rigidbody.velocity = Vector3.zero;
            transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);
            transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        }
        isTurned = false;
    }
}