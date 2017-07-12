using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject InventaryUI;
    public Lista Itens;
    public GameObject Grid;
    public GameObject itemPrefab;
    public GameObject Dialog;
    public bool Busy { get; set; }
    public List<string> FalasIniciais;
    public GameObject Fade;
    public PlayerStatus Status;
    

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Dialog = GameObject.Find("Dialog");
        Grid = GameObject.Find("Grid");
        InventaryUI = GameObject.Find("Inventary");
        Itens = new Lista();
        Status = GameObject.Find("ThirdPersonController").GetComponent<PlayerStatus>();



    }

    void Start()
    {
        if (Dialog)
            Dialog.SetActive(false);
        if (InventaryUI)
            InventaryUI.SetActive(false);
        if(Fade)
            Fade.SetActive(false);
        Fade.SetActive(true);
        Fade.GetComponent<Fade>().StartFade();
        CriarDialogo(FalasIniciais, "Guia");
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventaryUI)
                InventaryUI.SetActive(!InventaryUI.activeSelf);
            UpdateInventory();



        }
    }
    public void Pickup(Item item)
    {
        Fade.SetActive(true);
        Fade.GetComponent<Fade>().StartFade();
        Itens.Inserir(item);
        if (Itens.TodasAsPedras())
        {
            Status.EndGame();
        }
    }
    public void UpdateInventory()
    {
        if (Grid.transform.childCount > 0)
            foreach (Transform child in Grid.transform)
                Destroy(child.gameObject);
        Itens.Update(ref Grid, itemPrefab);

    }
    public void CriarDialogo(List<string> _linhas, string _npcname)
    {
        Dialog.SetActive(true);
        Dialog.GetComponentInChildren<Dialogo>().CriarDialogo(_linhas, _npcname);
    }


}
