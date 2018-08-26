using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhasesManager : Singleton<PhasesManager> {

    bool _initialized;

    List<bool> _phases;
    List<string> _phaseTexts;
    List<int> _earlyGamePhases;
    List<int> _midGamePhases;
    List<int> _lateGamePhases;

    bool _zoomedOut;
	bool _no;
    bool _fast;
    bool _buttonSwap;
    bool _variatingSizes;
    bool _shapes;
	bool _ghost;

    const float _GAMEPHASE_DURATION = 240f;
    int _gamePhase = 0;

    const float _FIRST_PHASE_DELAY = 20f;
    const float _DEFAULT_PHASE_DURATION = 10f;
    float _phaseDuration;
    const float _DEFAULT_NOPHASE_DURATION = 15f;
    float _noPhaseDuration;

    public delegate void PhasesManagerEvent();
    public PhasesManagerEvent OnPhaseChange;

    void _Init()
    {
        _phases = new List<bool>();
        _phaseTexts = new List<string>();
        _earlyGamePhases = new List<int>();
        _midGamePhases = new List<int>();
        _lateGamePhases = new List<int>();

        // IMPORTANT: DO NOT CHANGE THE ORDER OF THESE BOOLEANS IN THE LIST WITHOUT CHANGING THEM IN BALLGAME TOO
        _phases.AddRange(new bool[] {_no, _fast, _buttonSwap, _variatingSizes, _shapes, _ghost});
        // Texts will have " PHASE STARTED" after these strings
        _phaseTexts.AddRange(new string[] {"NO", "FAST", "BUTTON SHUFFLE", "WEIRD SIZES", "WEIRD SHAPES", "GHOST"});
        _earlyGamePhases.AddRange(new int[] { 2, 3 });
        _midGamePhases.AddRange(new int[] { 5, 4 });
        _lateGamePhases.AddRange(new int[] { 1 });

        _phaseDuration = _DEFAULT_PHASE_DURATION;
        _noPhaseDuration = _DEFAULT_NOPHASE_DURATION;
    }

    public void StartPhaseCirculation()
    {
        if (!_initialized)
        {
            _Init();
        }

        //0 is earlygame, 1 is midgame, 2 is lategame
        _gamePhase = 0;

        Invoke("_StartNextPhase", _FIRST_PHASE_DELAY);
        Invoke("_IncreaseGamePhase", _GAMEPHASE_DURATION);
    }

    void _IncreaseGamePhase()
    {
        _gamePhase++;
        if(_gamePhase != 2)
        {
            Invoke("_IncreaseGamePhase", _GAMEPHASE_DURATION);
        }      
    }

    void _StartNextPhase()
    {
        for(int i = 0; i < _phases.Count; i++)
        {
            _phases[i] = false;
        }

        int chosenPhase = -1;
        switch (_gamePhase)
        {
            case 0:
                chosenPhase = _earlyGamePhases[Random.Range(0, _earlyGamePhases.Count)];
                break;
            case 1:
                chosenPhase = _midGamePhases[Random.Range(0, _midGamePhases.Count)];
                break;
            case 2:
                chosenPhase = _lateGamePhases[0];
                break;
        }
        _phases[chosenPhase] = true;

        if (OnPhaseChange != null)
        {
            OnPhaseChange();
        }

        Invoke("_StopCurrentPhase", _phaseDuration);        
    }

    void _StopCurrentPhase()
    {
        for (int i = 0; i < _phases.Count; i++)
        {
			_phases[i] = false;
        }
        if (OnPhaseChange != null)
        {
            OnPhaseChange();
        }
        Invoke("_StartNextPhase", _noPhaseDuration);
    }

    public void StopPhasesCirculation()
    {
        CancelInvoke();
        for (int i = 0; i < _phases.Count; i++)
        {
            _phases[i] = false;
        }
        if (OnPhaseChange != null)
        {
            OnPhaseChange();
        }
    }

    public List<bool> phases
    {
        get
        {
            return _phases;
        }
    }

    public List<string> phaseTexts
    {
        get
        {
            return _phaseTexts;
        }
    }
}
