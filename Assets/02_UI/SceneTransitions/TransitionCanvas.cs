using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCanvas : MonoBehaviour
{
    public void PauseGame_AnimationEvent()
    {
        FindObjectOfType<PauseAndTutorial>().GamePause();
    }
}
