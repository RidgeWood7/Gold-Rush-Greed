using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region
    public UnityEvent startEvent;
    private bool coolingDown = false;
    private bool coolingDownDrillGold = false;
    private bool coolingDownDrillOil = false;
    public TMPro.TMP_Text goldAmtText;
    public TMPro.TMP_Text goldDrillCount;
    public TMPro.TMP_Text oilDrillCount;
    public TMPro.TMP_Text goldText;
    public TMPro.TMP_Text coalText;
    public TMPro.TMP_Text weightText;
    public Canvas storeCanvas;
    public Button ownBox;
    public Button ownPick;
    public Button ownGoldDrill;
    public Button ownOilDrill;
    public Image collectedPan;
    public Image collectedBox;
    public Image collectedPick;
    public Image selectedPan;
    public Image selectedBox;
    public Image selectedPick;
    public Image collectedGoldDrill;
    public Image collectedOilDrill;
    #endregion

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
    public TilemapCollider2D _colliderRiver;
    [Header("Mines")]
    [SerializeField] private bool _unlockedMines;
    public TilemapCollider2D _colliderMines;
    [Header("Fields")]
    [SerializeField] private bool _unlockedFields;
    public TilemapCollider2D _colliderFields;
    [Header("Other:")]

    //Ints of the amount of things the player has
    [SerializeField][Range(0f, 4f)] private int _wheelsCollected;
    [SerializeField][Range(0f, 4f)] private int _drillsUnlockedGold;
    [SerializeField] private bool _drillsUnlockedOil;
    [SerializeField] private int _coal;
    [SerializeField] private int _oil;
    [Header("Multipliers:")]
    [SerializeField] private int _multDust = 1;
    [SerializeField] private int _multIngot = 5;
    [SerializeField] private int _multCoal = 5;
    [SerializeField] private int _multDrilledOil = 10;
    [SerializeField] private int _multDrilledGold = 50;
    public static int _gold;
    public static int _money;
    public static int weight;
    public static int maxWeight = 100;

    //Costs
    [SerializeField] private int _goldDrillCost = 600;
    [SerializeField] private int _oilDrillCost = 400;
    [SerializeField] private int _RiverCost = 40; //must be > sluice box cost
    [SerializeField] private int _MinesCost = 300;
    [SerializeField] private int _FieldsCost = 500;

    //Equiped Items
    [SerializeField] private bool _equippedPan;
    [SerializeField] private bool _equippedBox;
    [SerializeField] private bool _equippedPick;

    private void Start() => startEvent.Invoke();
    public void ClickPan()
    {
        if (_hasPan)
        {
            _equippedPan = !_equippedPan;
            _equippedBox = false;
            _equippedPick = false;
        }
    }
    public void ClickBox()
    {
        if (_hasSluiceBox)
        {
            _equippedBox = !_equippedBox;
            _equippedPan = false;
            _equippedPick = false;
        }
    }
    public void ClickPick()
    {
        if (_hasPick)
        {
            _equippedPick = !_equippedPick;
            _equippedPan = false;
            _equippedBox = false;
        }
    }

    private void Update()
    {
        //Updating UI for Inventory
        selectedPan.enabled = _equippedPan == true ? true : false;
        selectedBox.enabled = _equippedBox == true ? true : false;
        selectedPick.enabled = _equippedPick == true ? true : false;

        if (_hasPan)
            collectedPan.enabled = true;
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
        if (_drillsUnlockedOil)
        {
            ownOilDrill.interactable = false;
            collectedOilDrill.enabled = true;
        }

        //updating UI for Prices
        ownBox.GetComponentInChildren<TMPro.TMP_Text>().text = "Sluice Box: " + _sluiceBoxCost.ToString();
        ownPick.GetComponentInChildren<TMPro.TMP_Text>().text = "Pick: " + _pickCost.ToString();
        ownGoldDrill.GetComponentInChildren<TMPro.TMP_Text>().text = "Gold Drill: " + _goldDrillCost.ToString();
        ownOilDrill.GetComponentInChildren<TMPro.TMP_Text>().text = "Oil Drill: " + _oilDrillCost.ToString();

        //Updating UI for Amounts
        if (goldDrillCount != null)
            goldDrillCount.text = _drillsUnlockedGold.ToString();
        if (oilDrillCount != null)
            oilDrillCount.text = _drillsUnlockedOil ? "1" : "0";
        if (goldText != null)
            goldText.text = "Gold: " + _gold.ToString();
        if (coalText != null)
            coalText.text = "Coal: " + _coal.ToString();
        if (weightText != null)
            weightText.text = "Weight: " + weight.ToString() + "/" + maxWeight.ToString();


        //Updating UI for Gold Amount
        if (goldAmtText != null)
        {
            goldAmtText.text = "Money: " + _money.ToString();
        }
        else Debug.LogWarning("Money Amount Text is not assigned in the inspector!");

        //Collecting For Drills
        if (!coolingDownDrillGold && _drillsUnlockedGold > 0 && weight < maxWeight - 5)
        {
            _gold += _drillsUnlockedGold * _multDrilledGold;
            weight += 5;

            StartCoroutine(CooldownDrillGold());
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
        if (_unlockedMines && _colliderMines != null)
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
    public void RemoveGold()
    {

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
        if (!_drillsUnlockedOil && _money >= _oilDrillCost && _unlockedFields)
        {
            _money -= _oilDrillCost;
            _drillsUnlockedOil = true;
            _oilDrillCost += 100;
        }
    }
    public void Collecting(CollectionData data)
    {
        if (!coolingDown && _hasPan && data.type == CollectionData.TypeEnum.Dust && weight < maxWeight && _equippedPan)
        {
            weight ++;
            _gold += _multDust;

            StartCoroutine(Cooldown());
        }
        else if (!coolingDown && _hasSluiceBox && data.type == CollectionData.TypeEnum.Dust && weight < maxWeight && _equippedBox)
        {
            weight += 3;
            _gold += 3;

            StartCoroutine(Cooldown());
        }
        if (!coolingDown && _hasPick && data.type == CollectionData.TypeEnum.Ingot && weight < maxWeight && _equippedPick)
        {
            weight++;
            _gold += _multIngot;
            StartCoroutine(Cooldown());
        }
        else if (!coolingDown && _hasPick && data.type == CollectionData.TypeEnum.Coal && weight < maxWeight && _equippedPick)
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
    public UnityEvent onCollectFeed;
    public void CollectFeed()
    {
        if (!_hasHorseFeed)
        {
            _hasHorseFeed = true;
            onCollectFeed.Invoke();
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
    #region
    public void Cheats()
    {
        _hasPan = true;
        _money += 1000000;
        _unlockedRiver = true;
        _unlockedMines = true;
        _unlockedFields = true;
    }
    #endregion
}