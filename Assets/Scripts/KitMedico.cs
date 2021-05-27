using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour
{
    private int quantidadeDeCura = 15;
    private int tempoDeDestruicao = 5;

    void Start()
    {
        Destroy(gameObject, tempoDeDestruicao);
    }
    void OnTriggerEnter(Collider objetoDeColisao)
    {
        if(objetoDeColisao.tag.Equals(Tags.Jogador))
        {
            objetoDeColisao.GetComponent<ControlaJogador>().CurarVida(quantidadeDeCura);
            Destroy(gameObject);
        }
    }

}
