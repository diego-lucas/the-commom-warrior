using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    [SerializeField]
    private Sprite _vidaCheia;

    [SerializeField]
    private Sprite _vidaVazia;

    [SerializeField]
    private GameObject _vida;

    private List<GameObject> _listaVidas = new List<GameObject>();

    public void AtualizaBarraDeVida(int vidaAtual, int totalDeVidas)
    {
        ResetaLista();
        for (int i = 0; i < totalDeVidas; i++)
        {
            if (vidaAtual <= i)
            {
                _vida.GetComponent<Image>().sprite = _vidaVazia;
            }
            else
            {
                _vida.GetComponent<Image>().sprite = _vidaCheia;
            }

            var posX = transform.position.x + (25 * i);
            var go = Instantiate(_vida, new Vector3(posX, transform.position.y, 0), Quaternion.identity, this.transform);
            _listaVidas.Add(go);
        }
    }

    private void ResetaLista()
    {
        foreach (var vida in _listaVidas)
        {
            Destroy(vida);
        }
    }
}
