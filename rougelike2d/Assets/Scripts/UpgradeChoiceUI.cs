using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeChoiceUI : MonoBehaviour
{
    [Header("UI")]
    public Image icon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Button selectButton;

    private UpgradeSO upgrade;
    private UpgradeManager manager;

    public void Setup(UpgradeSO upgradeSO, UpgradeManager upgradeManager)
    {
        upgrade = upgradeSO;
        manager = upgradeManager;

        icon.sprite = upgrade.icon;
        nameText.text = upgrade.upgradeName;
        descriptionText.text = upgrade.description;

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(OnSelected);
    }

    private void OnSelected()
    {
        manager.SelectUpgrade(upgrade);
    }
}
