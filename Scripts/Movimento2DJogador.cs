using Arcaedion.DevDasGalaxias;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Controle2D))]
public class Movimento2DJogador : MonoBehaviour
{

    public Animator animator;

    [SerializeField]
    private float _velocidade = 30f;

    private Controle2D _controle;
    private float _movimentoHorizontal;
    private bool _pulando;

    private void Awake()
    {
        _controle = GetComponent<Controle2D>();
    }
     
    void Update()
    {
        _movimentoHorizontal = Input.GetAxisRaw("Horizontal") * _velocidade;
        animator.SetFloat("speed", Mathf.Abs(_movimentoHorizontal));
        if (Input.GetButtonDown("Jump"))
        {
            _pulando = true;
            //animator.SetBool("isJumping", true);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("atacando", true);
        }

        animator.SetBool("isJumping", !_controle._estaNoChao);
    }

    void FixedUpdate()
    {
        _controle.Movimento(_movimentoHorizontal * Time.fixedDeltaTime, _pulando);
        _pulando = false;
    }
}
