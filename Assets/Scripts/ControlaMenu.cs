using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaMenu : MonoBehaviour
{
    public GameObject BotaoSair;

    void Start()
    {
        #if UNITY_STANDALONE || UNITY_EDITOR
        BotaoSair.SetActive(true);
        #endif
    }

    public void JogarJogo()
    {
        StartCoroutine(MudarCena(Tags.game));
    }

    public void SairDoJogo()
    {
        StartCoroutine(Sair());
    }

    IEnumerator MudarCena(string nomeDaCena)
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(nomeDaCena);
    }

    IEnumerator Sair()
    {
        yield return new WaitForSeconds(0.3f);
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
