using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //variavel estatica para o GameManger
    public static GameManager ins;
    public GameObject ButtonPiano;

   

    public CameraRig rig;

    void Awake()
    {
        ins = this;
    }
    void Start()
    {
        ButtonPiano.SetActive(false);
    }

    void Update()
    {
       
    }
    public void SceneOne()
    {
        SceneManager.LoadScene("Scene1");
    }

   
}
