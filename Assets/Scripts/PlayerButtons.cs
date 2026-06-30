using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerButtons : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Button[] actionButtons;
    [SerializeField] private TargetSelectionManager targetSelectionManager;
    [SerializeField] private TurnManager turnManager;

    private void Start()
    {
        List<ActionStrategy> strategies = character.GetActionStrategies();

        for (int i = 0; i < actionButtons.Length; i++)
        {
            if (i < strategies.Count)
            {
                ActionStrategy strategy = strategies[i];
                actionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = strategy.ActionName;

                ActionType actionType = strategy.ActionType;
                actionButtons[i].onClick.AddListener(() => targetSelectionManager.RequestAction(actionType));
            }
            else
            {
                actionButtons[i].gameObject.SetActive(false);
            }
        }

        turnManager.OnTurnStarted += HandleTurnStarted;
        SetInteractable(turnManager.CurrentCharacter == character); 
    }

    private void OnDestroy()
    { 
        turnManager.OnTurnStarted -= HandleTurnStarted;
    }

    private void HandleTurnStarted(Character activeCharacter)
    {
        SetInteractable(activeCharacter == character);
    }

    public void SetInteractable(bool interactable)
    {
        foreach (Button b in actionButtons)
            if (b.gameObject.activeSelf)
                b.interactable = interactable;
    }
}