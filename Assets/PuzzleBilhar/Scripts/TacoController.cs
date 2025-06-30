using UnityEngine;

public class TacoController : MonoBehaviour
{
    public GameObject canvasBilhar;
    public Rigidbody2D bolaBranca;
    public Transform tacoVisual; // Parte visual do taco (ex: sprite)
    public float maxForce = 15f;
    public float maxPullDistance = 2f;

    private Vector2 dragStart;
    private bool isDragging = false;

    public GameObject player; // Refer�ncia ao player
    private MobileLook playerScript; // Acessa o Script do player, cuidado caso as funcoes estiverem privadas nao podem ser modificadas, coloquei bools no MobileLook para desativar e ativar os touche de movimento e girar
    public GameObject joyStick;

    private void Start()
    {
        playerScript = player.GetComponent<MobileLook>();
    }
    void Update()
    {
        if (BolaEmMovimento()) return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 pos = GetWorldPosition();

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    dragStart = pos;
                    isDragging = true;
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (isDragging) AtualizarTaco(pos);
                    break;

                case TouchPhase.Ended:
                    if (isDragging) Tacar(pos);
                    break;
            }
        }
        transform.position = bolaBranca.position;

    }

    void FixedUpdate()
    {
        if (bolaBranca.linearVelocity.magnitude < 0.01f)
            bolaBranca.linearVelocity = Vector2.zero;
    }

    public void AbrirBilhar()
    {
        canvasBilhar.SetActive(true);
        joyStick.SetActive(false); // Oculta joystick
        playerScript.moveCamera = false;
        playerScript.movePlayer = false;
    }

    public void FecharBilhar()
    {
        canvasBilhar.SetActive(false);
        joyStick.SetActive(true);
        playerScript.moveCamera = true;
        playerScript.movePlayer = true;
    }
    Vector2 GetWorldPosition()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
#else
        return Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
    }

    void AtualizarTaco(Vector2 current)
    {
        Vector2 direction = dragStart - current;
        if (direction.magnitude < 0.05f) return;

        float pullDistance = Mathf.Clamp(direction.magnitude, 0.1f, maxPullDistance);

        // move o taco para trás da bola
        tacoVisual.localPosition = -direction.normalized * pullDistance;
        tacoVisual.right = direction; // gira o taco para apontar na direção do arrasto
    }

    void Tacar(Vector2 release)
    {
        Vector2 direction = dragStart - release;
        float pullDistance = Mathf.Clamp(direction.magnitude, 0.1f, maxPullDistance);
        float force = (pullDistance / maxPullDistance) * maxForce;

        bolaBranca.AddForce(direction.normalized * force, ForceMode2D.Impulse);

        tacoVisual.localPosition = Vector3.zero;
        isDragging = false;
    }

    void AtualizarPosicaoTaco()
    {
        // Coloca o taco no centro da bola branca
        transform.position = bolaBranca.position;
    }

    bool BolaEmMovimento()
    {
        return bolaBranca.linearVelocity.magnitude > 0.05f;
    }

}
