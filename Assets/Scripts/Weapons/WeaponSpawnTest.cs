using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnTest : MonoBehaviour
{
    public SMG origSMGScriptableObject;
    private SMG spawnedSMG;

    private void Start()
    {
        spawnedSMG = Instantiate(origSMGScriptableObject);
        spawnedSMG.CreateSMG();
        //spawnedSMG.RarityTest();
    }
}
