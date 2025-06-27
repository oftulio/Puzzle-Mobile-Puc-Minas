using UnityEngine;

public class ItemPuzzleClick : MonoBehaviour
{
    private static ItemPuzzleClick itemSelecionado;
    private static Material materialPadrao;
    private static Material materialSelecionado;
    public AudioSource audioSource;
    public AudioClip SomClicando;
    public AudioClip SomDesclicando;
    public AudioClip SomTrocando;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        // Salva o material original se for o primeiro
        if (materialPadrao == null)
            materialPadrao = rend.material;

        // Cria um material de destaque (ex: amarelo semi-transparente)
        if (materialSelecionado == null)
        {
            materialSelecionado = new Material(materialPadrao);
            materialSelecionado.color = Color.yellow;
        }
    }

    void OnMouseDown()
    {
        if (itemSelecionado == null)
        {
            // Seleciona o atual
            itemSelecionado = this;
            rend.material = materialSelecionado;
            audioSource.PlayOneShot(SomClicando);
        }
        else if (itemSelecionado == this)
        {
            // Deseleciona clicando novamente
            rend.material = materialPadrao;
            itemSelecionado = null;
            audioSource.PlayOneShot(SomDesclicando);
        }
        else
        {
            // Troca de posição com o selecionado
            Vector3 posTemp = transform.position;
            transform.position = itemSelecionado.transform.position;
            itemSelecionado.transform.position = posTemp;
            audioSource.PlayOneShot(SomTrocando);

            // Restaura materiais
            itemSelecionado.rend.material = materialPadrao;
            itemSelecionado = null;
            Object.FindAnyObjectByType<PuzzleVerificador>()?.VerificarPuzzle();
        }
        
    }
}
