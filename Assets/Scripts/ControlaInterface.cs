using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject TextoGameOver;
    // Start is called before the first frame update
    void Start()
    {
        scriptControlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }

    public void GameOver()
    {
        TextoGameOver.SetActive(true);
    }
}
