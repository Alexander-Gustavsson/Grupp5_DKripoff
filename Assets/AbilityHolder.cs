using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    float cooldownTime; //Behöver skapa en counter som räknar rundor så att cooldown är x antal rundor istället för tid. 
    public bool isActive = false;
    public Button AbilityButton; //Lägg till UI elementet i scenen. Används som knapp för att aktivera ability. 
    bool buttonPressed = false;
    public Button CancelButton; //Avbrytknapp
    bool cancelButtonPressed = false;

    InputClick inputClick;
    void Start()
    {
        AbilityButton.onClick.AddListener(OnButtonClicked);
        CancelButton.onClick.AddListener(OnCancelButtonClicked);
    }

    enum AbilityState
    {
        ready,
        active,
        canceled, 
        cooldown
    }

    AbilityState state = AbilityState.ready;

    void OnButtonClicked()
    {
        buttonPressed = true;
    }

    void OnCancelButtonClicked()
    {
        cancelButtonPressed = true;
    }

    void Update()
    {
        switch (state)
        {

            case AbilityState.ready:
                if (buttonPressed) //Korrigera detta så att den kollar att rätt icon klickas
                {
                    ability.Activate();
                    state = AbilityState.active;
                    isActive = true;

                    buttonPressed = false;
                }

                if (cancelButtonPressed) //If-statement som kollar om spelaren avbryter abilityn. 
                {
                    state = AbilityState.canceled;

                    cancelButtonPressed = false;
                }
                break;
            case AbilityState.canceled:
                
                    state = AbilityState.ready; //skickar tillbaka till case ready så att abilityn kan användas igen.

                break;
            case AbilityState.active:
                break;
            case AbilityState.cooldown:
                break;

        }


    }

}
