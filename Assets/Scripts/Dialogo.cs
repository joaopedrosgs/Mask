using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogo : MonoBehaviour
{
    public List<string> Linhas;
    private int linhaAtual;
    public float velocidade = 0.45f;

    public UnityEngine.UI.Text _text;

    // Use this for initialization
    private bool _revealingText;

    private string _npcName;
    public GameController Controller;
    public GameObject Fade;


    private void Awake()
    {
        Linhas = new List<string>();
        _text = GetComponent<UnityEngine.UI.Text>();
        Controller = GameObject.Find("GameController").GetComponent<GameController>();
    }


    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_revealingText)
            {
                StopCoroutine("MostrarLinha");
                if (Linhas[linhaAtual][0] == '>')
                {
                    _text.text = "Você" + ": " + Linhas[linhaAtual].Substring(1,Linhas[linhaAtual].Length-1);
                } else 
                    _text.text = _npcName + ": " + Linhas[linhaAtual];
                _revealingText = false;
            }
            else
            {
                linhaAtual++;
                if (linhaAtual >= Linhas.Count)
                {
                    if (_npcName == "Evil")
                    {
                        Fade.SetActive(true);
                        Fade.GetComponent<Fade>().StartFadeEnd();
                        Fade.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    _revealingText = false;
                    GameObject.Find("Dialog").SetActive(!GameObject.Find("Dialog").activeSelf);
                    Controller.Busy = false;
                }
                else
                    StartCoroutine("MostrarLinha");
            }
        }
    }

    public IEnumerator MostrarLinha()
    {
        _revealingText = true;
        int caractereAtual = 0;
        if(!_text)
            _text = GetComponent<UnityEngine.UI.Text>();
        _text.text = "";
        string _textoAtual;
        if (Linhas[linhaAtual][0] == '>')
        {
            _textoAtual = "Você" + ": " + Linhas[linhaAtual].Substring(1,Linhas[linhaAtual].Length-1);
        } else 
        _textoAtual = _npcName + ": " + Linhas[linhaAtual];
        int tamanho =_textoAtual.Length;

        while (caractereAtual < tamanho)
        {
            _text.text += _textoAtual[caractereAtual];
            caractereAtual++;
            yield return new WaitForSeconds(velocidade);
        }
        _revealingText = false;
    }

    public bool CriarDialogo(List<string> _linhas, string _npcname)
    {
        if (!_revealingText)
        {
            Linhas = _linhas;
            linhaAtual = 0;
            _npcName = _npcname;
            if(!Controller)
                Controller = GameObject.Find("GameController").GetComponent<GameController>();
            Controller.Busy = true;
            StartCoroutine("MostrarLinha");
            return true;
        }
        return false;
    }
}