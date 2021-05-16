using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelGameOver;
    public Text TextoTempoDeSobrevivencia;
    public Text TextoPontuacaoMaxima;
    private float tempoPontuacaoSalvo;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        scriptControlaJogador = GameObject.FindWithTag(Tags.Jogador).GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
        tempoPontuacaoSalvo = PlayerPrefs.GetFloat(Tags.PontuacaoMaxima);
    }
    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        PainelGameOver.SetActive(true);
    
        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);
        TextoTempoDeSobrevivencia.text = $"Você sobreviveu por {minutos}min e {segundos}s";
        AjustarPontuacaoMaxima(minutos, segundos);
    }
    void AjustarPontuacaoMaxima(int minutos, int segundos)
    {
        if(Time.timeSinceLevelLoad > tempoPontuacaoSalvo)
        {
            tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            TextoPontuacaoMaxima.text = $"Seu melhor tempo é {minutos}min e {segundos}s";
            PlayerPrefs.SetFloat(Tags.PontuacaoMaxima, tempoPontuacaoSalvo);
        }
        if( TextoPontuacaoMaxima.text.Equals(""))
        {
            minutos = (int)(tempoPontuacaoSalvo / 60);
            segundos = (int)(tempoPontuacaoSalvo % 60);
            TextoPontuacaoMaxima.text = $"Seu melhor tempo é {minutos}min e {segundos}s";
        }
    }
    public void Reiniciar()
    {
        SceneManager.LoadScene(Tags.GameScene);
    }
}
