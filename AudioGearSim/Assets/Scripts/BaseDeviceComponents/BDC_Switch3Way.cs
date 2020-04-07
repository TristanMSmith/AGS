using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BDC_Switch3Way : BDC_Switch
{
    public State state { get; private set; }

    public enum State { FirstPosition, SecondPosition, ThirdPosition }
}
