using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    
    
    public Sprite faceTexture; // Defina a imagem do rosto desse inimigo no Inspector

    
    //public Transform[] waypoints; // Array de pontos de patrulha
    //public float waitTimeFirst = 20f; // Tempo de espera no primeiro ponto
    //public float waitTimeOthers = 10f; // Tempo de espera nos outros pontos
    //public float fieldOfViewAngle = 90f; // Ângulo do campo de visão
    //public float detectionRange = 10f; // Distância de detecção do player
    public LayerMask playerLayer; // Layer do player

    //private NavMeshAgent agent;
    //private int currentWaypointIndex = 0;
    // private bool waiting = false;
    private Transform player;
    UIManager uiManager;
    public GameObject ConversarButton;
    public GameObject ImagemFundoDialogo;
    public GameObject TextoDialogo;

    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag("Player")?.transform;
        //StartCoroutine(PatrolRoutine());
        // uiManager = UIManager.Instance;

    }

    void Update()
    {
        //  if (PlayerInSight())
        // {
        //    uiManager.GameOver();
        // }

       
        
    }

    //IEnumerator PatrolRoutine()
    // {
    //  while (true)
    //  {
    //      if (!waiting)
    //   {
    //     agent.SetDestination(waypoints[currentWaypointIndex].position);
    //
    // Espera o inimigo chegar ao ponto
    //   while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
    //  {
    //   if (PlayerInSight()) yield break; // Para o loop se detectar o player
    // yield return null;
    //  }

    //   waiting = true;
    //  float waitTime = (currentWaypointIndex == 0) ? waitTimeFirst : waitTimeOthers;
    //  yield return new WaitForSeconds(waitTime);
    //  waiting = false;

    // Avança para o próximo ponto ou reinicia o ciclo
    //    currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    //  }
    //   yield return null;
    // }
    // }

    // bool PlayerInSight()
    // {
    // if (player == null) return false;

    // Vector3 directionToPlayer = (player.position - transform.position).normalized;
    //float angle = Vector3.Angle(transform.forward, directionToPlayer);

    //  if (angle < fieldOfViewAngle * 0.5f)
    //  {
    //  float distance = Vector3.Distance(transform.position, player.position);
    //  if (distance <= detectionRange)
    //  {
    //  if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRange))
    //  {
    // if (hit.collider.CompareTag("Player"))
    // {
    //    return true;
    //  }
    //}
    // }
    // }
    //return false;
    // }


    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            ConversarButton.SetActive(true);
            
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ConversarButton.SetActive(false);
            ImagemFundoDialogo.SetActive(false);
            

        }
       
    }
}
