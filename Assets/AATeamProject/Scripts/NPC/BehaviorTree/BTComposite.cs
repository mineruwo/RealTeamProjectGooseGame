using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BTComposite : BTBehavior
    {
        protected List<BTBehavior> listofChild;
        public BTComposite()
        {
            listofChild = new List<BTBehavior>();
        }
        public override void Reset()
        {
            for(int i = 0; i<GetChildCount(); ++i)
            {
                GetChild(i).Reset();
            }
        }

        public BTBehavior GetChild(int index)
        {
            return listofChild[index];
        }
        public int GetChildCount()
        {
            return listofChild.Count;
        }
        public void AddChild(BTBehavior newChild)
        {
            listofChild.Add(newChild);
            newChild.SetIndex(listofChild.Count-1);
            newChild.SetParent(this);
        }
    }
}

