using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Velocidade = 30;
    private Rigidbody rigidbodyBala;
    public AudioClip SomDeMorte;
    private int danoDoTiro = 1;

    void Start()
    {
        rigidbodyBala = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigidbodyBala.MovePosition
            (rigidbodyBala.position + transform.forward * Velocidade * Time.deltaTime);
    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        switch(objetoDeColisao.tag)
        {
            case Tags.Inimigo:
                objetoDeColisao.GetComponent<ControlaInimigo>().TomarDano(danoDoTiro);
                break;
            case Tags.Chefe:
                objetoDeColisao.GetComponent<ControlaChefe>().TomarDano(danoDoTiro);
                break;
        }
        Destroy(gameObject);
    }
}
