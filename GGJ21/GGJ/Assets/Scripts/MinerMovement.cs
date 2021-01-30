using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerMovement : MonoBehaviour
{
   
    [SerializeField]
    Rigidbody rb;
    bool moverse = false;
    Vector3 posicionNueva;
    [SerializeField]
    float distanciaMinima;
    [SerializeField]
    float fallbackSpeed;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float stunnedTime;
    enum MinerState {Following, Still, FallBack};
    MinerState minerActualState;
    Vector3 lastDirection;
    void Start()
    {
        minerActualState = MinerState.Still;
    }

    // Update is called once per frame
    void Update()
    {
        if (minerActualState == MinerState.Following)
        {
            rb.velocity = new Vector3(Mathf.Min(posicionNueva.x - transform.position.x,maxSpeed), 0,Mathf.Min(posicionNueva.z - transform.position.z,maxSpeed));
            if(Vector3.Distance(posicionNueva,transform.position) < distanciaMinima)
            {
                minerActualState = MinerState.Still;
                rb.velocity = Vector3.zero;
            }
        }
        if(rb.velocity != Vector3.zero)
        {
            lastDirection = rb.velocity.normalized;
        }
        //PARA DEBUG
        Debug.Log(minerActualState + "!");
    }
    public void Moverse(Vector3 posicion)
    {
        minerActualState = MinerState.Following;
        posicionNueva = posicion;
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine("Retroceder");
        //CHECKEAR QUE TIPO DE CHOQUE ES
        //CARRO -> screenshake
        //NUBE TOXICA -> postprocessing?
    }
    public void Detenerse()
    {
        minerActualState = MinerState.Still;
        rb.velocity = Vector3.zero;
    }
    IEnumerator Retroceder()
    {
        minerActualState = MinerState.FallBack;
        //rb.velocity = Vector3.forward * -1 * fallbackSpeed;
        rb.velocity = lastDirection * -1 * fallbackSpeed;
        gameObject.layer = 8;
        yield return new WaitForSeconds(stunnedTime);
        gameObject.layer = 0;
        Detenerse();

    }
}
