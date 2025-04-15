using UnityEngine;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject painelSettings;
    public RectTransform[] botoes;
    public RectTransform rawImage;
    public float moveDistance; // Distância que o botão vai oscilar
    public float moveSpeedRaw;
    public float duration; // Tempo da animação
    void Start()
    {
        painelSettings.SetActive(false);
        rawImage.DOLocalMoveX(rawImage.localPosition.x + moveSpeedRaw, duration).SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        foreach (RectTransform botao in botoes)
        {
            botao.DOLocalMoveX(botao.localPosition.x + moveDistance, duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openSettings()
    {
        painelSettings.SetActive(true);
    }

    public void closeSettings()
    {
        painelSettings.SetActive(false);
    }
}
