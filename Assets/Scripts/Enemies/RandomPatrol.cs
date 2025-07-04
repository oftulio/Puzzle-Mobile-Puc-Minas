using UnityEngine;
using UnityEngine.AI;

public class RandomPatrol : MonoBehaviour
{
    public float patrolRadius = 10f;
    public float waitTime = 3f;
    public LayerMask groundMask;

    private NavMeshAgent agent;
    private Animator animator;
    private float timer;
    private Vector3 destination;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Busca o Animator no mesmo objeto

        if (animator == null)
        {
            Debug.LogError($"{name}: Animator NÃO encontrado! Verifique se está no mesmo objeto.");
        }
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            timer += Time.deltaTime;

            if (timer >= waitTime)
            {
                SetNewDestination();
                timer = 0f;
            }

            // Parado → Idle
            if (animator != null)
            {
                animator.SetBool("isMoving", false);
                Debug.Log($"{name}: Parado → isMoving = false");
            }
        }
        else
        {
            // Andando → Run
            if (animator != null && agent.velocity.magnitude > 0.1f)
            {
                animator.SetBool("isMoving", true);
                Debug.Log($"{name}: Andando → isMoving = true");
            }
        }
    }

    void SetNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            destination = hit.position;
            agent.SetDestination(destination);
        }
    }
}
