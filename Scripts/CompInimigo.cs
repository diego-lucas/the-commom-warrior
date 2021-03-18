using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompInimigo : MonoBehaviour
{
    [SerializeField]
    private int _vida = 3;

    [SerializeField]
    private GerenciadorDeSons _gerenciadorDeSons;

    public void TomaDano(int dano)
    {
        _vida -= dano;
        if (_gerenciadorDeSons != null)
        {
            _gerenciadorDeSons.TocaAudio("MorteInimigo");
        }

        if (_vida <= 0)
        {
            Morre();
        }

    }

    private void Morre()
    {
        Destroy(gameObject);
    }
}
