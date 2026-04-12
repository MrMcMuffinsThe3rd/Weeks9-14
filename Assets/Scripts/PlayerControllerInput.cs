using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerInput : MonoBehaviour
{
    public float speed = 5;
    public Vector2 movement;
    public Vector2 position;

    public bool gunShot = false;
    public AudioSource gunShotSFX;

    public Animator playerAnimation;

    public Transform player;
    Vector2 playerPos;
    public Transform cursor;
    Vector2 cursorPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPos = player.transform.position;

        playerPos.y = -2.78f;
        transform.position = position;

        cursorPos = cursor.position;  
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimation.Play("idle");
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        //movement.x = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x;
        //position.x = movement.x;
        //transform.position = position;

        //to move the gun with the mouse
        movement.x = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>()).x;
        playerPos.x = movement.x;
        transform.position = playerPos;

        movement = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            Debug.Log("POWWW");
            gunShotSFX.Play();
            playerAnimation.Play("shootingGun");
        }
    }
}
