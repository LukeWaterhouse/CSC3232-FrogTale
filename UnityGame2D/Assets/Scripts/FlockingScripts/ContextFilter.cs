using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContextFilter : ScriptableObject
{
    //Disclaimer: This is adpated from a youtube series on flocking

    public abstract List<Transform> Filter(FlockAgent agent, List<Transform> original);
}