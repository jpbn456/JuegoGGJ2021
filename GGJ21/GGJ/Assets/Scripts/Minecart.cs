using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour
{
    [SerializeField]
    Vector3 direction;
    [SerializeField]
    float speed;
    [SerializeField]
    Rigidbody rb;
    Vector3 startPos;
    void Start()
    {
        
        //rb.velocity = direction * speed;
    }
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    private void OnEnable()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Despawn")
        {
            transform.position = startPos;
            gameObject.SetActive(false);
        }
    }
}
