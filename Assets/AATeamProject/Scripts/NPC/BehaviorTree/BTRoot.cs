using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BTRoot : BTBehavior
    {
        private BTBehavior child;
        public BTRoot()
        {
            SetNodeTypes(NodeTypes.Root);
            SetParent(null);
        }
        public void AddChild(BTBehavior newChild)
        {
            child = newChild;
            child.SetParent(this);
        }

        public BTBehavior GetChild() { return child; }
        public override void Terminate()
        {
            child.Terminate();
            base.Terminate();
        }
        public override NodeState Tick()
        {
            if (child == null)
            {
                return NodeState.INVALID;
            }

            else if(child.GetState()==NodeState.INVALID)
            {
                child.Initialize();
                child.SetState(NodeState.RUNNING);
            }
            SetState(child.Update());
            child.SetState(GetState());

            if(GetState() == NodeState.RUNNING)
                Terminate();

            return GetState();
        }
    }
}
