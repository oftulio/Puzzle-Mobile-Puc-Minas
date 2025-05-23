using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MordomoScript : MonoBehaviour
{
    public Transform[] waypoints; // Array de pontos de patrulha
    public float waitTimeFirst = 20f; // Tempo de espera no primeiro ponto
    public float waitTimeOthers = 10f; // Tempo de espera nos outros pontos
    public float fieldOfViewAngle = 90f; // �ngulo do campo de vis�o
    public float detectionRange = 10f; // Dist�ncia de detec��o do player
    public LayerMask playerLayer; // Layer do player
    public Texture faceSprite; // Defina a imagem do rosto desse inimigo no Inspector


    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private bool waiting = false;
    private Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(PatrolRoutine());
    }

    void Update()
    {
        if (PlayerInSight())
        {
            SceneManager.LoadScene("GameOver"); // Substitua pelo nome da sua cena de Game Over
        }
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            if (!waiting)
            {
                agent.SetDestination(waypoints[currentWaypointIndex].position);

                // Espera o inimigo chegar ao ponto
                while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
                {
                    if (PlayerInSight()) yield break; // Para o loop se detectar o player
                    yield return null;
                }

                waiting = true;
                float waitTime = (currentWaypointIndex == 0) ? waitTimeFirst : waitTimeOthers;
                yield return new WaitForSeconds(waitTime);
                waiting = false;

                // Avan�a para o pr�ximo ponto ou reinicia o ciclo
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
            yield return null;
        }
    }

    bool PlayerInSight()
    {
        if (player == null) return false;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle < fieldOfViewAngle * 0.5f)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= detectionRange)
            {
                if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRange))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
