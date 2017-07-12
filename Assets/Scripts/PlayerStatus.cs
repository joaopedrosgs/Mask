using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public GameController Controller;

    public enum State {
        Normal,
        Recovering,
        EndGame,
        Talking,
        Dead
    }
    public Image StaminaPrism, HpPrism;
    public float _recoveryRate;

    private State _state;
    private float _stamina, _maxStamina = 100;
    private float _hp, _maxHp = 100;
    public GameObject Mask;
    public GameObject[] ToChangeColor;
    public Material EvilMaterial;
    public float MaxStamina {
        get { return _maxStamina; }
        set { _maxStamina = value; }
    }
    
    public float Stamina
    {
        get { return _stamina; }
        set
        {
            _stamina = value;
            StaminaPrism.CrossFadeAlpha(value/100, Time.deltaTime, false);
        }
    }
    public float MaxHp {
        get { return _maxHp; }
        set { _maxHp = value; }
    }
    
    public float Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            HpPrism.CrossFadeAlpha(value/100, Time.deltaTime, false);

        }
    }

    // Use this for initialization
    void Awake()
    {
        Controller = GameObject.Find("GameController").GetComponent<GameController>();
        StaminaPrism = GameObject.Find("StaminaPrism").GetComponent<Image>();
        HpPrism = GameObject.Find("HpPrism").GetComponent<Image>();
        _state = State.Normal;
        Hp = MaxHp;
        Stamina = MaxStamina;
    }

    void Update()
    {
        switch(_state) {
            case State.Normal: {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    SceneManager.LoadScene(0);
                }
                if (Stamina <= 1)
                    _state = State.Recovering;
                else if (Stamina < MaxStamina)
                    Stamina += Time.deltaTime * _recoveryRate;
                
            }
            break;
            case State.Recovering : {
                if (Stamina < MaxStamina / 3)
                    Stamina += Time.deltaTime * _recoveryRate;
                else {
                    _state = State.Normal;
                }
            }
            break;
            case State.Dead:
            {
                _state = State.Normal;
                Controller.Fade.SetActive(true);
                Controller.Fade.GetComponent<Fade>().StartFade();
            }
            break;
            case State.EndGame:
            {
                ToChangeColor[0].GetComponent<NPC>().Falas = new List<string>()
                {
                    "> A agua se tornou sangue...",
                    "> O que? a mascara...",
                    "> Ela grudou no meu rosto, não consigo remove-la"
                };
                ToChangeColor[1].GetComponent<NPC>().Falas = new List<string>()
                {
                    
                    "Você realmente acreditou que precisavamos da sua ajuda?",
                    "Você nada mais é do que um receptaculo, um corpo vazio",
                    "Agora que você reuniu as pedras mágicas, a mascara pode retornar",
                    "Essa mascara contem o Caos, e agora ele está livre, tomando conta do seu corpo aos poucos",
                    "> Como você pôde...",
                    "> Não consigo mais pensar...",
                    "É... o fim..."
                };
                ToChangeColor[1].GetComponent<NPC>().Nome = "Evil";
                foreach (var obj in ToChangeColor)
                {
                    
                    obj.GetComponent<MeshRenderer>().material = EvilMaterial;

                }
                Mask.transform.parent = GameObject.Find("EthanHead").transform;
                Mask.transform.localPosition = new Vector3(-0.1f,-0.1f,0);
                Mask.transform.localRotation =  Quaternion.Euler(0, -90,-180);
                _state = State.Normal;
            }break;
                
            default: {
                _state = State.Normal ;
            }
            break;
        }


    }
    public bool IsRecovering() {
        return _state == State.Recovering;
    }
    public void TakeDamage(float damage)
    {
        Hp = Hp - damage;
        if(Hp<=0)
            _state = State.Dead;
    }

    public void EndGame()
    {
        _state = State.EndGame;
    }
}
