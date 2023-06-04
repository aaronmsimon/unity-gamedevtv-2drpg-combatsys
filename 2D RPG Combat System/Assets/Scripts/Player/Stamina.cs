using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singleton<Stamina>
{
    public int CurrentStamina { get; private set; }

    [SerializeField] private float staminaRefreshTime;
    [SerializeField] private Sprite emptyStamina;
    [SerializeField] private Sprite filledStamina;

    // Not serialized because of how UI was setup
    private int maxStamina = 3;
    private Transform staminaContainer;

    const string STAMINA_CONTAINER_TEXT = "Stamina Container";

    const string STAMINA = "Stamina";

    protected override void Awake() {
        base.Awake();

        CurrentStamina = maxStamina;
    }

    private void Start() {
        staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
        // I put it here, but Stephen makes a good point that you can get an orb, and the timer run out right away, thereby getting two at once
        StartCoroutine(RefreshStaminaRoutine());
    }

    public void UseStamina() {
        CurrentStamina -= 1;
        UpdateStaminaImages();
    }

    public void RefreshStamina() {
        if (CurrentStamina < maxStamina) {
            CurrentStamina += 1;
            UpdateStaminaImages();
        }
    }

    private void UpdateStaminaImages() {
        for (int i = 0; i < maxStamina; i++)
        {
            if (i <= CurrentStamina - 1) {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = filledStamina;
            } else {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = emptyStamina;
            }
        }

        if (CurrentStamina < maxStamina) {
            // to get one instance only at a time (as mentioned above), stop this (only) routine:
            StopAllCoroutines();
            // perhaps this would still work with my method...?
            StartCoroutine(RefreshStaminaRoutine());
        }
    }

    private IEnumerator RefreshStaminaRoutine() {
        // This was my version, which worked:
        /*
        yield return new WaitForSeconds(staminaRefreshTime);
        RefreshStamina();
        StartCoroutine(RefreshStaminaRoutine());
        */
        while (true) {
            yield return new WaitForSeconds(staminaRefreshTime);
            RefreshStamina();
        }
    }
}
