using System.Collections;
using TMPro;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public Animator animator;
    public int wood = 0;
    [SerializeField] public int woodLimit = 10;
    [SerializeField] private ShopController shopInfo;
    [SerializeField] private TextMeshProUGUI subtitles;
    [SerializeField] private TextMeshProUGUI woodInfo;
    [SerializeField] private PlayerController player;
    public int axeMinDamage = 1;
    public int axeMaxDamage = 3;
    void Update()
    {
        if (Camera.main.transform.childCount > 0)
            woodInfo.text = $"Wood: {wood} / {woodLimit}"; 
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Camera.main.transform.childCount > 0 && player.isPaused != 0)
        {
            animator.SetTrigger("isKick");
            if (Physics.Raycast(ray, out hit, 2f) && hit.collider.CompareTag("Tree"))
            {
                StartCoroutine(WaitForAnimation());
                StartCoroutine(SubtitlesInfo());
            }
        }
    }
    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        if (wood < woodLimit)
        {
            wood += Random.Range(axeMinDamage, axeMaxDamage);
            if (wood > woodLimit) wood = woodLimit;
        }
        Debug.Log(wood);
    }
    public IEnumerator SubtitlesInfo()
    {
        if (wood >= woodLimit)
        {
            subtitles.text = "Your inventory is full";
            yield return new WaitForSeconds(3f);
            subtitles.text = null;
        }
    }
}
