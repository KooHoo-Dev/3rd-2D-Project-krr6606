using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CatSlotSpriteDataManager : MonoBehaviour
{
    Dictionary<CatName, Sprite> catSlotIngameSpriteDict = new Dictionary<CatName, Sprite>();
    Dictionary<CatName, Sprite> catSlotSpriteDict = new Dictionary<CatName, Sprite>();
    public static CatSlotSpriteDataManager Instance { get; private set; }
    public Sprite[] CatSlotIngameSpriteArray;
    public Sprite[] CatSlotImage;
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
        for (int i = 0; i < CatSlotIngameSpriteArray.Length; i++)
        {
            CatName catName = (CatName)i;
            AddCatSprite(catName, CatSlotIngameSpriteArray[i]);
            AddCatImage(catName, CatSlotImage[i]);
        }
    }

    public void AddCatSprite(CatName catName, Sprite sprite)
    {
        if (!catSlotIngameSpriteDict.ContainsKey(catName))
        {
            catSlotIngameSpriteDict.Add(catName, sprite);
        }
    }
    public void AddCatImage(CatName catName, Sprite image)
    {
        if (!catSlotSpriteDict.ContainsKey(catName))
        {
            
            catSlotSpriteDict.Add(catName, image);
        }
    }
    public Sprite GetCatSpriteOrNull(CatName catName)
    {
        if (catSlotIngameSpriteDict.TryGetValue(catName, out Sprite sprite))
        {
            return sprite;
        }
        return null;
    }
    public Sprite GetImageOrNull(CatName catName)
    {
        if (catSlotSpriteDict.TryGetValue(catName, out Sprite sprite))
        {
            return sprite;
        }
        return null;
    }
}

