using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerInput : MonoBehaviour
{
    public float speed = 5;
    public Vector2 movement;
    public Vector2 position;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position.y = -2.78f;
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMove(InputAction.CallbackContext context) //"On" is a keyword for player events
    {
        movement = context.ReadValue<Vector2>(); //we want it to read a vector2 so thats why we wrote it like that (it'll give an error otherwise)
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
}
