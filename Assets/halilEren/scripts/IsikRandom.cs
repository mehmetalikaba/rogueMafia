using UnityEngine;

public class IsikRandom : MonoBehaviour
{
    public GameObject isiklar;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.childCount > 0)
        {
            Transform childTransform = gameObject.transform.GetChild(0);
            isiklar = childTransform.gameObject;

            int i = Random.Range(0, 2);
            if (i == 1)
            {
                isiklar.SetActive(true);
            }
            else
            {
                isiklar.SetActive(false);
            }
        }
        else
            Debug.Log(gameObject.name + " <==> child obje bulunamadi");
    }
}
