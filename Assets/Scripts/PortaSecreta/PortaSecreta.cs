using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor.Rendering;
#endif
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PortaSecreta : MonoBehaviour
{
    public Vector3 rotacaoFinal; // ângulo final da porta em Euler (ex: 0, 90, 0)
    public float velocidadeRotacao = 2f;
    private bool abrir = false;

    private Quaternion rotacaoInicial;
    private Quaternion rotacaoAlvo;
    public GameObject RefPortaSecreta;

    void Start()
    {
        rotacaoInicial = transform.rotation;
        rotacaoAlvo = Quaternion.Euler(rotacaoFinal);
        

    }

    void Update()
    {
        if (abrir)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                rotacaoAlvo,
                velocidadeRotacao * Time.deltaTime * 100

            );

        }

    }

    // Este método é chamado pelo botão
    public void Abrir()
    {

        Destroy(RefPortaSecreta);
            
            UIManager.Instance.MostrarMensagem("Aberto sala secreta");
       

    }
}