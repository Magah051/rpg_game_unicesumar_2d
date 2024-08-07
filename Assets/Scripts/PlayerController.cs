using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocidade = 5f;
    private Rigidbody2D rb;
    public Animator playerAnimator;
    bool isWalking = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isWalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxisRaw("Horizontal") * velocidade;
        float eixoY = Input.GetAxisRaw("Vertical") * velocidade;
        isWalking = eixoX != 0 || eixoY != 0;

        rb.velocity = new Vector2(eixoX,eixoY);

        if (isWalking)
        {
            playerAnimator.SetFloat("eixoX", eixoX);
            playerAnimator.SetFloat("eixoY", eixoY);
        }
        playerAnimator.SetBool("isWalking", isWalking);
    }
}
