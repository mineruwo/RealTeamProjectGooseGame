using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BTSelector : BTComposite
    {
        public BTSelector()
        {
            SetNodeTypes(NodeTypes.Selector);
        }

        public override NodeState Update()
        {
            for(int i =0; i<GetChildCount(); ++i)
            {
                NodeState currentState = GetChild(i).Tick();
                if(currentState != NodeState.FAILURE)
                {
                    ClearChild(i);
                    return currentState;
                }
            }
            return NodeState.FAILURE;
        }

        protected void ClearChild(int iSkipIndex)
        {
            for (int i = 0; i < GetChildCount(); ++i)
            {
                if(i != iSkipIndex)
                {
                    GetChild(i).Reset();
                }
            }
        }
    }
}
