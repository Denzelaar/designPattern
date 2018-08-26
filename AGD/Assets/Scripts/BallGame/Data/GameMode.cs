using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode
{
    private static int _currentGamemode;

    static public int currentGamemode
    {
        get
        {
            return _currentGamemode;
        }
        set
        {
            _currentGamemode = value;
        }
    }
}
