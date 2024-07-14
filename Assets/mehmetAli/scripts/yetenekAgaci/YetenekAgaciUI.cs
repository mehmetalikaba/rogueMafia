using UnityEngine;
using UnityEngine.UI;

public class YetenekAgaciUI : MonoBehaviour
{
    public YetenekAgaci yetenekAgaci;
    public Transform yakinYetenekUIContainer;
    public Transform uzakYetenekUIContainer;
    public Transform ozelGucYetenekUIContainer;
    public GameObject yetenekUIPrefab;

    private void Start()
    {
        foreach (var yetenek in yetenekAgaci.yakinSaldiriYetenekleri)
        {
            var yetenekUI = Instantiate(yetenekUIPrefab, yakinYetenekUIContainer);
            var yetenekUIComponent = yetenekUI.GetComponent<YetenekUI>();
            yetenekUIComponent.Setup(yetenek);
        }

        foreach (var yetenek in yetenekAgaci.uzakSaldiriYetenekleri)
        {
            var yetenekUI = Instantiate(yetenekUIPrefab, uzakYetenekUIContainer);
            var yetenekUIComponent = yetenekUI.GetComponent<YetenekUI>();
            yetenekUIComponent.Setup(yetenek);
        }

        foreach (var yetenek in yetenekAgaci.ozelGucYetenekleri)
        {
            var yetenekUI = Instantiate(yetenekUIPrefab, ozelGucYetenekUIContainer);
            var yetenekUIComponent = yetenekUI.GetComponent<YetenekUI>();
            yetenekUIComponent.Setup(yetenek);
        }
    }
}
