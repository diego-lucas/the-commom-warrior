using Arcaedion.DevDasGalaxias;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoIA : MonoBehaviour
{
    [SerializeField]
    private float _velocidade = 20f;

    [SerializeField]
    private LayerMask _layersPermitidas;

    [SerializeField]
    private Vector2 _raycastOffset;

    [SerializeField]
    private float _rangeDetectar;

    [SerializeField]
    private bool _modoZumbi = false;

    [SerializeField]
    private Jogador _jogador;

    private Rigidbody2D _rb;
    private Controle2D _controle;
    private int _andandoParaDireita;
    private float _movimentoHorizontal;
    private Animator _animator;
    private bool _seguindoJogador;
    private bool _estaPulando;
    private int ladoPular;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _controle = GetComponent<Controle2D>();
        _animator = GetComponent<Animator>();
        _andandoParaDireita = 1;
    }

    void Update()
    {

        //Aplica Movimento
        _movimentoHorizontal = _andandoParaDireita * _velocidade;
        _animator.SetFloat("velocidade", Mathf.Abs(_movimentoHorizontal));

        //Detecta Jogador
        float diferencaParaJogador = _jogador.gameObject.transform.position.x - transform.position.x;
        if (Mathf.Abs(diferencaParaJogador) <= 1.5 && _controle._estaNoChao)
        {
            if (diferencaParaJogador < 0)
                ladoPular = -1;
            else
                ladoPular = 1;
            _jogador.transform.position = new Vector2(transform.position.x + (5 * ladoPular), transform.position.y);
            _jogador.TomaDano(1);
        }
        _seguindoJogador = Mathf.Abs(diferencaParaJogador) < _rangeDetectar;
        if (_modoZumbi && _seguindoJogador)
        {
            if (diferencaParaJogador < 0)
            {
                _andandoParaDireita = -1;
            } else
            {
                _andandoParaDireita = 1;
            }
        }

        //Define origens
        var origemX = transform.position.x + _raycastOffset.x;
        var origemY = transform.position.y + _raycastOffset.y;

        //Detecta Parede Direita
        var raycastParedeDireta = Physics2D.Raycast(new Vector2(origemX, origemY), Vector2.right, 1f, _layersPermitidas);
        if (raycastParedeDireta.collider != null)
        {
            _andandoParaDireita = -1;
            
        }

        //Detecta Parede Esquerda
        var raycastParedeEsquerda = Physics2D.Raycast(new Vector2(transform.position.x - _raycastOffset.x, origemY), Vector2.left, 1f, _layersPermitidas);
        if (raycastParedeEsquerda.collider != null)
        {
            _andandoParaDireita = 1;
        }

        //Detecta Chao Direita
        var raycastChaoDireta = Physics2D.Raycast(new Vector2(transform.position.x + _raycastOffset.x, transform.position.y), Vector2.down, 2f, _layersPermitidas);
        if (raycastChaoDireta.collider == null)
        {
            _andandoParaDireita = -1;
        }

        //Detecta Chao Esquerda
        var raycastChaoEsquerda = Physics2D.Raycast(new Vector2(transform.position.x - _raycastOffset.x, transform.position.y), Vector2.down, 2f, _layersPermitidas);
        if (raycastChaoEsquerda.collider == null)
        {
            _andandoParaDireita = 1;
        }
    }

    private void FixedUpdate()
    {
        _controle.Movimento(_movimentoHorizontal * Time.fixedDeltaTime, _estaPulando);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _rangeDetectar);
    }
}
