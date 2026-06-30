using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerButtons : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Button[] actionButtons;
    [SerializeField] private TargetSelectionManager targetSelectionManager;

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
    }

    public void SetInteractable(bool interactable)
    {
        foreach (Button b in actionButtons)
            b.interactable = interactable;
    }
}