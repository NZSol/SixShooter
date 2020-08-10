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
SecretRoomTrigger
EnterPlatformRoomTrigger
ExitPlatformRoomTrigger
EnterComsTrigger
ExitComsTigger
EnterLockerTrigger
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

            break;
            case "Ladder":

                break;
            case "Ladder":

                break;
            case "Ladder":

                break;
            case "Ladder":

                break;

        }
    }
}
