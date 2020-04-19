using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.Utils;

class Mole
{
    public Transform mole;
    public enum Mole_CurrentMode { RAISED, RAISING, LOWERED, LOWERING}
    public bool struck = false;
    public Mole_CurrentMode moleMode = Mole_CurrentMode.LOWERED;
    public float animationTime;

    public Mole(Transform transform)
    {
        mole = transform;
    }
}

public class PuzzleManager_Whackamole : PuzzleManager_Base
{
    public bool puzzleActive = false;
    public bool puzzleFailed = false;

    public AnimationCurve raisingMotion;
    public AnimationCurve gameSpeed;
    public float raiseTime = 1f;
    public float holdTime = .5f;
    public float lowerTime = .75f;
    public float spawnTime = .4f;
    float spawnTimer = 0f;
    int molesHit = 0;
    public int molesToWin = 25;
    public List<AxeSimpleButton> moles;
    List<Mole> moleList;
    //public Material VictoryMaterial;
    //public Material NeutralMaterial;
    public Color victoryColor;
    public Color weakColor;
    public Color strongColor;
    public Object explosionEffect;

    private void Awake()
    {
        moleList = new List<Mole>();
        foreach (AxeSimpleButton asb in moles)
        {
            moleList.Add(new Mole(asb.transform));
            asb.transform.localPosition = new Vector3(asb.transform.localPosition.x, -5f, asb.transform.localPosition.z);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Signals.Get<PerformVerbSignal>().AddListener(ReceivedVerb);
        foreach(Mole m in moleList)
            m.mole.GetComponent<MeshRenderer>().material.color = Color.Lerp(weakColor, strongColor, molesHit / (float)molesToWin);
    }

    private void Update()
    {
        if (!puzzleActive)
            return;

        spawnTimer += Time.deltaTime * gameSpeed.Evaluate(molesHit / (float)molesToWin);
        if(spawnTimer > spawnTime)
        {
            Mole m = moleList[Random.Range(0, moleList.Count - 1)];
            if(m.moleMode == Mole.Mole_CurrentMode.LOWERED)
            {
                spawnTimer = 0f;
                m.animationTime = 0f;
                m.moleMode = Mole.Mole_CurrentMode.RAISING;
            }
        }

        foreach (Mole m in moleList)
            MoleUpdate(m, Time.deltaTime * gameSpeed.Evaluate(molesHit / (float)molesToWin));
    }

    void MoleUpdate(Mole m, float time)
    {
        m.animationTime += time;
        switch (m.moleMode)
        {
            case Mole.Mole_CurrentMode.RAISED:
                if(!IsCompleted && m.animationTime >= holdTime)
                {
                    m.moleMode = Mole.Mole_CurrentMode.LOWERING;
                    m.animationTime = 0f;
                }
                break;
            case Mole.Mole_CurrentMode.RAISING:
                float yPos = Mathf.Lerp(-5f, 5f, raisingMotion.Evaluate(m.animationTime / raiseTime));
                m.mole.localPosition = new Vector3(m.mole.localPosition.x, yPos, m.mole.localPosition.z);
                if(m.animationTime >= raiseTime)
                {
                    m.moleMode = Mole.Mole_CurrentMode.RAISED;
                    m.animationTime = 0f;
                }
                break;
            case Mole.Mole_CurrentMode.LOWERED:
                //do nothing
                break;
            case Mole.Mole_CurrentMode.LOWERING:
                yPos = Mathf.Lerp(5f, -5f, raisingMotion.Evaluate(m.animationTime / lowerTime));
                m.mole.localPosition = new Vector3(m.mole.localPosition.x, yPos, m.mole.localPosition.z);
                if (m.animationTime >= lowerTime)
                {
                    m.moleMode = Mole.Mole_CurrentMode.LOWERED;
                    m.animationTime = 0f;
                    //m.mole.GetComponent<MeshRenderer>().material = NeutralMaterial;
                    m.mole.GetComponent<MeshRenderer>().material.color = Color.Lerp(weakColor, strongColor, molesHit / (float) molesToWin);
                    m.struck = false;
                }
                break;
        }
    }

    public void ReceivedVerb(Component source, LifeformManager.EControlVerbs Verb, int data)
    {
        if (!puzzleActive || IsCompleted)
            return;

        if (source.gameObject.transform.IsChildOf(this.transform))
        {
            Mole mole = null; //the mole that was pressed
            foreach(Mole m in moleList)
                if(m.mole == source.transform)
                {
                    mole = m;
                    break;
                }

            if (mole == null || mole.struck == true)
                return;

            molesHit++;
            mole.struck = true;
            foreach (Mole m in moleList)
            {
                if (m.mole.GetComponent<MeshRenderer>().material.color != victoryColor)
                {
                    m.mole.GetComponent<MeshRenderer>().material.color = Color.Lerp(weakColor, strongColor, molesHit / (float)molesToWin);
                }
            }
            mole.mole.GetComponent<MeshRenderer>().material.color = victoryColor;
            GameObject go = (GameObject) Instantiate(explosionEffect, mole.mole.position, transform.rotation);//spawn explosion
            go.transform.localScale = mole.mole.lossyScale;

            if (molesHit >= molesToWin)
            {
                IsCompleted = true;
                Signals.Get<PuzzleComplete>().Dispatch(this);
                foreach (Mole m in moleList)
                {
                    if(m.moleMode != Mole.Mole_CurrentMode.RAISING && m.moleMode != Mole.Mole_CurrentMode.RAISED)
                    {
                        m.animationTime = 0f;
                        m.moleMode = Mole.Mole_CurrentMode.RAISING;
                    }
                    m.mole.GetComponent<MeshRenderer>().material.color = victoryColor;
                }
            }
        }
    }

    private void OnDestroy()
    {
        Signals.Get<PerformVerbSignal>().RemoveListener(ReceivedVerb);
    }
}
