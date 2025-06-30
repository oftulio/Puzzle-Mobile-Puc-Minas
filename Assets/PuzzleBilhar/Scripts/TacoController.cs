using UnityEngine;

public class TacoController : MonoBehaviour
{
    public Rigidbody2D bolaBranca;
    public Transform tacoVisual;
    public float maxForce = 15f;
    public float maxPullDistance = 2f;

    private Vector2 dragStart;
    private bool isDragging;

    void Update()
    {
        if (BolaEmMovimento()) return;

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            dragStart = GetWorldPosition();
            isDragging = true;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            AtualizarTaco(GetWorldPosition());
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Tacar(GetWorldPosition());
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);

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
