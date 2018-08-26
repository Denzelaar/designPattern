using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PhaseState {

    void normalPhase();
    void fastPhase();
    void buttonSwapPhase();
    void variatingSizesPhase();
    void differentShapesPhase();
    void ghostPhase();

}
