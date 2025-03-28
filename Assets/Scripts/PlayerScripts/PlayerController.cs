using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static PlayerController instance;
    public CharacterController controller;
    public float gravidade = 19.81f;


    private Vector3 velocidadeVertical;

    private void Awake()
    {
           instance = this;  
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded && velocidadeVertical.y < 0)
        {
            velocidadeVertical.y = -2f; 
        }
        velocidadeVertical.y -= gravidade * Time.deltaTime;
        controller.Move(velocidadeVertical * Time.deltaTime);

    }
}
