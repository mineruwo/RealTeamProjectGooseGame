using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class GardenerBT : MonoBehaviour
{
    private BTRoot aiState;

    private void Start()
    {
        CreateBehaviorTreeAiState();
    }
    private void Update()
    {
        aiState.Tick();
    }

    private void CreateBehaviorTreeAiState()
    {
        aiState = new BTRoot();
        BTSelector btMainSelector = new BTSelector();

        //move
        BTSequence btMove = new BTSequence();
        Move aiMoveAction = new Move(gameObject);
        btMove.AddChild(aiMoveAction);

        BTSequence btFind = new BTSequence();
        FindItem aiFindItem = new FindItem(gameObject);
        btFind.AddChild(aiFindItem);

        btMainSelector.AddChild(btMove);
        btMainSelector.AddChild(btFind);

        aiState.AddChild(btMainSelector);


    }
}
