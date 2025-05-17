using UnityEngine;
using UnityEngine.UI;

public class FaceSteal : MonoBehaviour
{
    public Image playerFaceUI; // Refer�ncia ao Image da UI que mostra o rosto do player
    private EnemyAI nearbyEnemy; // Refer�ncia ao inimigo mais pr�ximo
    public GameObject RoubarRostoButton;
    public bool RoubouFaceJardineiro;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            nearbyEnemy = other.GetComponent<EnemyAI>();
            RoubarRostoButton.SetActive(true);
        }
        if (other.CompareTag("Jardineiro"))
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
        if (other.CompareTag("Jardineiro"))
        {
            nearbyEnemy = other.GetComponent<EnemyAI>();
            RoubarRostoButton.SetActive(true);
        }
    }

    public void StealFace()
    {
        if (nearbyEnemy != null)
        {
            playerFaceUI.sprite = nearbyEnemy.faceTexture; // Atualiza o rosto do player
            //Destroy(nearbyEnemy.gameObject); // Remove o inimigo
            //nearbyEnemy = null;
            RoubarRostoButton.SetActive(false);
        }
    }
}
