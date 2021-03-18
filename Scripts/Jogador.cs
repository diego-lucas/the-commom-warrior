using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{

    [SerializeField]
    private int _vidaMax = 3;

    [SerializeField]
    public int _vidaAtual;

    [SerializeField]
    private BarraDeVida _barraDeVida;

    //[SerializeField]
    //private GameObject _arma;

    [SerializeField]
    private Collider2D _areaDeAtaque;

    [SerializeField]
    private GerenciaJogo _gerenciaJogo;

    [SerializeField]
    private GameObject gameOver;

    private Animator _animator;
    

    private void Awake()
    {
        AtualizaVidaUI();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetTrigger("atacando");
        }
    }

    public void TomaDano(int dano)
    {
        _vidaAtual -= dano;
        AtualizaVidaUI();
        if (_vidaAtual <= 0)
        {
            Morre();
        }
    }

    public void Morre()
    {
        Destroy(gameObject);
        GameOver();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
    }

    public void AtualizaVidaUI()
    {
        _barraDeVida.AtualizaBarraDeVida(_vidaAtual, _vidaMax);
    }

    public void IniciaAtaque()
    {
        _areaDeAtaque.enabled = true;
    }

    public void TerminaAtaque()
    {
        _areaDeAtaque.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Diamond")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Portal")
        {
            _gerenciaJogo.EntraPortal();
        }
    }
}
