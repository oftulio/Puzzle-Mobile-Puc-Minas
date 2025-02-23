using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public abstract class Node : MonoBehaviour
{

    public Transform cameraPosition;
    public List<Node> reachableNodes = new List<Node>();

    [HideInInspector]
    public Collider col;

    void Start()
    {
        //faz a variavel receber o componente do tipo collider
        col = GetComponent<Collider>();
    }
    void OnMouseDown()
    {
        //chama o metodo criado quando clica no mouse
        Arrive();
    }
   public void Arrive()
    {
        if (GameManager.ins.currentNode != null)
            GameManager.ins.currentNode.Leave();

        //defini o node atual
        GameManager.ins.currentNode = this;

        GameManager.ins.rig.AlignTo(cameraPosition);

        //move a camera
        Sequence seq = DOTween.Sequence();
        seq.Append(Camera.main.transform.DOMove(cameraPosition.position, 0.75f));
        seq.Join(Camera.main.transform.DORotate(cameraPosition.rotation.eulerAngles, 0.75f));



        //desabilita o colisor
        if (col != null) 
        {
            col.enabled = false;
        }

        //liga os colliders dos objetos
        foreach (Node node in reachableNodes)
        {
            if (node != null)
                node.col.enabled = true;
        }
    }
    //desliga os colliders dos objetos
    public void Leave()
    {
        foreach (Node node in reachableNodes)
        {
            if (node != null)
                node.col.enabled = false;
        }
    }
}
