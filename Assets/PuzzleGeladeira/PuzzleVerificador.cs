using UnityEngine;

public class PuzzleVerificador : MonoBehaviour
{
    public Transform[] posicoesEmOrdem; // Lista de posi��es corretas na ordem do puzzle

    void Start()
    {
        VerificarPuzzle();
    }

    public void VerificarPuzzle()
    {
        Debug.Log("Verificando puzzle...");
        bool resolvido = true;

        for (int i = 0; i < posicoesEmOrdem.Length; i++)
        {
            Debug.Log($"Esperado ID {i} na posi��o {posicoesEmOrdem[i].position}");
            Transform objetoNaPosicao = ObterObjetoNaPosicao(posicoesEmOrdem[i].position);

            if (objetoNaPosicao == null)
            {
                resolvido = false;
                break;
            }

            ItemIdentificador id = objetoNaPosicao.GetComponent<ItemIdentificador>();
            if (id == null || id.idCorreto != i)
            {
                if (id == null)
                    Debug.LogError($" Nenhum ItemIdentificador encontrado no objeto em {posicoesEmOrdem[i].position}");
                else if (id.idCorreto != i)
                    Debug.LogError($" Objeto com ID {id.idCorreto} est� na posi��o {i}, esperado {i}");
                resolvido = false;
                break;

            }
        }

        if (resolvido)
        {
            Debug.Log("Puzzle Resolvido!");
            // Aqui voc� pode tocar um som, mostrar painel de vit�ria etc.
        }
        else
        {
            Debug.Log("Puzzle ainda n�o resolvido");
        }
    }

    Transform ObterObjetoNaPosicao(Vector3 pos)
    {
        float tolerancia = 0.3f; // margem de erro de posi��o
        GameObject[] todosItens = GameObject.FindGameObjectsWithTag("ItemPuzzle");

        foreach (GameObject item in todosItens)
        {
            if (Vector3.Distance(item.transform.position, pos) < tolerancia)
                return item.transform;
        }

        return null;
    }
}
