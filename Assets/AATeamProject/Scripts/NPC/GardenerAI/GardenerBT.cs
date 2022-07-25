using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.Animations.Rigging;


public class GardenerBT : MonoBehaviour
{
    private RigBuilder rigBuilder;
    private bool isWet;
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
        BTSelector workSelector = new BTSelector();


        //**************************gardener wet
        ChaseGoose chaseGoose = new ChaseGoose(gameObject);

        //**************************gardener watering
        BTSequence watering = new BTSequence();
        WaterCanPoint waterWp = new WaterCanPoint(gameObject);
        GrabItem waterWp2 = new GrabItem(gameObject);
        WateringPoint3 waterWp3 = new WateringPoint3(gameObject);
        WateringPlants waterWp4 = new WateringPlants(gameObject);
        WaterCanPoint waterWp5 = new WaterCanPoint(gameObject);
        DropItem waterWp6 = new DropItem(gameObject);

        watering.AddChild(waterWp);
        watering.AddChild(waterWp2);
        watering.AddChild(waterWp3);
        watering.AddChild(waterWp4);
        watering.AddChild(waterWp5);
        watering.AddChild(waterWp6);

        //**************************gardener gardening

        BTSequence gardening = new BTSequence();
        GoShovelPos gardenWp = new GoShovelPos(gameObject);
        GrabItem gardenWp2 = new GrabItem(gameObject);
        GoGardening gardenWp3 = new GoGardening(gameObject);
        GardeningPlants gardenWp4 = new GardeningPlants(gameObject);
        GoShovelPos gardenWp5 = new GoShovelPos(gameObject);
        gardening.AddChild(gardenWp);
        gardening.AddChild(gardenWp2);
        gardening.AddChild(gardenWp3);
        gardening.AddChild(gardenWp4);
        gardening.AddChild(gardenWp5);


        //**************************gardener vase


        //**************************gardener hammering
        BTSequence hammeringSign = new BTSequence();
        GrabItem getHammer = new GrabItem(gameObject);
        Hammering hammering = new Hammering(gameObject);
        hammeringSign.AddChild(getHammer);
        hammeringSign.AddChild(hammering);

        //**************************When gardener Wets
        BTSequence wetState = new BTSequence();
        Wet wetGardener = new Wet(gameObject);
        wetState.AddChild(wetGardener);



        //**************************Work Selector
        workSelector.AddChild(gardening);
        workSelector.AddChild(watering);
        workSelector.AddChild(hammeringSign);


        //**************************Main Selector
        //btMainSelector.AddChild(idle);
        Idle idle = new Idle(gameObject);
        btMainSelector.AddChild(chaseGoose);
        btMainSelector.AddChild(wetState);
        btMainSelector.AddChild(workSelector);

        aiState.AddChild(btMainSelector);


    }
}
