using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCondition
{
    //goose가 아이템을 물었을때
    //goose가 범위안에 들어왔을 때
    //goose가 뺏었을 때 <얘들은 조건이다
    public virtual bool Test()
    {
        return false;
    }
}

public class Transition
{
    public AiCondition condition;
    public NPCState target;
}
public class NPC : MonoBehaviour 
{
    List<Transition> transitions = new List<Transition>();
    private void Awake()
    {
        
    }
    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {
        
    }

    public virtual void Detect()
    {
    }
    public virtual void Idle()
    {
    }
    public virtual void Undetect()
    {
    }
    public virtual void Chase()
    {
    }
    public virtual void Move()
    {
    }
    public virtual void PickUp()
    {
    }
    public virtual void DropDown()
    {
    }
    public virtual void TouchGoose()
    {
    }
    public virtual void DoWork()
    {
    }
}
