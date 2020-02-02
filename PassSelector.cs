using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassSelector : MonoBehaviour,IShotable
{
    public DanceMoves moves;
    
    public void interact(Vector3 hitPos)
    {
       DanceEvent d_event = GetComponentInParent<DanceEvent>();
       d_event.SendDanceMovement(moves); 
    }
}
public enum DanceMoves
{
    step=0,
    stayAlive,
    ballerin,
}