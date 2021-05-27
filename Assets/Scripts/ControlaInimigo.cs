using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel 
{
    public GameObject Jogador;
    private Animator animatorInimigo;
    private MovimentoPersonagem movimentaInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private Status statusInimigo;
    public AudioClip SomDeMorte;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoEntrePosicoesAleatorias = 4f;
    private float porgentagemGerarKitMedico = 0.1f;
    public GameObject KitMedicoPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag(Tags.Jogador);
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        statusInimigo = GetComponent<Status>();
        AleatorizarZumbi();
    }
    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);
        animacaoInimigo.AnimarMovimento(direcao);
        if (distancia > 15f)
        {
            Vagar();
        }
        else if (distancia > 2.5f)
        {
            direcao = Jogador.transform.position - transform.position;
            movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
            movimentaInimigo.Rotacionar(direcao);
            animacaoInimigo.Atacar(false);
        }
        else
        {
            direcao = Jogador.transform.position - transform.position;
            animacaoInimigo.Atacar(true);
        }
        
    }
    void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        if (contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoEntrePosicoesAleatorias;
        }

        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05f;
        if (ficouPertoOSuficiente == false)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
            movimentaInimigo.Rotacionar(direcao);
        }
    }
    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 10f;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }
    void AtacaJogador()
    {
        int dano = Random.Range(20, 30);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    void AleatorizarZumbi()
    {
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        statusInimigo.Vida -= dano;
        if (statusInimigo.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        Destroy(gameObject);
        ControlaAudio.instancia.PlayOneShot(SomDeMorte);
        VerificaGeracaoKitMedico(porgentagemGerarKitMedico);
    }

    void VerificaGeracaoKitMedico(float porcentagemGeracao)
    {
        if(Random.value <= porcentagemGeracao)
        {
            Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        }
    }
}
