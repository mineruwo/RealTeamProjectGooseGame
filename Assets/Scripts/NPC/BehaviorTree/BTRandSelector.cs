using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BTRandSelector : BTComposite
    {
        protected int current;
        public BTRandSelector()
        {
            SetNodeTypes(NodeTypes.RandSelector);
        }

        public override NodeState Update()
        {
            current = Random.Range(0, GetChildCount());
            NodeState currentState = GetChild(current).Tick();
            if (currentState != NodeState.FAILURE)
            {
                ClearChild(current);
                return currentState;
            }
            return NodeState.FAILURE;
        }

        protected void ClearChild(int iSkipIndex)
        {
            if (current != iSkipIndex)
            {
                GetChild(current).Reset();
            }

        }
    }
}
