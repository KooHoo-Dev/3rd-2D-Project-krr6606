using System.Collections.Generic;
using UnityEngine;
public class CatSlotSpriteDataManager : MonoBehaviour
{
    Dictionary<CatName, Sprite> catSlotSpriteDict = new Dictionary<CatName, Sprite>();
    public static CatSlotSpriteDataManager Instance { get; private set; }
    public Sprite[] CatSlotSpriteArray;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        for (int i = 0; i < CatSlotSpriteArray.Length; i++)
        {
            CatName catName = (CatName)i;
            AddCatSprite(catName, CatSlotSpriteArray[i]);
        }
    }

    public void AddCatSprite(CatName catName, Sprite sprite)
    {
        if (!catSlotSpriteDict.ContainsKey(catName))
        {
            catSlotSpriteDict.Add(catName, sprite);
        }
    }
    public Sprite GetCatSpriteOrNull(CatName catName)
    {
        if (catSlotSpriteDict.TryGetValue(catName, out Sprite sprite))
        {
            return sprite;
        }
        return null;
    }
}

