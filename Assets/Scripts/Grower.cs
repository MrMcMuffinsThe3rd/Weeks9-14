using System.Collections;
using UnityEngine;

public class Grower : MonoBehaviour
{
    public Transform treeTransform;
    public Transform appleTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //makes sure the scale of the tree is set to zero at the begining of the game
        treeTransform.localScale = Vector2.zero;

        //StartCoroutine(GrowTree()); //this is how you call a function with an IEnumerator return type
                                    //you can no longer just say "GrowTree();" to call the function anymore
                                    // if you just said "GrowTree();" it'll just call it like a regular function before we added IEnumerator
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTreeGrowing()
    {
        //because we have to say StartCoroutine(GrowTree()); instead of just GrowTree();, we no longer can call GrowTree() function from a button like usual
        //Which is why we created this seperate public function where we put the StartCoroutine(GrowTree()); command in it
        //that way, we can call this public function from the button like usual

        StartCoroutine(GrowTree());
    }

    IEnumerator GrowTree()
    {
        //this block of code only makes the tree appear instantly in a single frame
        //and not grow consistantly across several frames to give the illusion of an animation of a tree growing
        //this block of code only won't be sufficeint to achieve the animation that we want 
        //to achieve that, we have to change the return type of this function from "void" to "IEnumerator"
        //IEnumerator functions do not need to be public because we won't be able to call them from the button like usual anyway
        //NOTE: you have to be using System.Collections at the top to not get an error under IEnumerator
        float t = 0; //assign time variable

        while (t < 1)
        {
            t += Time.deltaTime;
            treeTransform.localScale = Vector2.one * t;

            yield return null; //this return statement is the equivalent to saying:
                               //"I don't want you to touch the loop. Hold on looping the next loop until the next frame runs and so on and so forth"
        }   //"null" just means that the while loop doesn't return a *value* (as in, float, bool, int, string, etc), it just makes it so the loop come back next frame




        //there's another way to code the previous using an if statement 
        //t += Time.deltaTime; //increase its value

        //if (t > 1) //check if it's too big or bigger than we want it to be
        //{
        //    //if yes:
        //    //stop the timer
        //}

        ////if no:
        ////continue growing the tree
        //treeTransform.localScale = Vector2.one * t;


        //this code is to make the apple grow
        //notice how we're setting t = 0 right after our first while loop and before our second
        //why is that? won't that mess up our IEnumerator for the first loop?
        //the answer to that is No, it will not
        //why? --> when there is an IEnumerator function and it has more than one while loops
        //it does not run the entire function at once (like if it was a void function)
        //it runs everything before a while loop (float t = 0), then goes to the while loop right after that code and runs it every frame
        //then once it's done with that loop, it leaves it and DOES NOT go back to the begining code before that loop. That part is done now until everything in the function has ran
        //it then runs the code before the next while loop (t = 0) then runs that while loop every frame till it's done with it and so on and so forth
       
        t = 0; //NOTE: if we placed this line of code after the yield return statement of the first while loop, it'll just create a loop that never ends. We do not want that

        while (t < 1)
        {
            t += Time.deltaTime;
            appleTransform.localScale = Vector2.one * t;

            yield return null; 
        }
    }
}
