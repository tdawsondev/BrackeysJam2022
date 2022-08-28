using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dart : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private float lifeTimer = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lifeTimer = 5f;
    }

    public void Launch(Vector3 direction)
    {
        if(rb == null)
        {
            rb=GetComponent<Rigidbody>();
        }
        Vector3 force = direction * speed;
        rb.AddForce(force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer-= Time.deltaTime;
        if(lifeTimer < 0)
        {
            Destroy(this);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.instance.GameOver("One of your characters died!");
        }
        Destroy(gameObject);
    }

}
