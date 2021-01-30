using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    float speed;
    [SerializeField]
    MinerMovement minero;
    [SerializeField]
    Transform meshTransform;
    [SerializeField]
    float rotationSpeed;
    void Start()
    {
        
    }
    void Update()
    {
        rb.velocity = new Vector3((-1)*Input.GetAxis("Vertical") * speed, 0, Input.GetAxis("Horizontal")*speed);
        if (rb.velocity != Vector3.zero)
        {
            RotateToFaceDirection();
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            minero.Moverse(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            minero.Detenerse();
        }
    }
    void RotateToFaceDirection()
    {
        float moveVertical = Input.GetAxis("Vertical") * -1;
        float moveHorizontal = Input.GetAxis("Horizontal") * -1;

        Vector3 newPosition = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Quaternion desiredRotation = Quaternion.LookRotation(newPosition);
        meshTransform.rotation = Quaternion.Slerp(meshTransform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }
}
