using UnityEngine;

public class Buraco : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bola"))
        {
            Destroy(other.gameObject);
            VerificarVitoria();
        }
    }
    public void VerificarVitoria()
    {
        if (GameObject.FindGameObjectsWithTag("Bola").Length == 0)
        {
            // Venceu
            print("Alouuuuuu");
        }
    }
}
