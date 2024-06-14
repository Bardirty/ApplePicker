using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LumberShopController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PlayerController player;
    [SerializeField] private AxeController axe;
    [SerializeField] private ShopController shop;
    [SerializeField] private int[] prices = new int[] {10, 200, 100, 150};
    [Header ("UI")]
    [SerializeField] private Button back;
    [SerializeField] private Canvas shopUI;
    [SerializeField] private Canvas interfaceUI;
    [SerializeField] private TextMeshProUGUI woodInfo;
    [SerializeField] private TextMeshProUGUI message;
    [Header("Shop Params")]
    private int capacity = 0;
    [SerializeField] private int capacityMax = 10;
    [SerializeField] private Material spaceMat;
    [SerializeField] private GameObject tavern;
    [SerializeField] private GameObject sawmill;
    private MeshRenderer axeRenderer;
    private bool[] isBought = new bool[3];
    private void Start()
    {
        for (int i = 0; i < isBought.Length; isBought[i] = false, i++) ;
        message.text = null;
    }
    private void FixedUpdate()
    {
        woodInfo.text = axe.wood.ToString();
    }

    public void Back()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        interfaceUI.gameObject.SetActive(true);
        shopUI.gameObject.SetActive(false);
        player.isPaused = 1;
    }
    public void WoodCapacityIncrease()
    {
        if (axe.wood >= prices[0] && capacity < capacityMax)
        {
            axe.woodLimit += 20;
            axe.wood -= prices[0];
            capacity++;
        }
        else StartCoroutine(SubtitlesInfo(prices[0], 0));
    }
    public void SpaceAxe()
    {
        if (axe.wood >= prices[1] && shop.isBought[1] && !isBought[0])
        {
            GameObject axePrefab = Camera.main.transform.GetChild(0).gameObject;
            axe.axeMinDamage *= 10;
            axe.axeMaxDamage *= 10;
            player.goldenApples -= prices[3];
            isBought[0] = true;
            axeRenderer = axePrefab.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
            axeRenderer.material = spaceMat;
        }
        else StartCoroutine(SubtitlesInfo(prices[2], 1));
    }
    public void Tavern()
    {
        if (axe.wood >= prices[2] && !isBought[1])
        {
            tavern.SetActive(true);
            axe.wood -= prices[2];
            isBought[1] = true;
        }
    }
    public void Sawmill()
    {
        if (axe.wood >= prices[3] && !isBought[2])
        {
            sawmill.SetActive(true);
            axe.wood -= prices[3];
            isBought[2] = true;
        }
    }
    public IEnumerator SubtitlesInfo(int price, int kindOfPurchase)
    {
        if (axe.wood < price)
            message.text = "You don't have enough resourses!";
        else
        {
            if (kindOfPurchase == 1) message.text = "You need a golden axe for this purchase";
            message.text = "You have purchased the maximum quantity of this item";
        }
        yield return new WaitForSeconds(3f);
        message.text = null;
    }
}
