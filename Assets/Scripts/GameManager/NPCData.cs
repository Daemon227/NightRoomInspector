using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCData
{
    public int id;
    public string npcName;
    public string description;
    public bool isMonster;
    public List<DialogueByDay> dialogueByDay;
}

[System.Serializable]
public class DialogueByDay 
{
    public int day;
    public List<DialogueOption> options;
}

[System.Serializable]
public class DialogueOption
{
    public string playerText;
    public string[] npcTexts;
    public bool isExit = false;
}
