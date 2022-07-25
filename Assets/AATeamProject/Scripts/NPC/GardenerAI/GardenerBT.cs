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
        //BTSequence chasing = new BTSequence();
        DetectGooseSound detectSound = new DetectGooseSound(gameObject);
        DetectGoosePos detectGoose = new DetectGoosePos(gameObject);
        Chasing chasingGoose = new Chasing(gameObject);


        //**************************gardener watering
        BTSequence watering = new BTSequence();
        GoPos waterWp = new GoPos(gameObject, "WateringPos1");
        GrabItem waterWp2 = new GrabItem(gameObject);
        WateringPoint3 waterWp3 = new WateringPoint3(gameObject);
        WateringPlants waterWp4 = new WateringPlants(gameObject);
        GoPos waterWp5 = new GoPos(gameObject, "WateringPos1");
        DropItem waterWp6 = new DropItem(gameObject);

        watering.AddChild(waterWp);
        watering.AddChild(waterWp2);
        watering.AddChild(waterWp3);
        watering.AddChild(waterWp4);
        watering.AddChild(waterWp5);
        watering.AddChild(waterWp6);

        //**************************gardener gardening

        BTSequence gardening = new BTSequence();
        GoPos gardenWp = new GoPos(gameObject, "GardeningPos2");
        GrabItem gardenWp2 = new GrabItem(gameObject);
        GoWork gardenWp3 = new GoWork(gameObject, "GardeningPos1");
        GardeningPlants gardenWp4 = new GardeningPlants(gameObject);
        GoPos gardenWp5 = new GoPos(gameObject, "GardeningPos2");
        gardening.AddChild(gardenWp);
        gardening.AddChild(gardenWp2);
        gardening.AddChild(gardenWp3);
        gardening.AddChild(gardenWp4);
        gardening.AddChild(gardenWp5);


        //**************************gardener vase


        //**************************gardener hammering
        BTSequence hammeringSign = new BTSequence();
        GoPos hammerWp = new GoPos(gameObject, "HammeringPos2");
        GrabItem hammerWp2 = new GrabItem(gameObject);
        GoWork hammerWp3 = new GoWork(gameObject, "HammeringPos1");
        Hammering hammerWp4 = new Hammering(gameObject);
        GoPos hammerWp5 = new GoPos(gameObject, "HammeringPos2");

        hammeringSign.AddChild(hammerWp);
        hammeringSign.AddChild(hammerWp2);
        hammeringSign.AddChild(hammerWp3);
        hammeringSign.AddChild(hammerWp4);
        hammeringSign.AddChild(hammerWp5);


        //**************************When gardener Wets
        BTSequence wetState = new BTSequence();
        Wet wetGardener = new Wet(gameObject);
        wetState.AddChild(wetGardener);



        //**************************Work Selector
        workSelector.AddChild(watering);
        workSelector.AddChild(gardening);
        workSelector.AddChild(hammeringSign);





        //**************************Main Selector
        //btMainSelector.AddChild(idle);
        Idle idle = new Idle(gameObject);
        btMainSelector.AddChild(detectSound);
        btMainSelector.AddChild(wetState);
        btMainSelector.AddChild(workSelector);

        aiState.AddChild(btMainSelector);


    }
}
