using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Locker : Interactable
{
    public Transform playerhideposition;

    public Outline LockerActual;
    public Outline LockerDoor;

    public GameObject RegisterArea;
    public Transform ExitArea;

    private GameObject Player;

    private float hideTimer;

    private bool HidingInside;

    private bool Activated;

    private void Start()
    {
        Activated = true;
        HidingInside = false;
        hideTimer = 0.0f;
    }

    private void Update()
    {
        if(HidingInside && Input.GetKeyDown(KeyCode.E) && hideTimer > 0)
        {
            Debug.Log("Unhiding");
            Player.GetComponent<CharacterController>().enabled = false;
            Player.transform.position = ExitArea.position;
            Player.GetComponent<CharacterController>().enabled = true;
            Player.GetComponent<charactercontroller>().ActiveState = charactercontroller.PlayerState.Active;
            HidingInside = false;
        }
        hideTimer += Time.deltaTime;
    }

    public void EnableGlow()
    {
        LockerActual.enabled = true;
        LockerDoor.enabled = true;
    }

    public void DisableGlow()
    {
        LockerActual.enabled = false;
        LockerDoor.enabled = false;
    }

    public override void InteractedWith(charactercontroller PlayerCharacter)
    {
        base.InteractedWith(PlayerCharacter);
        if (Activated)
        {
            hideTimer = 0;
            Debug.Log("Hiding");
            HidingInside = true;
            PlayerCharacter.ActiveState = charactercontroller.PlayerState.Hiding;
            PlayerCharacter.GetComponent<CharacterController>().enabled = false;
            PlayerCharacter.gameObject.transform.position = playerhideposition.position;
            PlayerCharacter.GetComponent<CharacterController>().enabled = true;
            Player = PlayerCharacter.gameObject;
        }
    }
}