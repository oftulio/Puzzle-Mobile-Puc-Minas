using UnityEngine;

public class TacoFisico : MonoBehaviour
{
    public Rigidbody2D bolaBranca;
    public Transform tacoVisual;
    public float maxForce = 15f;
    public float maxPullDistance = 2f;

    private Vector2 dragStart;
    private bool isDragging;
    public Transform pontoInicial; // ponto onde o taco começa
    public Transform limiteMaximo; // até onde ele pode ser puxado
    public float velocidadeVolta = 20f;
    

    
    private Rigidbody2D rb;
    private bool atirando = false;

    public float forcaAplicada = 10f; // Magnitude da força a ser aplicada
    public Vector2 direcaoForca = Vector2.up; // Direção da força (ex: para cima)
    public ForceMode2D modoForca = ForceMode2D.Impulse; // Tipo de força
   
    public string tagDoObjetoColisor = "Bola"; // Tag do objeto com o qual queremos colidir

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D não encontrado neste GameObject. A força não será aplicada.");
            enabled = false; // Desativa o script se não houver Rigidbody2D
        }
    }

    void Update()
    {
        if (atirando) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                dragStart = touch.position;
                isDragging = true;
            }
            else if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) && isDragging)
            {
                AtualizarPosicao(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended && isDragging)
            {
                Tacar(GetWorldPosition());
            }
        }

    }

    void AtualizarPosicao(Vector2 dragPos)
    {
        Vector2 direction = dragStart - dragPos;
        float deltaX = (dragStart.x - dragPos.x) / Screen.width * 50f; // força
        float clamped = Mathf.Clamp(deltaX, 0f, Vector2.Distance(pontoInicial.position, limiteMaximo.position));

        // move taco para trás (negativo no eixo X)
        transform.position = pontoInicial.position - new Vector3(clamped, 0f, 0f);
    }

  
    void Tacar(Vector2 release)
    {
        Vector2 direction = dragStart - release;
        float pullDistance = Mathf.Clamp(direction.magnitude, 0.1f, maxPullDistance);
        float force = (pullDistance / maxPullDistance) * maxForce;

        bolaBranca.AddForce(direction.normalized * force, ForceMode2D.Impulse);

        tacoVisual.localPosition = Vector3.zero;

        isDragging = false;
        isDragging = false;
        isDragging = false;
        atirando = true;
        StopAllCoroutines();
        StartCoroutine(VoltarComImpulso());
    }

    System.Collections.IEnumerator VoltarComImpulso()
    {
        while (Vector2.Distance(transform.position, pontoInicial.position) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, pontoInicial.position, velocidadeVolta * Time.deltaTime);
            yield return null;
        }

        transform.position = pontoInicial.position;
        atirando = false;
    }
    Vector2 GetWorldPosition()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
#else
        return Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
    }

    bool BolaEmMovimento()
    {
        return bolaBranca.linearVelocity.magnitude > 0.05f;
    }



}
