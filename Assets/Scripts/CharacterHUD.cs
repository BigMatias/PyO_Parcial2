using UnityEngine;
using TMPro;

public class CharacterHUD : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private TextMeshProUGUI hpText;

    private void Start()
    {
        character.OnHPChanged += UpdateHPText;
        UpdateHPText(character.CurrentHP);
    }

    private void UpdateHPText(int hp)
    {
        hpText.text = hp.ToString();
    }
}