using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool coolingDown = false;
    private bool coolingDownDrillGold = false;
    private bool coolingDownDrillOil = false;
    public TMPro.TMP_Text goldAmtText;
    public TMPro.TMP_Text goldDrillCount;
    public TMPro.TMP_Text oilDrillCount;
    public Canvas storeCanvas;
    public Button ownBox;
    public Button ownPick;
    public Button ownGoldDrill;
    public Button ownOilDrill;
    public Image collectedBox;
    public Image collectedPick;
    public Image collectedGoldDrill;
    public Image collectedOilDrill;
    public Image goldIcon;
    public Image coalIcon;
    public Image oilIcon;

    [Header("Inventory:")]
    //Inventory Bools For if the player has an item or not
    [SerializeField] private bool _hasPan;
    [SerializeField] private bool _hasSluiceBox;
    [SerializeField] private int _sluiceBoxCost = 50;
    [SerializeField] private bool _hasPick;
    [SerializeField] private int _pickCost = 200;
    [SerializeField] private bool _hasHorseFeed;
    [Header("Transport:")]

    //bools for transport unlocked
    [SerializeField] private bool _hasHorse;
    [SerializeField] private bool _hasStagecoach;
    [SerializeField] private bool _hasTrain;

    //bools for unlocked regions
    [Header("River")]
    [SerializeField] private bool _unlockedRiver;
    public BoxCollider2D _colliderRiver;
    [Header("Mines")]
    [SerializeField] private bool _unlockedMines;
    public BoxCollider2D _colliderMines;
    [Header("Fields")]
    [SerializeField] private bool _unlockedFields;
    public BoxCollider2D _colliderFields;
    [Header("Ints:")]

    //Ints of the amount of things the player has
    [SerializeField][Range(0f, 4f)] private int _wheelsCollected;
    [SerializeField][Range(0f, 4f)] private int _drillsUnlockedGold;
    [SerializeField][Range(0f, 4f)] private int _drillsUnlockedOil;
    [SerializeField] private int _coal;
    [SerializeField] private int _oil;
    [SerializeField] private int _gold;
    [SerializeField] private static int _money;
    [Header("Multipliers:")]
    [SerializeField] private int _multDust = 1;
    [SerializeField] private int _multIngot = 5;
    [SerializeField] private int _multCoal = 5;
    [SerializeField] private int _multDrilledOil = 10;
    [SerializeField] private int _multDrilledGold = 50;
    public static int weight;
    public static int maxWeight = 100;

    //Costs
    [SerializeField] private int _goldDrillCost = 600;
    [SerializeField] private int _oilDrillCost = 400;
    [SerializeField] private int _RiverCost = 40; //must be > sluice box cost
    [SerializeField] private int _MinesCost = 300;
    [SerializeField] private int _FieldsCost = 500;


    private void Update()
    {
        //Updating UI for Store
        if (_hasSluiceBox)
        {
            ownBox.interactable = false;
            collectedBox.enabled = true;
        }
        if (_hasPick)
        {
            ownPick.interactable = false;
            collectedPick.enabled = true;
        }
        if (_drillsUnlockedGold == 4)
        {
            ownGoldDrill.interactable = false;
            collectedGoldDrill.enabled = true;
        }
        if (_drillsUnlockedOil == 4)
        {
            ownOilDrill.interactable = false;
            collectedOilDrill.enabled = true;
        }

        //updating UI for Prices
        ownBox.GetComponentInChildren<TMPro.TMP_Text>().text = "Sluice Box: " + _sluiceBoxCost.ToString();
        ownPick.GetComponentInChildren<TMPro.TMP_Text>().text = "Pick: " + _pickCost.ToString();
        ownGoldDrill.GetComponentInChildren<TMPro.TMP_Text>().text = "Gold Drill: " + _goldDrillCost.ToString();
        ownOilDrill.GetComponentInChildren<TMPro.TMP_Text>().text = "Oil Drill: " + _oilDrillCost.ToString();

        //Updating UI for Drill Amounts
        if (goldDrillCount != null)
            goldDrillCount.text = _drillsUnlockedGold.ToString();
        if (oilDrillCount != null)
            oilDrillCount.text = _drillsUnlockedOil.ToString();

        //Updating UI for Gold Amount
        if (goldAmtText != null)
        {
            goldAmtText.text = "Money: " + _money.ToString();
        }
        else Debug.LogWarning("Money Amount Text is not assigned in the inspector!");

        //Collecting For Drills
        if (!coolingDownDrillGold && _drillsUnlockedGold > 0 && weight < maxWeight)
        {
            _gold += _drillsUnlockedGold * _multDrilledGold;
            weight++;

            StartCoroutine(CooldownDrillGold());
        }
        if (!coolingDownDrillOil && _drillsUnlockedOil > 0)
        {
            _oil += _drillsUnlockedOil * _multDrilledOil;

            StartCoroutine(CooldownDrillOil());
        }

        //River
        if (_unlockedRiver && _colliderRiver != null)
        {
            _colliderRiver.enabled = false;
        }
        else if (_colliderRiver != null)
        {
            _colliderRiver.enabled = true;
        }

        //Mines
        if (_unlockedMines && _colliderRiver != null)
        {
            _colliderMines.enabled = false;
        }
        else if (_colliderMines != null)
        {
            _colliderMines.enabled = true;
        }

        //Fields
        if (_unlockedFields && _colliderFields != null)
        {
            _colliderFields.enabled = false;
        }
        else if (_colliderFields != null)
        {
            _colliderFields.enabled = true;
        }
    }

    public void OpenStore()
    {
        storeCanvas.gameObject.SetActive(true);
    }
    public void CloseStore()
    {
        storeCanvas.gameObject.SetActive(false);
    }
    public void PurchaseBox()
    {
        if (!_hasSluiceBox && _money >= _sluiceBoxCost && _unlockedRiver)
        {
            _money -= _sluiceBoxCost;
            _hasSluiceBox = true;
        }
    }
    public void PurchasePick()
    {
        if (!_hasPick && _money >= _pickCost && _unlockedMines)
        {
            _money -= _pickCost;
            _hasPick = true;
        }
    }
    public void PurchaseGoldDrill()
    {
        if (_drillsUnlockedGold < 4 && _money >= _goldDrillCost && _unlockedFields)
        {
            _money -= _goldDrillCost;
            _drillsUnlockedGold++;
            _goldDrillCost += 100;
        }
    }
    public void PurchaseOilDrill()
    {
        if (_drillsUnlockedOil < 4 && _money >= _oilDrillCost && _unlockedFields)
        {
            _money -= _oilDrillCost;
            _drillsUnlockedOil++;
            _oilDrillCost += 100;
        }
    }
    public void Collecting(CollectionData data)
    {
        if (!coolingDown && _hasPan && data.type == CollectionData.TypeEnum.Dust && weight < maxWeight)
        {
            weight ++;
            _gold += _multDust;
            if (_hasSluiceBox)
            {
                _gold += 2;
                weight += 2;
            }

            StartCoroutine(Cooldown());
        }
        if (!coolingDown && _hasPick && data.type == CollectionData.TypeEnum.Ingot && weight < maxWeight)
        {
            weight++;
            _gold += _multIngot;
            StartCoroutine(Cooldown());
        }else if (!coolingDown && _hasPick && data.type == CollectionData.TypeEnum.Coal && weight < maxWeight)
        {
            weight++;
            _coal += _multCoal;
            StartCoroutine(Cooldown());
        }
    }
    public void CollectWheel()
    {
        if (_wheelsCollected < 4)
        {
            _wheelsCollected++;
        }
    }
    public void UnlockRiver()
    {
        if (_money >= _RiverCost && !_unlockedRiver)
            _money -= _RiverCost;
        _unlockedRiver = true;
    }
    public void UnlockMines()
    {
        if (_money >= _MinesCost && !_unlockedMines && _unlockedRiver)
            _money -= _MinesCost;
        _unlockedMines = true;
    }
    public void UnlockFields()
    {
        if (_money >= _FieldsCost && !_unlockedFields && _unlockedRiver && _unlockedMines)
            _money -= _FieldsCost;
        _unlockedFields = true;
    }


    IEnumerator Cooldown()
    {
        coolingDown = true;
        yield return new WaitForSeconds(2f);
        coolingDown = false;
    }
    IEnumerator CooldownDrillGold()
    {
        coolingDownDrillGold = true;
        yield return new WaitForSeconds(20f);
        coolingDownDrillGold = false;
    }
    IEnumerator CooldownDrillOil()
    {
        coolingDownDrillOil = true;
        yield return new WaitForSeconds(20f);
        coolingDownDrillOil = false;
    }

    public void Cheats()
    {
        _hasPan = true;
        _money += 1000000;
        _unlockedRiver = true;
        _unlockedMines = true;
        _unlockedFields = true;
    }
}
