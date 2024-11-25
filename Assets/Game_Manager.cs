using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Blinds();
    void PreFlop();
    void Flop();
    void Turn();
    void River();
    void Showdown();
}
public class Game_Manager
{
    
}
