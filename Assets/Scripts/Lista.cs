using System;
using UnityEngine;
using UnityEngine.UI;

public class Node
{
    public Node Anterior { get; set; }
    public Item Elemento { get; set; }
    public Node Proximo { get; set; }
}

public class Lista
{

    private Node _primeiro;
    private int _tamanho = 0;

    public void Inserir(Item elemento)
    {
        _tamanho++;
        var node = new Node
        {
            Elemento = elemento
        };
        if (Vazia())
        {
            _primeiro = node;
            _primeiro.Proximo = _primeiro;
            _primeiro.Anterior = _primeiro;

        }
        else
        {
            _primeiro.Anterior.Proximo = node;
            node.Anterior = _primeiro.Anterior;
            _primeiro.Anterior = node;
            node.Proximo = _primeiro;
            _primeiro = node;
        }
    }

    internal void Update(ref GameObject Grid, GameObject prefab)
    {
        if (Vazia())
            return;
        var aux = _primeiro;
        for(int a=0; a<_tamanho; a++){
            var element = GameObject.Instantiate(prefab);
            element.GetComponentsInChildren<Image>()[1].overrideSprite = aux.Elemento.img;
            element.transform.SetParent(Grid.transform, false);
            aux = aux.Proximo;
        }
        
    }

    public bool Remover(ref Item elemento)
    {
        Node encontrado;
        if (EstaNaLista(elemento, out encontrado))
        {
            encontrado.Anterior.Proximo = encontrado.Proximo;
            encontrado.Proximo.Anterior = encontrado.Anterior;
            return true;
        }
        return false;

    }

    public bool EstaNaLista(Item elemento, out Node encontrado)
    {
        encontrado = null;
        if (Vazia())
            return false;
        var aux = _primeiro;
        do
        {
            if (aux.Elemento.Equals(elemento))
            {
                encontrado = aux;
                return true;
            }
        } while (aux != _primeiro);
        return false;
    }

    public bool TodasAsPedras()
    {
        if (Vazia())
            return false;
        var aux = _primeiro;
        int cont = 0;
        for(int a=0; a<_tamanho; a++){
            if (aux.Elemento.Name.Contains("Pedra"))
            {
                cont++;
            }
            aux = aux.Proximo;
        }
        Debug.Log(cont);
        return cont == 3;
    }
    public bool Vazia()
    {
        return _primeiro == null;
    }
}
