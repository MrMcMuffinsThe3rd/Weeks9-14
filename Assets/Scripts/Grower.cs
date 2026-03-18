using System.Collections;
using UnityEngine;

public class Grower : MonoBehaviour
{
    public Transform treeTransform;
    public Transform appleTransform;
    public float appleDelay = 1f;

    Coroutine theTreeCoroutine; //creates a coroutine variable
    //(a variable that takes a coroutine)

    Coroutine theAppleCoroutine;
    Coroutine theGrowingCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //makes sure the scale of the tree is set to zero at the begining of the game
        treeTransform.localScale = Vector2.zero;
        appleTransform.localScale = Vector2.zero;

        //these two lines belong to Waiting for Coroutine
        //StartCoroutine(GrowTree());
        //StartCoroutine(GrowApple()); //removing this so it only runs GrowApple coroutine after GrowTree (see code below)

        //StartCoroutine(GrowTree()); //this is how you call a function with an IEnumerator return type
        //you can no longer just say "GrowTree();" to call the function anymore
        // if you just said "GrowTree();" it'll just call it like a regular function before we added IEnumerator

        StartTreeGrowing(); //to use the IEnumerator StartTheGrowing
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

        //StartCoroutine(GrowTree());

        //these two lines belong to Waiting for Coroutine
        //StartCoroutine(GrowTree());
        //StartCoroutine(GrowApple()); //instead of calling this line here (bec it makes it so the apple grows with the tree)
        //(we dont want that we want it so that the tree grows then the apple)    


        // //checks if the coroutine variable is not null (as in, it's not been assigned a coroutine yet)
        // //if yes: stop the coroutine that is running in the coroutine variable
        // if(theTreeCoroutine != null)
        // {
        //     StopCoroutine(theTreeCoroutine);
        // }

        // //if no: set the coroutine variable to a coroutine
        //theTreeCoroutine = StartCoroutine(StartTheGrowing());
        // //the previous block of code only stops the tree coroutine from running but not the apple's
        // //we want them both to stop so lets see how to do that:

        //we're gonna use the same if statement just different variables
        //this "theGrowingCoroutine" variable starts the "StartTheGrowing" coroutine
        if (theGrowingCoroutine != null)
        {
            //if theGrowingCoroutine variable is assigned a value
            //stop that coroutine
            StopCoroutine(theGrowingCoroutine);
        }

        //but before reassigning theGrowingCoroutine variable
        //check if theTreeCoroutine variable is assigned a value
        if (theTreeCoroutine != null)
        {
            //if it is assigned a value
            //stop that coroutine
            StopCoroutine(theTreeCoroutine);
        }

        //before reassigning theGrowingCoroutine variable we still need to check for the apple coroutine
        //check if theAppleCoroutine variable is assigned a value
        if(theAppleCoroutine != null)
        {
            //if it is assigned a value
            //stop that coroutine
            StopCoroutine(theAppleCoroutine);
        }

        //then finally, reassign theGrowingCoroutine variable
         theGrowingCoroutine = StartCoroutine(StartTheGrowing());

        //but we're not done yet, we need to reassign theTreeCoroutine and theAppleCoroutine variables
        //(go to StartTheGrowing coroutine)
    }

    //this is another way to make the tree grow before growung the apple
    IEnumerator StartTheGrowing()
    {
        //yield return StartCoroutine(GrowTree());
        //yield return StartCoroutine(GrowApple());

        //instead of the previous two lines, we want to reassign theTreeCoroutine and theAppleCoroutine variables to their respective coroutines
        //so we can do:
        yield return theTreeCoroutine = StartCoroutine(GrowTree());
        yield return theAppleCoroutine = StartCoroutine(GrowApple());

        //it's also easier to just do the previous two lines but add yield return new WaitForSeconds() instead of this mess
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

        //makes sure the tree and the apple scales are set to zero when this function is called again
        treeTransform.localScale = Vector2.zero;
        appleTransform.localScale = Vector2.zero; 

        while (t < 1)
        {
            t += Time.deltaTime;
            treeTransform.localScale = Vector2.one * t; //you can use an animation curve here, instead of multiplying by t, you multiply by curve

            yield return null; //this return statement is the equivalent to saying:
                               //"I don't want you to touch the loop. Hold on looping the next loop until the next frame runs and so on and so forth"
        }   //"null" just means that the while loop doesn't return a *value* (as in, float, bool, int, string, etc), it just makes it so the loop come back next frame
        //we can also say new WaitForSeconds() instead of null here to set a timer for the tree to be fully grown




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


        //yield return new WaitForSeconds(appleDelay); //this code means "come back after this amount of time has passed" then run the code after this
                                                    // basically saying skip all the frames that will run in this given amount of time


        //this code below is to make the apple grow
        //notice how we're setting t = 0 right after our first while loop and before our second
        //why is that? won't that mess up our IEnumerator for the first loop?
        //the answer to that is No, it will not
        //why? --> when there is an IEnumerator function and it has more than one while loops
        //it does not run the entire function at once (like if it was a void function)
        //it runs everything before a while loop (float t = 0), then goes to the while loop right after that code and runs it every frame
        //then once it's done with that loop, it leaves it and DOES NOT go back to the begining code before that loop. That part is done now until everything in the function has ran
        //it then runs the code before the next while loop (t = 0) then runs that while loop every frame till it's done with it and so on and so forth
       
        //t = 0; //NOTE: if we placed this line of code after the yield return statement of the first while loop, it'll just create a loop that never ends. We do not want that

        //while (t < 1)
        //{
        //    t += Time.deltaTime;
        //    appleTransform.localScale = Vector2.one * t;

        //    yield return null; 
        //}


        //StartCoroutine(GrowApple()); //we run this code at the end of the GrowTree coroutine instead of together with GrowTree at Start
                                    //this makes it so the GrowTree coroutine finishes running first then it starts the coroutine for GrowApple
                                    //which will give us the effect we want (grow tree then grow apple)

    }

    //here we are trying to do what we did in GrowTree where everything was happening in the same coroutine
    //we want to seperate them now but currently, they grow at the same time and the apple doesn't wait for the tree
    //like when they were both in the same coroutine
    IEnumerator GrowApple()
    {
        float t = 0;

        appleTransform.localScale = Vector2.zero;

        while (t < 1)
        {
            t += Time.deltaTime;
            appleTransform.localScale = Vector2.one * t;

            yield return null;
        }
    }
}
