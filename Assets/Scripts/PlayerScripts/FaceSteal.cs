﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FaceSteal : MonoBehaviour
{
    
    public Image playerFaceUI; // Referência ao Image da UI que mostra o rosto do player
    private EnemyAI nearbyEnemy; // Referência ao inimigo mais próximo
    public GameObject RoubarRostoButton;
    public bool RoubouFaceJardineiro;
    public DialogoMordomo dialogoMordomo;
    public GameObject canvas;
    public bool TerminouDiologoMordomo;
    public GameObject birdsPrefab;
    public Transform headPosition; // Posição da cabeça do inimigo
    public InimigoTonto inimigotonto;
    public GameObject inimigo;
    public GameObject MordomoScript;
    [Header("Configurações")]
    public float anguloDeVisao = 60f;
    public string nomeCenaGameOver = "GameOver";



    private void Start()
    {
        dialogoMordomo = canvas.GetComponent<DialogoMordomo>();
        inimigotonto = inimigo.GetComponent<InimigoTonto>();
        TerminouDiologoMordomo = dialogoMordomo.DialogoTerminou;
    }
    

    
    void OnTriggerEnter(Collider other)
    {
        if (TerminouDiologoMordomo == true)
        {
            if (other.CompareTag("Mordomo"))
            {
                nearbyEnemy = other.GetComponent<EnemyAI>();
                RoubarRostoButton.SetActive(true);
            }
        }
           
        if (other.CompareTag("Jardineiro"))
        {
            nearbyEnemy = other.GetComponent<EnemyAI>();
            RoubarRostoButton.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
            if (other.CompareTag("Mordomo"))
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

    public void StealFaceJardineiro()
    {
     

            if (nearbyEnemy == null) return;

            Transform inimigoTransform = nearbyEnemy.transform;

            Vector3 direcaoDoInimigo = inimigoTransform.forward;
            Vector3 direcaoParaPlayer = (transform.position - inimigoTransform.position).normalized;

            float angulo = Vector3.Angle(direcaoDoInimigo, direcaoParaPlayer);

        if (angulo < anguloDeVisao)
        {
            // Está na frente → Game Over
            SceneManager.LoadScene(nomeCenaGameOver);
        }

        else
        {
            playerFaceUI.sprite = nearbyEnemy.faceTexture; // Atualiza o rosto do player
            //Destroy(nearbyEnemy.gameObject); // Remove o inimigo
            //nearbyEnemy = null;
            RoubarRostoButton.SetActive(false);
            PlayerFaceManager.Instance.currentFace = "Jardineiro";
            Instantiate(birdsPrefab, headPosition.position, Quaternion.identity, headPosition);
            inimigotonto.AtivarTontura();
        }
        
        
    }
    public void StealFaceMordomo()
    {

        if (nearbyEnemy == null) return;

        Transform inimigoTransform = nearbyEnemy.transform;

        Vector3 direcaoDoInimigo = inimigoTransform.forward;
        Vector3 direcaoParaPlayer = (transform.position - inimigoTransform.position).normalized;

        float angulo = Vector3.Angle(direcaoDoInimigo, direcaoParaPlayer);

        if (angulo < anguloDeVisao)
        {
            // Está na frente → Game Over
            SceneManager.LoadScene(nomeCenaGameOver);
        }

        else
        {
            print("aaaaaaaaaaaaaaa");

            playerFaceUI.sprite = nearbyEnemy.faceTexture; // Atualiza o rosto do player
            //Destroy(nearbyEnemy.gameObject); // Remove o inimigo
            //nearbyEnemy = null;
            //RoubarRostoButton.SetActive(false);
            PlayerFaceManager.Instance.currentFace = "Mordomo";
            Instantiate(birdsPrefab, headPosition.position, Quaternion.identity, headPosition);
            inimigotonto.AtivarTontura();
            MordomoScript.GetComponent<RandomPatrol>().enabled = false;

        }

    }
}
