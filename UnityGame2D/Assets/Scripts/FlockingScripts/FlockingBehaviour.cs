using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockingBehaviour : ScriptableObject
{
    //Disclaimer: This is adpated from a youtube series on flocking

    public abstract Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
}
