using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BTSequence : BTComposite
    {
         public BTSequence()
        {
            SetNodeTypes(NodeTypes.Sequence);
        }

        public override NodeState Update()
        {
            NodeState currentState = NodeState.INVALID;
            for(int i = 0; i<GetChildCount(); i++)
            {
                currentState = GetChild(i).GetState();
                if(GetChild(i).GetNodeTypes() != NodeTypes.Action || GetChild(i).GetState()!=NodeState.SUCCESS)
                {
                    currentState = GetChild(i).Tick();
                }
                if(currentState != NodeState.SUCCESS)
                {
                    return currentState;
                }
            }
            return NodeState.SUCCESS;
        }
    }
}
