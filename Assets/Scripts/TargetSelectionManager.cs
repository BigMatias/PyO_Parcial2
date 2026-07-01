using UnityEngine;

public class TargetSelectionManager : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;
    
    private ActionType pendingAction;
    private bool waitingForTarget = false;
    
    private void Start()
    {
        turnManager.OnTurnStarted += HandleTurnStarted;
    }

    private void OnDestroy()
    {
        if (turnManager != null)
            turnManager.OnTurnStarted -= HandleTurnStarted;
    }
    
    private void HandleTurnStarted(Character character)
    {
        CancelSelection();
    }
    
    public void RequestAction(ActionType actionType)
    {
        pendingAction = actionType;
        waitingForTarget = true;
    }

    public void OnTargetClicked(Character target)
    {
        if (!waitingForTarget) return;

        bool success = turnManager.TryPerformAction(pendingAction, target);

        if (success)
            waitingForTarget = false;
        else
            Debug.Log("Target fuera de rango o acción inválida");
    }

    public void CancelSelection()
    {
        waitingForTarget = false;
    }
}