using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PortaGeladeira : MonoBehaviour
{
    public GameObject RefPortaGeladeira;
    public AudioSource audioSource;
    public AudioClip SomPortaAberta;
    public AudioClip SomSemChave;
    public GameObject RefButtonInteragir;
    public GameObject canvasPortaGeladeira;
    public ChaveColetavel chaveColetavel;
    public PortaGeladeira portaGeladeira;
    public bool coletouchave;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 rotacaoFinal; // ângulo final da porta em Euler (ex: 0, 90, 0)
    public float velocidadeRotacao = 2f;
    private bool abrir = false;

    private Quaternion rotacaoInicial;
    private Quaternion rotacaoAlvo;

    void Start()
    {
        rotacaoInicial = transform.rotation;
        rotacaoAlvo = Quaternion.Euler(rotacaoFinal);
        coletouchave = chaveColetavel.ColetouChave;
        
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
        if(chaveColetavel.ColetouChave == true)
        {
            abrir = true;
            audioSource.PlayOneShot(SomPortaAberta);
            Destroy(canvasPortaGeladeira);
        }
        else
            UIManager.Instance.MostrarMensagem("Não tem a chave da cozinha");
        audioSource.PlayOneShot(SomSemChave);

    }


}
