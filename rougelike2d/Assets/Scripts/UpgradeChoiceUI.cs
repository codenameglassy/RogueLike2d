using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeChoiceUI : MonoBehaviour
{
    [Header("UI")]
    public Image icon;
    public Image rarityImg;       // This will show rarity color
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI rarityText; // NEW: display rarity name
    public TextMeshProUGUI descriptionText;
    public Button selectButton;

    [Header("Rarity Colors")]
    public Color commonColor = Color.white;
    public Color uncommonColor = Color.green;
    public Color rareColor = Color.blue;
    public Color legendaryColor = Color.magenta;

    private UpgradeSO upgrade;
    private UpgradeManager manager;

    public void Setup(UpgradeSO upgradeSO, UpgradeManager upgradeManager)
    {
        upgrade = upgradeSO;
        manager = upgradeManager;

        // UI updates
        icon.sprite = upgrade.icon;
        nameText.text = upgrade.upgradeName;
        descriptionText.text = upgrade.description;

        // Set rarity color
        rarityImg.color = GetRarityColor(upgrade.rarity);

        // Set rarity text
        rarityText.text = upgrade.rarity.ToString();

        // Optionally, color the text too
        rarityText.color = GetRarityColor(upgrade.rarity);

        // Button
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(OnSelected);
    }

    private Color GetRarityColor(UpgradeRarity rarity)
    {
        return rarity switch
        {
            UpgradeRarity.Common => commonColor,
            UpgradeRarity.Uncommon => uncommonColor,
            UpgradeRarity.Rare => rareColor,
            UpgradeRarity.Legendary => legendaryColor,
            _ => Color.white
        };
    }

    private void OnSelected()
    {
        manager.SelectUpgrade(upgrade);
    }
}
