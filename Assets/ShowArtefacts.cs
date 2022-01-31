using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowArtefacts : MonoBehaviour
{
    private ArtefactsManager _artefactsManager;
    public Transform parent;
    public GameObject instantiateObj;
    void Start()
    {
        _artefactsManager = FindObjectOfType<ArtefactsManager>();
    }
    
    // Update is called once per frame
    public void Regenerate()
    {
        foreach (GameObject v in parent)
        {
            Destroy(v);
        }
        foreach (KeyValuePair<ArtefactsManager.Artefact, bool> kv in _artefactsManager.extArtefacts)
        {
            if (!kv.Value)
            {
                GameObject tmp_g = Instantiate(instantiateObj, Vector3.zero, Quaternion.identity, parent);
                tmp_g.GetComponent<Image>().sprite = kv.Key.disableSprite;
            }
            else
            {
                GameObject tmp_g = Instantiate(instantiateObj, Vector3.zero, Quaternion.identity, parent);
                tmp_g.GetComponent<Image>().sprite = kv.Key.sprite;
            }
        }
    }
}
