using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsController : MonoBehaviour
{
    /*
LadderTrigger
JumpingPuzzleTrigger
FallingJumpingPuzzleTrigger
BridgeTrigger
EnterOfficeTrigger
ExitOfficeTrigger
EnterOfficeTrigger
ExitOfficeTrigger
EnterCafeteriaTrigger
ExitCafeteriaTrigger
EnterPuzzleRoomTrigger
ExitPuzzleRoomTrigger

EnterPlatformRoomTrigger
ExitPlatformRoomTrigger
EnterComsTrigger
ExitComsTigger
EnterLockerTrigger

SecretRoomTrigger
EnterEndZoneTrigger

     */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "LadderTrigger":
                Debug.Log(other.gameObject.name);
            break;
            case "JumpingPuzzleTrigger":
                Debug.Log(other.gameObject.name);
                break;
            case "FallingJumpingPuzzleTrigger":

                break;
            case "BridgeTrigger":

                break;
            case "EnterOfficeTrigger":

                break;
            case "ExitOfficeTrigger":

                break;
            case "EnterCafeteriaTrigger":

                break;
            case "ExitCafeteriaTrigger":

                break;
            case "EnterPuzzleRoomTrigger":

                break;
            case "ExitPuzzleRoomTrigger":

                break;
            case "EnterPlatformRoomTrigger":

                break;
            case "ExitPlatformRoomTrigger":

                break;
            case "EnterComsTrigger":

                break;
            case "ExitComsTigger":

                break;
            case "EnterLockerTrigger":

                break;
            case "SecretRoomTrigger":

                break;
            case "EnterEndZoneTrigger":

                break;

        }
    }
}
