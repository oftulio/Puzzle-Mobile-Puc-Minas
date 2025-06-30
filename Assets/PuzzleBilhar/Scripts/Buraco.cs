using UnityEngine;

public class Buraco : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BolaBranca"))
        {
            GameManagerSinuca.instance.ResetarBilhar(); // se branca cair
        }
        else if (other.CompareTag("BolaVermelha"))
        {
            GameManagerSinuca.instance.Vitoria(); // se vermelha cair
        }

        Destroy(other.gameObject);
    }
}
