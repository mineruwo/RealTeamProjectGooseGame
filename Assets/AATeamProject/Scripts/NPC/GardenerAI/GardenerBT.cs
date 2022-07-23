using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.Animations.Rigging;


public class GardenerBT : MonoBehaviour
{
    private RigBuilder rigBuilder;
    private BTRoot aiState;

    private void Start()
    {
        CreateBehaviorTreeAiState();
        rigBuilder = GetComponent<RigBuilder>();
    }
    private void Update()
    {
        aiState.Tick();
    }

    private void CreateBehaviorTreeAiState()
    {
        aiState = new BTRoot();
        BTSelector btMainSelector = new BTSelector();

        //**************************gardener work1
        BTSequence watering = new BTSequence();
        WaterCanPoint waterWp = new WaterCanPoint(gameObject);
        GrabItem waterWp2 = new GrabItem(gameObject);
        WateringPoint3 waterWp3 = new WateringPoint3(gameObject);
        WateringPlants waterWp4 = new WateringPlants(gameObject);
        WaterCanPoint waterWp5 = new WaterCanPoint(gameObject);

        watering.AddChild(waterWp);
        watering.AddChild(waterWp2);
        watering.AddChild(waterWp3);
        watering.AddChild(waterWp4);
        watering.AddChild(waterWp5);

        //**************************gardener work2
        BTSequence gardening = new BTSequence();
        FindItem aiFindItem = new FindItem(gameObject);
        gardening.AddChild(aiFindItem);


        //**************************gardener work3



        //**************************gardener hammering
        BTSequence hammeringSign = new BTSequence();
        GrabItem getHammer = new GrabItem(gameObject);
        Hammering hammering = new Hammering(gameObject);
        hammeringSign.AddChild(getHammer);
        hammeringSign.AddChild(hammering);


        //**************************Main Selector

        btMainSelector.AddChild(watering);
        btMainSelector.AddChild(hammeringSign);

        btMainSelector.AddChild(gardening);

        aiState.AddChild(btMainSelector);


    }
}
