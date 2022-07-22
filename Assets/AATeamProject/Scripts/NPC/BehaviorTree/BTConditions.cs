using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    //LeftNode
    public class BTConditions : BTBehavior
    {
        public BTConditions()
        {
            SetNodeTypes(NodeTypes.Condition);
        }

        public override NodeState Tick()
        {
            SetState(Update());
            if(GetState() == NodeState.RUNNING)
            {
                Debug.Log("!");
            }

            if(GetState()==NodeState.SUCCESS)
            {
                TerminateRunningStateByOtherAction();
            }
            return GetState();
        }

        public void TerminateRunningStateByOtherAction()
        {
            BTBehavior btFindRoot = null;
            int errorCount = 0;

            btFindRoot = GetParent();
            if(btFindRoot != null)
            {
                while(errorCount<100)
                {
                    btFindRoot = btFindRoot.GetParent();
                    if(btFindRoot.GetParent() == null) 
                        break;
                    ++errorCount;
                }
            }
            if(btFindRoot != null)
            {
                if(btFindRoot.GetState()==NodeState.RUNNING)
                {
                    BTBehavior btRunningAction = FindRunningAction(((BTRoot)btFindRoot).GetChild());
                    if(btRunningAction != null)
                    {
                        if (GetParent() != btRunningAction.GetParent() || GetParent().GetNodeTypes() != NodeTypes.Sequence)
                            btRunningAction.Terminate();
                    }
                }
            }
        }

        public BTBehavior FindRunningAction(BTBehavior btChild)
        {
            BTBehavior btRunningAcition = null;
            if(btChild != null)
            {
                if(btChild.GetNodeTypes()==NodeTypes.Selector || btChild.GetNodeTypes()==NodeTypes.Sequence)
                {
                    for(int i = 0; i<((BTComposite)btChild).GetChildCount(); ++i)
                    {
                        btRunningAcition = FindRunningAction(((BTComposite)btChild).GetChild(i));
                        if(btRunningAcition!=null)
                            return btRunningAcition;
                    }
                }
                if(btChild.GetNodeTypes()==NodeTypes.Action&&btChild.GetState()==NodeState.RUNNING)
                    return btChild;
            }
            return btRunningAcition;
        }
    }

}
