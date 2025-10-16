using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region
    public UnityEvent startEvent;
    public static bool hasStarted = false;
    private bool coolingDown = false;
    private bool coolingDownDrillGold = false;
    private bool coolingDownDrillOil = false;
    public TMPro.TMP_Text moneyAmtTextTools;
    public TMPro.TMP_Text moneyAmtTextLand;
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
    public Button ownRiver;
    public Button ownFields;
    public Button usingTown;
    public Button usingHorse;
    public Button usingStagecoach;
    public Button usingTrain;
    public Image collectedPan;
    public Image collectedBox;
    public Image collectedPick;
    public Image selectedPan;
    public Image selectedBox;
    public Image selectedPick;
    public Image collectedGoldDrill;
    public Image collectedOilDrill;
    public GameObject wheel1;
    public GameObject wheel2;
    public GameObject wheel3;
    public GameObject wheel4;
    public GameObject feed;
    public GameObject oilDrill;
    public GameObject goldDrill1;
    public GameObject goldDrill2;
    public GameObject goldDrill3;
    public GameObject goldDrill4;
    public GameObject stagecoachNoWheels;
    public GameObject stagecoachWithWheels;
    #endregion

    [Header("Inventory:")]
    //Inventory Bools For if the player has an item or not
    [SerializeField] static private bool _hasPan;
    [SerializeField] static private bool _hasSluiceBox;
    [SerializeField] static private int _sluiceBoxCost = 50;
    [SerializeField] static private bool _hasPick;
    [SerializeField] static private int _pickCost = 200;
    [SerializeField] static private bool _hasHorseFeed;
    [Header("Transport:")]

    //bools for transport unlocked
    [SerializeField] static private bool _hasHorse;
    [SerializeField] static private bool _hasStagecoach;
    [SerializeField] static private bool _hasTrain;
    [SerializeField] static private bool _usingTown = true;
    [SerializeField] static private bool _usingHorse;
    [SerializeField] static private bool _usingStagecoach;
    [SerializeField] static private bool _usingTrain;

    //bools for unlocked regions
    [Header("River")]
    [SerializeField] static private bool _unlockedRiver;
    public TilemapCollider2D _colliderRiver;
    [Header("Mines")]
    [SerializeField] static private bool _unlockedMines;
    public TilemapCollider2D _colliderMines;
    [Header("Fields")]
    [SerializeField] static private bool _unlockedFields;
    public TilemapCollider2D _colliderFields;
    [Header("Other:")]

    //Ints of the amount of things the player has
    [SerializeField] static private bool _wheelsCollected1;
    [SerializeField] static private bool _wheelsCollected2;
    [SerializeField] static private bool _wheelsCollected3;
    [SerializeField] static private bool _wheelsCollected4;
    [SerializeField][Range(0f, 4f)] static private int _drillsUnlockedGold;
    [SerializeField] static private bool _drillsUnlockedOil;
    [SerializeField] static private int _coal;
    [SerializeField] static private int _oil;
    [Header("Multipliers:")]
    [SerializeField] static private int _multDust = 1;
    [SerializeField] static private int _multIngot = 5;
    [SerializeField] static private int _multCoal = 5;
    [SerializeField] static private int _multDrilledOil = 10;
    [SerializeField] static private int _multDrilledGold = 50;
    public static int _gold;
    public static int _maxGold;
    public static int _money;
    public static int weight;
    public static int maxWeight = 100;

    //Costs
    [SerializeField] static private int _goldDrillCost = 600;
    [SerializeField] static private int _oilDrillCost = 400;
    [SerializeField] static private int _RiverCost = 40; //must be < sluice box cost
    [SerializeField] static private int _FieldsCost = 500;

    //Equiped Items
    [SerializeField] static private bool _equippedPan;
    [SerializeField] static private bool _equippedBox;
    [SerializeField] static private bool _equippedPick;

    private void Start()
    {
        if (!hasStarted)
        {
            _gold = 0;
            _money = 40;
            _hasPan = true;
            hasStarted = true;
            startEvent.Invoke();
        }
    } //done
    private void Update()
    {
        //updating wheels collected
        wheel1.SetActive(!_wheelsCollected1 ? true : false);
        wheel2.SetActive(!_wheelsCollected2 ? true : false);
        wheel3.SetActive(!_wheelsCollected3 ? true : false);
        wheel4.SetActive(!_wheelsCollected4 ? true : false);

        //updating stagecoach model
        if (_wheelsCollected1 && _wheelsCollected2 && _wheelsCollected3 && _wheelsCollected4)
        {
            stagecoachNoWheels.SetActive(false);
            stagecoachWithWheels.SetActive(true);
        }
        else
        {
            stagecoachNoWheels.SetActive(true);
            stagecoachWithWheels.SetActive(false);
        }

        //updating transport
        if (_hasHorseFeed)
            _hasHorse = true;
        if (_wheelsCollected1 && _wheelsCollected2 && _wheelsCollected3 && _wheelsCollected4)
            _hasStagecoach = true;
        if (_drillsUnlockedOil)
        {
            _hasTrain = true;
        }

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
        if (_unlockedRiver)
            ownRiver.interactable = false;
        if (_unlockedFields)
            ownFields.interactable = false;
        //Updating UI for Transport
        usingTown.interactable = !_usingTown ? true : false;
        usingHorse.interactable = (!_usingHorse && _hasHorse) ? true : false;
        usingStagecoach.interactable = (!_usingStagecoach && _hasStagecoach) ? true : false;
        usingTrain.interactable = (!_usingTrain && _hasTrain && _drillsUnlockedOil && _coal >= 20) ? true : false;

        //updating UI for Prices
        ownBox.GetComponentInChildren<TMPro.TMP_Text>().text = "Sluice Box: " + _sluiceBoxCost.ToString();
        ownPick.GetComponentInChildren<TMPro.TMP_Text>().text = "Pick: " + _pickCost.ToString();
        ownGoldDrill.GetComponentInChildren<TMPro.TMP_Text>().text = "Gold Drill: " + _goldDrillCost.ToString();
        ownOilDrill.GetComponentInChildren<TMPro.TMP_Text>().text = "Oil Drill: " + _oilDrillCost.ToString();
        ownRiver.GetComponentInChildren<TMPro.TMP_Text>().text = "River: " + _RiverCost.ToString();
        ownFields.GetComponentInChildren<TMPro.TMP_Text>().text = "Fields: " + _FieldsCost.ToString();

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
        if (moneyAmtTextTools != null)
        {
            moneyAmtTextTools.text = "Money: " + _money.ToString();
        }
        else Debug.LogWarning("Money Amount Text is not assigned in the inspector!");
        if (moneyAmtTextLand != null)
        {
            moneyAmtTextLand.text = "Money: " + _money.ToString();
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
    public void CloseGame()
    {
        Application.Quit();
    }
    public void BuyTicket()
    {
        if (_money >= 2000f && _coal >= 200 && _drillsUnlockedOil)
        {
            SceneManager.LoadScene(3);
        }
    }
    public void SetMoneyMax()
    {
        if (_money > _maxGold)
            _gold = _maxGold;
    }
    public void SelectTown()
    {
        _usingTown = true;
        _usingHorse = false;
        _usingStagecoach = false;
        _usingTrain = false;
        _maxGold = 15;
        SetMoneyMax();
    }
    public void SelectHorse()
    {
        if (_hasHorse)
        {
            _usingTown = false;
            _usingHorse = true;
            _usingStagecoach = false;
            _usingTrain = false;
            _maxGold = 25;
            SetMoneyMax();
        }
    }
    public void SelectStagecoach()
    {
        if (_hasStagecoach)
        {
            _usingTown = false;
            _usingHorse = false;
            _usingStagecoach = true;
            _usingTrain = false;
            _maxGold = 50;
            SetMoneyMax();
        }
    }
    public void SelectTrain()
    {
        if (_hasTrain && _drillsUnlockedOil && _coal >= 20f)
        {
            _usingTown = false;
            _usingHorse = false;
            _usingStagecoach = false;
            _usingTrain = true;
            _coal -= 20;
            _maxGold = 100;
            SetMoneyMax();
        }
    }
    public void buyRiver()
    {
        if(_money >= _RiverCost && !_unlockedRiver)
        {
            _money -= _RiverCost;
            _unlockedRiver = true;
        }
    }
    public void buyFields()
    {
        if(_money >= _FieldsCost && !_unlockedFields && _unlockedRiver && _unlockedMines)
        {
            _money -= _FieldsCost;
            _unlockedFields = true;
        }
    }
    public void ClickPan()
    {
        if (_hasPan)
        {
            _equippedPan = !_equippedPan;
            _equippedBox = false;
            _equippedPick = false;
        }
    } //done
    public void ClickBox()
    {
        if (_hasSluiceBox)
        {
            _equippedBox = !_equippedBox;
            _equippedPan = false;
            _equippedPick = false;
        }
    } //done
    public void ClickPick()
    {
        if (_hasPick)
        {
            _equippedPick = !_equippedPick;
            _equippedPan = false;
            _equippedBox = false;
        }
    } //done
    public void RemoveGold()
    {
        if (_gold >= 5)
            _gold -= 5;
        else if (_gold >= 0)
            _gold = 0;
    } //done
    public void TransferToMoney()
    {
        _money += _gold;
        weight = 0;
        _gold = 0;
    }
    public void PurchaseBox()
    {
        if (!_hasSluiceBox && _money >= _sluiceBoxCost && _unlockedRiver)
        {
            _money -= _sluiceBoxCost;
            _hasSluiceBox = true;
        }
    } //done
    public void PurchasePick()
    {
        if (!_hasPick && _money >= _pickCost && _unlockedMines)
        {
            _money -= _pickCost;
            _hasPick = true;
        }
    } //done
    public void PurchaseGoldDrill()
    {
        if (_drillsUnlockedGold < 4 && _money >= _goldDrillCost && _unlockedFields)
        {
            _money -= _goldDrillCost;
            _drillsUnlockedGold++;
            _goldDrillCost += 100;
        }
        if (_drillsUnlockedGold == 1)
            goldDrill1.SetActive(true);
        else if (_drillsUnlockedGold == 2)
            goldDrill2.SetActive(true);
        else if (_drillsUnlockedGold == 3)
            goldDrill3.SetActive(true);
        else if (_drillsUnlockedGold == 4)
            goldDrill4.SetActive(true);

    } //done
    public void PurchaseOilDrill()
    {
        if (!_drillsUnlockedOil && _money >= _oilDrillCost && _unlockedFields)
        {
            _money -= _oilDrillCost;
            _drillsUnlockedOil = true;
            _oilDrillCost += 100;
            oilDrill.SetActive(true);
        }
    } //done
    public void Collecting(CollectionData data)
    {
        if (!coolingDown && _hasPan && data.type == CollectionData.TypeEnum.Dust && weight < maxWeight && _equippedPan)
        {
            weight++;
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
    } //done
    public void CollectWheel1()
    {
        if (!_wheelsCollected1)
        {
            _wheelsCollected1 = true;
            wheel1.SetActive(false);
        }
    } //done
    public void CollectWheel2()
    {
        if (!_wheelsCollected2)
        {
            _wheelsCollected2 = true;
            wheel2.SetActive(false);
        }
    } //done
    public void CollectWheel3()
    {
        if (!_wheelsCollected3)
        {
            _wheelsCollected3 = true;
            wheel3.SetActive(false);
        }
    } //done
    public void CollectWheel4()
    {
        if (!_wheelsCollected4)
        {
            _wheelsCollected4 = true;
            wheel4.SetActive(false);
        }
    } //done
    public UnityEvent onCollectFeed;
    public void CollectFeed()
    {
        if (!_hasHorseFeed)
        {
            _hasHorseFeed = true;
            feed.SetActive(false);
        }
    } //done
    public void UnlockRiver()
    {
        if (_money >= _RiverCost && !_unlockedRiver)
        {
            _money -= _RiverCost;
            _unlockedRiver = true;
        }
    } //done
    public void UnlockMines()
    {
            _unlockedMines = true;
    } //done
    public void UnlockFields()
    {
        if (_money >= _FieldsCost && !_unlockedFields && _unlockedRiver && _unlockedMines)
        {
            _money -= _FieldsCost;
            _unlockedFields = true;
        }
    } //done


    IEnumerator Cooldown()
    {
        coolingDown = true;
        yield return new WaitForSeconds(2f);
        coolingDown = false;
    } //done
    IEnumerator CooldownDrillGold()
    {
        coolingDownDrillGold = true;
        yield return new WaitForSeconds(20f);
        coolingDownDrillGold = false;
    } //done
    #region
    public void Cheats()
    {
        _money += 1000000;
        _coal += 200;
    }
    #endregion
}