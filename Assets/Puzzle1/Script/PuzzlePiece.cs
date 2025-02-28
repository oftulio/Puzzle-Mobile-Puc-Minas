using UnityEngine;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour
{
    public Image pieceImage;
    public int currentIndex;
    private bool isEmpty = false;
    private PuzzleManager puzzleManager;

    public void Start()
    {
        puzzleManager = Object.FindFirstObjectByType<PuzzleManager>();
    }

    public void SetImage(Sprite sprite, int index)
    {
        pieceImage.sprite = sprite;
        currentIndex = index;
    }

    public void SetEmpty()
    {
        isEmpty = true;
        pieceImage.enabled = false;
    }

    public void OnClick()
    {
        if (!isEmpty)
        {
            TryMove();
        }
    }

    private void TryMove()
    {
        PuzzlePiece emptyPiece = puzzleManager.emptyPiece;
        if (IsAdjacent(emptyPiece))
        {
            SwapWithEmpty(emptyPiece);
            puzzleManager.CheckPuzzleCompletion();
        }
    }

    private bool IsAdjacent(PuzzlePiece other)
    {
        float distance = Vector2.Distance(transform.position, other.transform.position);
        return distance < 1.1f; // Ajuste baseado no tamanho da peça
    }

    private void SwapWithEmpty(PuzzlePiece emptyPiece)
    {
        Vector3 tempPosition = transform.position;
        transform.position = emptyPiece.transform.position;
        emptyPiece.transform.position = tempPosition;
    }
}
