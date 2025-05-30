using UnityEngine;

public class PlayerFaceManager : MonoBehaviour
{
    public static PlayerFaceManager Instance;

    public string currentFace; // Exemplo: "Jardineiro", "Guarda", etc.

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
