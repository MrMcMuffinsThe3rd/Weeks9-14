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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position.y = -2.78f;
        transform.position = position;
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
        position.x = movement.x;
        transform.position = position;

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
