using UnityEngine;

public class Buraco : MonoBehaviour
{
    public GameManagerSinuca Manager;
    public GameObject ManagerObject;

    private void Start()
    {
        Manager = ManagerObject.GetComponent<GameManagerSinuca>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BolaBranca"))
        {
            Manager.ResetarBilhar(); // se branca cair
        }
        else if (other.CompareTag("BolaVermelha"))
        {
            Manager.Vitoria(); // se vermelha cair
        }

        Destroy(other.gameObject);
    }
}
