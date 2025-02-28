using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    public GameObject puzzlePanel; // Painel que contém o puzzle
    public Button exitButton; // Botão para sair do puzzle
    public Transform gridParent; // O pai das peças do puzzle (Grid Layout)
    public GameObject piecePrefab; // Prefab da peça do puzzle
    public Sprite[] puzzleSprites; // Imagens do puzzle

    private List<PuzzlePiece> pieces = new List<PuzzlePiece>();
    public PuzzlePiece emptyPiece;

    private PlayerMovement playerMovement;
    private Camera playerCamera;

    void Start()
    {
        exitButton.onClick.AddListener(ExitPuzzle);
        InitializePuzzle();
    }

    void InitializePuzzle()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject piece = Instantiate(piecePrefab, gridParent);
            PuzzlePiece puzzlePiece = piece.GetComponent<PuzzlePiece>();
            puzzlePiece.SetImage(puzzleSprites[i], i);
            pieces.Add(puzzlePiece);
        }
        emptyPiece = pieces[8]; // Última peça será vazia
        emptyPiece.SetEmpty();
        ShufflePuzzle();
    }

    void ShufflePuzzle()
    {
        // Embaralha as peças
        for (int i = 0; i < pieces.Count; i++)
        {
            int randomIndex = Random.Range(0, pieces.Count);
            (pieces[i].transform.position, pieces[randomIndex].transform.position) = (pieces[randomIndex].transform.position, pieces[i].transform.position);
        }
    }

    public void StartPuzzle()
    {
        puzzlePanel.SetActive(true);
        playerMovement.enabled = false;
        playerCamera.enabled = false;
    }

    public void ExitPuzzle()
    {
        puzzlePanel.SetActive(false);
        playerMovement.enabled = true;
        playerCamera.enabled = true;
    }

    public void CheckPuzzleCompletion()
    {
        bool correct = true;
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].currentIndex != i)
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            Debug.Log("Puzzle Completo!");
            ExitPuzzle();
        }
    }
}
