using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseStateManager : MonoBehaviour {

    PhaseState normaiState;
    PhaseState fastState;
    PhaseState buttonSwapState;
    PhaseState variatingSizesState;
    PhaseState differentShapesState;
    PhaseState ghostState;

    PhaseState currentState;

    // Use this for initialization
    void Start () {
        normaiState = new NormalPhase();
        fastState = new FastPhase();
        buttonSwapState = new ButtonSwapPhase();
        variatingSizesState = new VariatingSizesPhase();
        differentShapesState = new DifferentShapesPhase();
        ghostState = new GhostPhase();

        currentState = normaiState;
    }

   public void SetPhaseState(PhaseState newState)
    {
        currentState = newState;
    }

    public void ChangeToButtonSwapPhase()
    {
        currentState.buttonSwapPhase();
    }

    public void changeToVariatingSizesPhase()
    {
        currentState.variatingSizesPhase();
    }

    public void changeToDifferentShapesPhase()
    {
        currentState.differentShapesPhase();
    }

    public void ChangeToGhostPhase()
    {
        currentState.ghostPhase();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
