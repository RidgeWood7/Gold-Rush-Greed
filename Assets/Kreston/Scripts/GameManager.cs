using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Inventory:")]
    //Inventory Bools For if the player has an item or not
    //public bool has
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
    [SerializeField] private BoxCollider2D _boxColliderRiver;
    [Header("Mines")]
    [SerializeField] private bool _unlockedMines;
    [SerializeField] private BoxCollider2D _boxColliderMines;
    [Header("Fields")]
    [SerializeField] private bool _unlockedFields;
    [SerializeField] private BoxCollider2D _boxColliderFields;
    [Header("Ints:")]
    //Ints of the amount of things the player has
    [SerializeField] private int _wheelsCollected;
    [SerializeField] private int _wheelsInInv;
    [SerializeField] private int _coal;
    [SerializeField] private int _oil;
    [SerializeField] private int _gold;
    [Header("Multipliers:")]
    [SerializeField] private int _multDust = 1;
    [SerializeField] private int _multIngot = 8;
    [SerializeField] private int _multDrilled = 22;
    public int weight;

    //Region Checkers
    private void Update()
    {
        //Mines
        if (_unlockedMines){

        }
    }

    public void Collecting(int goldAmount, int weightAdding, string type)
    {
        weight += weightAdding;

        _gold += type switch
        {
            "Dust" => goldAmount * _multDust,
            "Ingot" => goldAmount * _multIngot,
            "Drilled" => goldAmount * _multDrilled,
            _ => 0
        };

        if (type != "Dust" && type != "Ingot" && type != "Drilled")
            Debug.Log("There was not a correct string name, please enter either: \"Dust\" \"Ingot\" \"Drilled\" ");
    }
}
