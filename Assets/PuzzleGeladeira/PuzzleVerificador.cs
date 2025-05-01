using UnityEngine;

public class PuzzleVerificador : MonoBehaviour
{
    public ItemPuzzle[] itensNaOrdemCorreta;

    public void VerificarOrdem()
    {
        for (int i = 0; i < itensNaOrdemCorreta.Length; i++)
        {
            if (itensNaOrdemCorreta[i].slotAtual.name != "Slot" + i)
            {
                return; // não está na ordem
            }
        }

        Debug.Log("Puzzle resolvido!");
        // aqui você pode disparar um evento, abrir uma porta, etc.
    }
}
