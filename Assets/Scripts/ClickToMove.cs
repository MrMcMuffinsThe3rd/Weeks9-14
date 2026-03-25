using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    public LineRenderer lr;
    //public Vector2 mousePosNewInputSystem;

    //we cannot remove any of the elements in the default line list
    //so we have to create our own
    public List<Vector2> points; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        points = new List<Vector2>();
        points.Add(transform.position); 

     UpdateLineRenderer();

        //makes it so that the line doesnt start at the bottom corner of the screen like usual
        //lr.positionCount = 1;
        //lr.SetPosition(lr.positionCount - 1, transform.position); //so we dont have to click twice for the line to be drawn
    }

    // Update is called once per frame
    void Update()
    {
        //we are trying to allow the player to draw lines within the game
        //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());


        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //add a new point into the line
            //lr.positionCount++;
            //lr.SetPosition(lr.positionCount - 1, mousePos);

            //for our own list
            //add new point into the line
            Vector2 newPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            points.Add(newPos);
            UpdateLineRenderer();

        }

        if(Mouse.current.rightButton.wasPressedThisFrame)
        {
            //remove a point from our line list
            points.RemoveAt(0);
            UpdateLineRenderer();
        }

    }

    void UpdateLineRenderer()
    {
        lr.positionCount = points.Count;
        for (int i = 0; i < points.Count; i++)
        {
            lr.SetPosition(lr.positionCount - 1, transform.position);
        }
    }

    //public void OnPoint(InputAction.CallbackContext context)
    //{
    //    mousePosNewInputSystem = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    //}
}
