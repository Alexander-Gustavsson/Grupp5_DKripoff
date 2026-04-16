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

    [SerializeField] private GamePlay gamePlay;

    InputClick inputClick;
    void Start()
    {
        AbilityButton.onClick.AddListener(OnButtonClicked);
        CancelButton.onClick.AddListener(OnCancelButtonClicked);
        ability.SetGamePlay(gamePlay);
        ability.SetAbilityHolder(this);
        CancelButton.gameObject.SetActive(false);
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

    public void HandleGridClick(Vector2 pos)
    {
        ability.HandleClick(pos);
    }

    public void AbilityFinished()
    {
        gamePlay.inputLocked = false;

        state = AbilityState.ready;

        AbilityButton.gameObject.SetActive(true);
        CancelButton.gameObject.SetActive(false);

        isActive = false;
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

                    gamePlay.inputLocked = true;

                    AbilityButton.gameObject.SetActive(false);
                    CancelButton.gameObject.SetActive(true);
                    buttonPressed = false;
                }

                if (cancelButtonPressed) //If-statement som kollar om spelaren avbryter abilityn. 
                {
                    state = AbilityState.canceled;
                }
                break;
            case AbilityState.active:
                if (cancelButtonPressed)
                {
                    state = AbilityState.canceled;
                }
                break;
            case AbilityState.canceled:

                gamePlay.inputLocked = false;
                state = AbilityState.ready; //skickar tillbaka till case ready så att abilityn kan användas igen.


                AbilityButton.gameObject.SetActive(true);
                CancelButton.gameObject.SetActive(false);

                cancelButtonPressed = false;

                break;
          
            case AbilityState.cooldown:
                break;

        }


    }

}
