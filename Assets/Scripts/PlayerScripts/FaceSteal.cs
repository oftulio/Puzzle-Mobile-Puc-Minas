using UnityEngine;
using UnityEngine.UI;

public class FaceSteal : MonoBehaviour
{
    public Image playerFaceUI; // Referência ao Image da UI que mostra o rosto do player
    private EnemyAI nearbyEnemy; // Referência ao inimigo mais próximo
    public GameObject RoubarRostoButton;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            nearbyEnemy = other.GetComponent<EnemyAI>();
            RoubarRostoButton.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            nearbyEnemy = null;
            RoubarRostoButton.SetActive(false);
        }
    }

    public void StealFace()
    {
        if (nearbyEnemy != null)
        {
            playerFaceUI.sprite = nearbyEnemy.faceTexture; // Atualiza o rosto do player
            Destroy(nearbyEnemy.gameObject); // Remove o inimigo
            nearbyEnemy = null;
            RoubarRostoButton.SetActive(false);
        }
    }
}
