using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

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
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Ladder");
                Destroy(other.gameObject);
            break;
            case "JumpingPuzzleTrigger":
                Debug.Log(other.gameObject.name);
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "JumpingPuzzle");
                break;
            case "FallingJumpingPuzzleTrigger":
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "FallingJumpingPuzzle");
                break;
            case "BridgeTrigger":
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Bridge");
                break;
            case "EnterOfficeTrigger":
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "EnterOffice");
                break;
            case "ExitOfficeTrigger":
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "ExitOffice");
                break;
            case "EnterCafeteriaTrigger":
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "EnterCafeteria");
                break;
            case "ExitCafeteriaTrigger":
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "ExitCafeteria");
                break;
            case "EnterPuzzleRoomTrigger":
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "EnterPuzzleRoom");
                break;
            case "ExitPuzzleRoomTrigger":
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "ExitPuzzleRoom");
                break;
            case "EnterPlatformRoomTrigger":
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "EnterPlatformRoom");
                break;
            case "ExitPlatformRoomTrigger":
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "ExitPlatformRoom");
                break;
            case "EnterComsTrigger":
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "EnterComs");
                break;
            case "ExitComsTigger":
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "ExitComs");
                break;
            case "EnterLockerTrigger":
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "EnterLocker");
                break;
            case "SecretRoomTrigger":
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "SecretRoom");
                break;
            case "EnterEndZoneTrigger":
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "EnterEndZone");
                break;

        }
    }
}
