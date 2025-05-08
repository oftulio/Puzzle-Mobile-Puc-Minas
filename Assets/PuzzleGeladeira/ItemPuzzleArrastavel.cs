using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPuzzle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform slotAtual;
    private Vector3 offset;
    private float zPos;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        zPos = transform.position.z;

        // Garante que comece na posição correta
        if (slotAtual != null)
        {
            transform.position = slotAtual.position;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 worldPoint = cam.ScreenToWorldPoint(eventData.position);
        offset = transform.position - worldPoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPoint = cam.ScreenToWorldPoint(eventData.position);
        Vector3 targetPos = worldPoint + offset;
        targetPos.z = zPos;
        transform.position = targetPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject slotMaisProximo = EncontrarSlotMaisProximo();

        if (slotMaisProximo == null)
        {
            VoltarParaSlotOriginal();
            return;
        }

        // Verifica se há outro item nesse slot
        ItemPuzzle itemNoSlot = EncontrarItemNoSlot(slotMaisProximo.transform);

        if (itemNoSlot != null && itemNoSlot != this)
        {
            // Troca de slots
            Transform slotTemp = slotAtual;

            slotAtual = itemNoSlot.slotAtual;
            itemNoSlot.slotAtual = slotTemp;

            // Atualiza posições
            transform.position = slotAtual.position;
            itemNoSlot.transform.position = itemNoSlot.slotAtual.position;
        }
        else
        {
            // Move para o novo slot (vazio)
            slotAtual = slotMaisProximo.transform;
            transform.position = slotAtual.position;
        }

        // Verifica o puzzle
        PuzzleVerificador verificador = Object.FindFirstObjectByType<PuzzleVerificador>();
        if (verificador != null)
        {
            verificador.VerificarOrdem();
        }
    }

    private void VoltarParaSlotOriginal()
    {
        transform.position = slotAtual.position;
    }

    private GameObject EncontrarSlotMaisProximo()
    {
        GameObject[] todosSlots = GameObject.FindGameObjectsWithTag("SlotGeladeira");

        GameObject maisProximo = null;
        float menorDistancia = float.MaxValue;

        foreach (GameObject slot in todosSlots)
        {
            float dist = Vector3.Distance(transform.position, slot.transform.position);
            if (dist < menorDistancia)
            {
                menorDistancia = dist;
                maisProximo = slot;
            }
        }

        // Opcional: definir um limite de distância mínima para aceitar troca
        if (menorDistancia > 1.5f) // evite troca com slots muito distantes
        {
            return null;
        }

        return maisProximo;
    }

    private ItemPuzzle EncontrarItemNoSlot(Transform slot)
    {
        ItemPuzzle[] todos = Object.FindObjectsByType<ItemPuzzle>(FindObjectsSortMode.None);
        foreach (ItemPuzzle item in todos)
        {
            if (item.slotAtual == slot)
                return item;
        }
        return null;
    }
}
