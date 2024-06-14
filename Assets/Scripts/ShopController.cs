using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [Header ("Stats")]
    [SerializeField] private PlayerController player;
    [SerializeField] private TextMeshProUGUI appleInfo;
    [SerializeField] private TextMeshProUGUI goldenAppleInfo;
    [SerializeField] private int[] prices = new int[] {10, 100, 10, 30, 10};
    [Header ("UI")]
    [SerializeField] private Button back;
    [SerializeField] private Canvas shopUI;
    [SerializeField] private Canvas interfaceUI;
    [SerializeField] private GameObject lumberjack;
    private int capacity = 0;
    [SerializeField] private int capacityMax = 10;
    private int speed = 0;
    [SerializeField] private int speedMax = 2;
    [Header ("AxeParams")]
    [SerializeField] private GameObject axePrefab;
    [SerializeField] private AxeController axe;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private Material goldMat;
    private MeshRenderer axeRenderer;
    public bool[] isBought = new bool[3];
    private void Start()
    {
        for (int i = 0; i < isBought.Length; isBought[i] = false, i++) ;
        message.text = null;
    }
    private void FixedUpdate()
    {
        appleInfo.text = $"{player.apples}";
        goldenAppleInfo.text = $"{player.goldenApples}";
    }

    public void Back()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        interfaceUI.gameObject.SetActive(true);
        shopUI.gameObject.SetActive(false);
        player.isPaused = 1;
    }
    public void CapacityIncrease()
    {
        if (player.apples >= prices[0] && capacity < capacityMax)
        {
            player.appleLimit += 20;
            player.apples -= prices[0];
            capacity++;
        }
        else StartCoroutine(SubtitlesInfo(prices[0], 0));
    }
    public void Axe()
    {
        if (!isBought[0] && player.apples >= prices[1]) 
        {
            axePrefab = Instantiate(axePrefab, Camera.main.transform);
            player.apples -= prices[1];
            axe.animator = axePrefab.GetComponent<Animator>();
            isBought[0] = true;
        }
        else StartCoroutine(SubtitlesInfo(prices[1], 0));
    }
    public void SpeedX2()
    {
        if (player.goldenApples >= prices[2] && speed < speedMax)
        {
            player.speed *= 2;
            player.goldenApples -= prices[2];
            speed++;
        }
        else StartCoroutine(SubtitlesInfo(prices[2], 1));
    }
    public void GoldenAxe()
    {
        if (Camera.main.transform.childCount > 0 && player.goldenApples >= prices[3] && !isBought[1])
        {
            axe.axeMinDamage += 5;
            axe.axeMaxDamage += 5;
            player.goldenApples -= prices[3];
            isBought[1] = true;
            axeRenderer = axePrefab.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
            axeRenderer.material = goldMat;
        }
        else StartCoroutine(SubtitlesInfo(prices[3], 1));
    }
    public void LamberJackHouse()
    {
        if (axe.wood >= prices[4] && !isBought[2])
        {
            //Debug.Log("Yaayyy");
            axe.wood -= prices[4];
            lumberjack.SetActive(true);
            shopUI.gameObject.SetActive(true);
            isBought[2] = true;
        }
        else StartCoroutine(SubtitlesInfo(prices[4], 2));
    }
    public IEnumerator SubtitlesInfo(int price, int kindOfPurchase)
    {
        if ((player.apples < price && kindOfPurchase == 0) || (player.goldenApples < price && kindOfPurchase == 1) || (axe.wood < price && kindOfPurchase == 2)) message.text = "You don't have enough resourses!";
        else message.text = "You have purchased the maximum quantity of this item";
        yield return new WaitForSeconds(3f);
        message.text = null;
    }
}
