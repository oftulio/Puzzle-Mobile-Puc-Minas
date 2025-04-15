using UnityEngine;

public class PlayerColisionGeneral : MonoBehaviour
{
    public GameObject PanfletoPainel1;
    public GameObject PanfletoPainel2;
    public GameObject PanfletoPainel3;
    public GameObject InteragirButton;
    public GameObject fecharPanfleto;
    private MobileLook playerScript;
    public GameObject player; // Referência ao player
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = player.GetComponent<MobileLook>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Panfleto1"))
            InteragirButton.SetActive(true);
        
        if (other.CompareTag("Panfleto2"))
            InteragirButton.SetActive(true);
        
        if (other.CompareTag("Panfleto3"))
            InteragirButton.SetActive(true);
        

    }

    public void InteragirPanfleto1()
    {

        PanfletoPainel1.SetActive(true);
        playerScript.moveCamera = false;
        playerScript.movePlayer = false;
    }
    public void InteragirPanfleto2()
    {
        PanfletoPainel2.SetActive(true);
        playerScript.moveCamera = false;
        playerScript.movePlayer = false;
    }
    public void InteragirPanfleto3()
    {
        PanfletoPainel3.SetActive(true);
        playerScript.moveCamera = false;
        playerScript.movePlayer = false;
    }

    public void FecharPanfleto()
    {
        PanfletoPainel1.SetActive(false);
        PanfletoPainel2.SetActive(false);
        PanfletoPainel3.SetActive(false);
        playerScript.moveCamera = true;
        playerScript.movePlayer = true;
    }

}
