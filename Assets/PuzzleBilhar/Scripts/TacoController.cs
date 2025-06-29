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
        Vector3 screenPos = Input.mousePosition;
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        screenPos = Input.GetTouch(0).position;
#endif
        return Camera.main.ScreenToWorldPoint(screenPos);
    }

    void AtualizarTaco(Vector2 current)
    {
        Vector2 direction = dragStart - current;

        if (direction.magnitude < 0.05f) return;

        float pullDistance = Mathf.Clamp(direction.magnitude, 0f, maxPullDistance);
        Vector2 offset = direction.normalized * pullDistance;

        tacoVisual.localPosition = -offset;
        tacoVisual.right = direction; // Alinha o taco com a direção do arrastoed;
    }

    void Tacar(Vector2 releasePos)
    {
        Vector2 direction = dragStart - releasePos;

        if (direction.magnitude < 0.05f) return;

        float force = Mathf.Clamp(direction.magnitude / maxPullDistance, 0f, 1f) * maxForce;
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
