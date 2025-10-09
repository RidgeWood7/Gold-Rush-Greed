using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool coolingDown = false;
    private bool coolingDownDrillGold = false;
    private bool coolingDownDrillOil = false;
    public TMPro.TMP_Text goldAmtText;

    [Header("Inventory:")]
    //Inventory Bools For if the player has an item or not
    [SerializeField] private bool _hasPan;
    [SerializeField] private bool _hasSluiceBox;
    [SerializeField] private bool _hasPick;
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
    [SerializeField][Range(0f, 4f)] private int _wheelsInInv;
    [SerializeField][Range(0f, 4f)] private int _drillsUnlockedOil;
    [SerializeField][Range(0f, 4f)] private int _drillsUnlockedGold;
    [SerializeField] private int _coal;
    [SerializeField] private int _oil;
    [SerializeField] private int _gold;
    [Header("Multipliers:")]
    [SerializeField] private int _multDust = 1;
    [SerializeField] private int _multIngot = 5;
    [SerializeField] private int _multCoal = 5;
    [SerializeField] private int _multDrilledOil = 10;
    [SerializeField] private int _multDrilledGold = 50;
    public int weight;

    private void Update()
    {
        //Updating UI for Gold Amount
        if (goldAmtText != null)
        {
            goldAmtText.text = "Gold: " + _gold.ToString();
        } else  Debug.LogWarning("Gold Amount Text is not assigned in the inspector!");

        //Collecting For Drills
        if (!coolingDownDrillGold && _drillsUnlockedGold > 0)
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
            Debug.Log("Unlocked");
            _colliderRiver.enabled = false;
        }
        else if (_colliderRiver != null)
        {
            _colliderRiver.enabled = true;
        }

        //Mines
        if (_unlockedMines && _colliderRiver != null)
        {
            Debug.Log("Unlocked");
            _colliderMines.enabled = false;
        }
        else if (_colliderMines != null)
        {
            _colliderMines.enabled = true;
        }

        //Fields
        if (_unlockedFields && _colliderFields != null)
        {
            Debug.Log("Unlocked");
            _colliderFields.enabled = false;
        }
        else if (_colliderFields != null)
        {
            _colliderFields.enabled = true;
        }
    }

    public void Collecting(CollectionData data)
    {
        if (!coolingDown && _hasPan && data.type == CollectionData.TypeEnum.Dust)
        {
            weight += data.weightAdding * _multDust;
            _gold += _multDust;
            if (_hasSluiceBox)
            {
                _gold += 2;
                weight += 2;
            }

            StartCoroutine(Cooldown());
        }
        if (!coolingDown && _hasPick && data.type == CollectionData.TypeEnum.Ingot)
        {
            weight += data.weightAdding * _multIngot;
            _gold += _multIngot;
            StartCoroutine(Cooldown());
        }else if (!coolingDown && _hasPick && data.type == CollectionData.TypeEnum.Coal)
        {
            weight += data.weightAdding * _multCoal;
            _gold += _multCoal;
            StartCoroutine(Cooldown());
        }
    }
    public void AddWheel()
    {
        if (_wheelsInInv < 4)
        {
            _wheelsInInv++;
        }
    }
    public void UseWheel()
    {
        if (_wheelsInInv > 0)
        {
            _wheelsInInv--;
            _wheelsCollected++;
        }
    }
    public void UnlockDrillOil()
    {
        if (_wheelsCollected > 0 && _drillsUnlockedOil < 4)
        {
            _drillsUnlockedOil++;
        }
    }
    public void UnlockDrillGold()
    {
        if (_wheelsCollected > 0 && _drillsUnlockedGold < 4)
        {
            _drillsUnlockedGold++;
        }
    }
    public void UnlockRiver()
    {
        _unlockedRiver = true;
    }
    public void UnlockMines()
    {
        _unlockedMines = true;
    }
    public void UnlockFields()
    {
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
}
