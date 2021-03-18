using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GerenciaJogo : MonoBehaviour
{

    [SerializeField]
    private Text diamantesAtuais;

    [SerializeField]
    private Text diamantesTotais;

    [SerializeField]
    private GameObject portal;

    [SerializeField]
    private GameObject nivelConcluido;

    [SerializeField]
    private GameObject venceu;

    private int _totalDiamantes;
    private int _diamantesAtual;
    private bool portalAberto = false;



    private void Awake()
    {
        Time.timeScale = 1;
        _totalDiamantes = GameObject.FindGameObjectsWithTag("Diamond").Length;
        diamantesTotais.text = _totalDiamantes.ToString();
    }

    void Update()
    {
        _diamantesAtual = _totalDiamantes - GameObject.FindGameObjectsWithTag("Diamond").Length;
        diamantesAtuais.text = _diamantesAtual.ToString();

        if (_diamantesAtual == _totalDiamantes && !portalAberto)
            AbrePortal();
        if (Input.GetKeyDown(KeyCode.L))
            EntraPortal();
    }

    private void AbrePortal()
    {
        portalAberto = true;
        portal.SetActive(true);
    }

    public void EntraPortal()
    {
        Time.timeScale = 0;
        if (SceneManager.GetActiveScene().name == "Nivel1")
            nivelConcluido.SetActive(true);
        else
            venceu.SetActive(true);
    }

    public void Venceu()
    {
        Time.timeScale = 0;
        venceu.SetActive(true);
    }

    public void CarregaNivel1()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void CarregaNivel2()
    {
        SceneManager.LoadScene("Nivel2");
    }

    public void TentarNovamente()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
