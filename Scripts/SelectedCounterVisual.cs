using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;


    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnselectedCounterChanged;
        //We did this in start and not awake since in awake there is a possibility it might run before the player class
    }


    private void Player_OnselectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
            Show();
        else
            Hide();
    }


    private void Show()
    {
        visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        visualGameObject.SetActive(false);
    }

}
