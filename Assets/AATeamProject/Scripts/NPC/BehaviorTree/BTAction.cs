using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BTAction : BTBehavior
    {
        public BTAction()
        {
            SetNodeTypes(NodeTypes.Action);
        }

        public override void Initialize() { }
        public override void Terminate() { }
        public override void Reset()
        {
            SetState(NodeState.INVALID);
        }

        public override NodeState Tick()
        {
            if(GetState() == NodeState.INVALID)
            {
                Initialize();
                SetState(NodeState.RUNNING);
            }

            SetState(Update());
            if(GetState() != NodeState.RUNNING)
                Terminate();

            return GetState();
        }
    }
}
