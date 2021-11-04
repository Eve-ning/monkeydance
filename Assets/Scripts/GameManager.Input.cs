using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public partial class GameManager
{
    Arrow.EDirection CurrentKeyInput()
    {
        if (!Input.anyKeyDown) return Arrow.EDirection.None;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) return Arrow.EDirection.Left;
        if (Input.GetKeyDown(KeyCode.RightArrow)) return Arrow.EDirection.Right;
        if (Input.GetKeyDown(KeyCode.UpArrow)) return Arrow.EDirection.Up;
        if (Input.GetKeyDown(KeyCode.DownArrow)) return Arrow.EDirection.Down;
        return Arrow.EDirection.None;
    }

    void CheckKeyInput(Arrow.EDirection keyInput)
    {
        StartCoroutine(keyInput == CurrentArrowDirection() ? SetArrowGood() : SetArrowBad());
        arrowIx++;
    }
}
