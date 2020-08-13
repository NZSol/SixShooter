using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreInteraction : MonoBehaviour
{
    public GameObject interact;
    [SerializeField] KeyCode Interact;
    public GameObject getaway;
    public GameObject newspaper;
    public GameObject injury;
    public GameObject diary;
    bool GetawayActive;
    bool newspaperActive;
    bool InjuryActive;
    bool DiaryActive;
    bool readingNote;


    // Start is called before the first frame update
    void Start()
    {
        GetawayActive = false;
        newspaperActive = false;
        InjuryActive = false;
        DiaryActive = false;
        getaway.SetActive(false);
        newspaper.SetActive(false);
        injury.SetActive(false);
        diary.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        closeGetAway();
        newsPaperActive();
        closediary();
        closeInjury();
    }
    public void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(Interact) && (other.CompareTag("getaway") || other.CompareTag("newspaper") || other.CompareTag("injury") || other.CompareTag("diary")))
        {
            FindObjectOfType<AudioManager>().Play("Paper_Pickup");
        }
        if (Input.GetKey(Interact) && other.CompareTag("getaway"))
        {
            GetawayActive = true;
        }
        else
        {
            GetawayActive = false;
        }
        if (Input.GetKey(Interact) && other.CompareTag("newspaper"))
        {
            newspaperActive = true;
        }
        else
        {
            newspaperActive = false;
        }
        if (Input.GetKey(Interact) && other.CompareTag("injury"))
        {
            InjuryActive = true;
        }
        else
        {
            InjuryActive = false;
        }
        if (Input.GetKey(Interact) && other.CompareTag("diary"))
        {
            DiaryActive = true;

        }
        else
        {
            DiaryActive = false;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("diary") || other.CompareTag("injury") || other.CompareTag("newspaper") || other.CompareTag("getaway"))
        {
            GetawayActive = false;
            newspaperActive = false;
            InjuryActive = false;
            DiaryActive = false;
        }
    }
    public void closeGetAway()
    {
        if(GetawayActive == true)
        {
            getaway.SetActive(true);
        }
        if(GetawayActive == false)
        {
            getaway.SetActive(false);
        }
    }
    public void newsPaperActive()
    {
        if (newspaperActive == true)
        {
            newspaper.SetActive(true);
        }
        if (newspaperActive == false)
        {
            newspaper.SetActive(false);
        }
    }
    public void closeInjury()
    {
        if (InjuryActive == true)
        {
            injury.SetActive(true);
        }
        if (InjuryActive == false)
        {
            injury.SetActive(false);
        }
    }
    public void closediary()
    {
        if (DiaryActive == true)
        {
            diary.SetActive(true);
        }
        if (DiaryActive == false)
        {
            diary.SetActive(false);
        }
    }
}
