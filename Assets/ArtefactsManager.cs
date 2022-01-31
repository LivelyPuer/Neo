using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArtefactsManager : MonoBehaviour
{
    [Serializable]
    public struct Artefact
    {
        public string title;
        public Sprite sprite;
        public Sprite disableSprite;
    }

    public Artefact[] artefacts;
    public Dictionary<Artefact, bool> extArtefacts = new Dictionary<Artefact, bool>();

    void Start()
    {
        DontDestroyOnLoad(this);
        if (ES3.KeyExists("extArtefacts"))
        // ES3.DeleteKey("extArtefacts");
            extArtefacts = ES3.Load<Dictionary<Artefact, bool>>("extArtefacts");
        else
        {
            foreach (Artefact artefact in artefacts)
            {
                extArtefacts.Add(artefact, false);
            }
        }
        FindObjectOfType<ShowArtefacts>().Regenerate();

    }

    public void SetArtefact(Artefact artefact)
    {
        extArtefacts[artefact] = true;
        SaveArtefacts();
    }
    
    public Artefact SetArtefact()
    {
        extArtefacts[GetRandomArtefact()] = true;
        SaveArtefacts();
        return GetRandomArtefact();
    }

    public Artefact GetRandomArtefact()
    {
        return artefacts[Random.Range(0, artefacts.Length)];
    }

    public void SaveArtefacts()
    {
        ES3.Save("extArtefacts", extArtefacts );
    }

}
