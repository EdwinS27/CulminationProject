using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EzrealMysticShot : Ezreal    {
    int stacks;
    float stackDuration;
    float bonusAttackSpeed;
    float originalAttackSpeed;
    Stats statScript;
    void Start()    {
        statScript = GetComponent<Stats>();
    }
     
    public T GetTargetDestination<T>(T param)  {
        return param;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseAbility()    {

    }
}
