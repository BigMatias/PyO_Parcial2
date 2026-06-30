using System;
using UnityEngine;

public class CharacterClickHandler : MonoBehaviour
{
    [SerializeField] private TargetSelectionManager targetSelectionManager;

    private Character character;
    
    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void OnMouseDown()
    {
        targetSelectionManager.OnTargetClicked(character);
    }
}