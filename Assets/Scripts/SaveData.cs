using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public float vidaJogador = 100f;
    private bool podeSalvar = false;

    private Vector3 ultimaPosicaoSalva;
    private float ultimaVidaSalva;
    private bool temDadosSalvos = false;


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("PontoDeSalvamento"))
        {
            Debug.Log("Colidiu com o ponto de salvamento! " + "Pressione a tecla 'F5' para salvar.");
            podeSalvar = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CarregaDadosSalvos();
    }

    // Update is called once per frame
    void Update()
    {
        if (podeSalvar && Input.GetKeyDown(KeyCode.F5)) 
        {
            SalvarDadosJogador();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            LimparDadosSalvos();
        }
    }

    public void SalvarDadosJogador()
    {
        ultimaPosicaoSalva = transform.position;
        ultimaVidaSalva = vidaJogador;

        PlayerPrefs.SetFloat("PosX", ultimaPosicaoSalva.x);
        PlayerPrefs.SetFloat("PosY", ultimaPosicaoSalva.y);
        PlayerPrefs.SetFloat("PosZ", ultimaPosicaoSalva.z);
        PlayerPrefs.SetFloat("Vida", ultimaVidaSalva);
        PlayerPrefs.SetInt("TemDadosSalvos", 1);

        Debug.Log($"Jogo salvo! Posição: {ultimaPosicaoSalva}, Vida: {ultimaVidaSalva}");
        PlayerPrefs.Save();
        podeSalvar = false;
    }

    void LimparDadosSalvos()
    {
        PlayerPrefs.DeleteKey("PosX");
        PlayerPrefs.DeleteKey("PosY");
        PlayerPrefs.DeleteKey("PosZ");
        PlayerPrefs.DeleteKey("Vida");
        PlayerPrefs.DeleteKey("TemDadosSalvos");

        ultimaPosicaoSalva = Vector3.zero;
        ultimaVidaSalva = 0f;
        temDadosSalvos = false;

        Debug.Log("Dados salvos foram limpos");
    }

    void CarregaDadosSalvos()
    {
        if(PlayerPrefs.GetInt("TemDadosSalvos", 0) == 1)
        {
            float PosX = PlayerPrefs.GetFloat("PosX");
            float PosY = PlayerPrefs.GetFloat("PosY");
            float PosZ = PlayerPrefs.GetFloat("PosZ");
            ultimaPosicaoSalva = new Vector3( PosX, PosY, PosZ);

            ultimaVidaSalva = PlayerPrefs.GetFloat("Vida");

            transform.position = ultimaPosicaoSalva;
            vidaJogador = ultimaVidaSalva;
            temDadosSalvos = true;

            Debug.Log($"Jogo carregado! Posição: {ultimaPosicaoSalva}, Vida: {ultimaVidaSalva}");
        }
        else
        {
            Debug.Log("Nenhum dado salvo encontrado!");
        }
    }

}
