using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSkillTest : MonoBehaviour
{
    public bool staticSkill;
    public GameObject skill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.Q))
        {
            if(staticSkill)
            {
                Instantiate(skill, transform.position, transform.rotation, transform.transform);

            }
            else
            {
                Instantiate(skill, transform.position, transform.rotation);

            }
        }
    }
}
