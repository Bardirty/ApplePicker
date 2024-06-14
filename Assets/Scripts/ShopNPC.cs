using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopNPC : MonoBehaviour, NPCInteraction
{
    [SerializeField] private Canvas shopUI;
    [SerializeField] private Canvas interfaceUI;
    [SerializeField] private Transform NPCHead;
    [SerializeField] private Animator animator;
    [SerializeField] private int typeOfSeller = 0;
    [SerializeField] private PlayerController player;
    [SerializeField] private TextMeshProUGUI subtitles;
    [SerializeField] private GameObject choiceUI;
    [SerializeField] private Image fade;
    [SerializeField] private Canvas credits;
    private bool interacted = false;
    private string[] phrasesLumber = null;
    private void Start()
    {
        phrasesLumber = new string[] { "I can build everything you desire. Your dreams, my blueprint!",
            "Hello, stranger, woodworker at your service! Tell me your dream, and I'll make it happen!",
            "From pines to oak, I've got the wood for your needs" };
    }
    public string GetDesccription()
    {
        return "Press to talk";
    }

    public void Interact()
    {
        if(typeOfSeller == 0) subtitles.text = "Apple Hunter: Wanna buy something?";
        else if(typeOfSeller == 1) subtitles.text = "Jack: " + phrasesLumber[Random.Range(0, phrasesLumber.Length)];
        else if(typeOfSeller == 2)
        {
            StartCoroutine(FinalDialogue(3f, false));
            interacted = true;
            return;
        }
        interacted = true;
        StartCoroutine(CallFunctionAfterDelay(3f));
    }
    public void Update()
    {
        if (interacted)
        {
            Vector3 direction = Camera.main.transform.position - NPCHead.position;
            NPCHead.rotation = Quaternion.Lerp(NPCHead.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 3f);
        }
    }
    public void Yes()
    {
        subtitles.text = "";
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        choiceUI.SetActive(false);
        if (player.goldenApples >= 50 && player.apples >= 50)
        {
            StartCoroutine(Fade());
            return;
        }
        else StartCoroutine(FinalDialogue(3f, true));
        player.isPaused = 1;
    }
    public void No()
    {
        subtitles.text = "";
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.isPaused = 1;
        choiceUI.SetActive(false);
    }
    IEnumerator FinalDialogue(float delay, bool isPositiveButNoApples)
    {
        if (!isPositiveButNoApples)
        {
            subtitles.text = "Iselda: Hello, stranger! Would you like to stay here?";
            yield return new WaitForSeconds(delay);
            subtitles.text = " It will cost 50 golden apples and 50 red apples. Do you agree?";
            choiceUI.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            player.isPaused = 0;
        }
        else subtitles.text = "Iselda: You do not have enough apples. Please come back later!";
        yield return new WaitForSeconds(delay);
        subtitles.text = "";
    }
    IEnumerator Fade()
    {
        Color color = fade.color;
        while(color.a < 1f)
        {
            color.a += 1f * Time.deltaTime;
            fade.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        credits.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
    IEnumerator CallFunctionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        NPCHead.rotation = Quaternion.LookRotation(Vector3.zero);
        interacted = false;
        interfaceUI.gameObject.SetActive(false);
        shopUI.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        subtitles.text = "";
        player.isPaused = 0;
    }
}
