using UnityEngine;

public class ItemPuzzleClick : MonoBehaviour
{
    private static ItemPuzzleClick itemSelecionado;
    private static Material materialPadrao;
    private static Material materialSelecionado;

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
        }
        else if (itemSelecionado == this)
        {
            // Deseleciona clicando novamente
            rend.material = materialPadrao;
            itemSelecionado = null;
        }
        else
        {
            // Troca de posição com o selecionado
            Vector3 posTemp = transform.position;
            transform.position = itemSelecionado.transform.position;
            itemSelecionado.transform.position = posTemp;

            // Restaura materiais
            itemSelecionado.rend.material = materialPadrao;
            itemSelecionado = null;
        }
    }
}
