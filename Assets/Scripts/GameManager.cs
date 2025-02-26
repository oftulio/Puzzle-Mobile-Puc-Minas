using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //variavel estatica para o GameManger
    public static GameManager ins;

    //Variavel para recber o script atual
    [HideInInspector]
    public Node currentNode;

    public CameraRig rig;

    void Awake()
    {
        ins = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && currentNode.GetComponent<Prop>() != null)
        {
            currentNode.GetComponent<Prop>().loc.Arrive();
        }
    }
    public void SceneOne()
    {
        SceneManager.LoadScene("SceneOne");
    }

   
}
