using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject painelSettings;
    void Start()
    {
        painelSettings.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openSettings()
    {
        painelSettings.SetActive(true);
    }

    public void closeSettings()
    {
        painelSettings.SetActive(false);
    }
}
