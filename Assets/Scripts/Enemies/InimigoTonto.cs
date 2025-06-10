using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class InimigoTonto : MonoBehaviour
{
    public NavMeshAgent agente;
    public Transform pontoFinal;
    public float tempoTonto = 120f; // 2 minutos
    public float raioMovimento = 3f;

   

    public void AtivarTontura()
    {
        StartCoroutine(FicarTonto());
    }

   public IEnumerator FicarTonto()
    {
        print("chamouuuuu");
        
        float tempoDecorrido = 0f;

        while (tempoDecorrido < tempoTonto)
        {
            Vector3 destinoAleatorio = transform.position + Random.insideUnitSphere * raioMovimento;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(destinoAleatorio, out hit, 2f, NavMesh.AllAreas))
            {
                agente.SetDestination(hit.position);
            }

            yield return new WaitForSeconds(2f); // Espera antes de mudar de destino
            tempoDecorrido += 2f;
        }

        
        agente.SetDestination(pontoFinal.position); // Vai para o destino final
    }
}
