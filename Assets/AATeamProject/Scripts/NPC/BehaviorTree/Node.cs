using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    public enum NodeState
    {
        INVALID,
        RUNNING,
        SUCCESS,
        FAILURE,
        ABORTED,
    }

    public enum NodeTypes
    {
        Root,
        Selector,
        Sequence,
        Paraller,
        Decorator,
        Condition,
        Action,
    }

    public class BTBehavior
    {
        private NodeState mystate;
        private NodeTypes myNodeType;
        private int index;
        private BTBehavior btParent;

        public BTBehavior()
        {
            mystate = NodeState.INVALID;
        }

        public bool IsTerminated() { return mystate == NodeState.SUCCESS | mystate == NodeState.FAILURE; }
        public bool IsRunning() { return mystate == NodeState.RUNNING; }
        public void SetParent(BTBehavior btNewParent) { btParent = btNewParent; }
        public BTBehavior GetParent() { return btParent; }
        public NodeState GetState() { return mystate; }
        public void SetState(NodeState newState) { mystate = newState; }
        public NodeTypes GetNodeTypes() { return myNodeType; }
        public void SetNodeTypes(NodeTypes newNodeType) { myNodeType = newNodeType; }
        public int GetIndex() { return index; }
        public void SetIndex(int newIndex) { index = newIndex; }
        virtual public void Reset() { mystate = NodeState.INVALID; }

        public virtual void Initialize() { }
        public virtual NodeState Update()
        {
            return NodeState.SUCCESS;
        }

        public virtual void Terminate() { }
        public virtual NodeState Tick()
        {
            if(mystate == NodeState.INVALID)
            {
                Initialize();
                mystate = NodeState.RUNNING;
            }

            mystate = Update();
            if(mystate != NodeState.RUNNING)
            {
                Terminate();
            }
            return mystate;
        }
    }   
}

