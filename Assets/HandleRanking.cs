using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleRanking : MonoBehaviour
{
    [SerializeField] Image[] listRankingImage;
    [SerializeField] Sprite[] listSpriteByID;

    public void UpdateList(int[] list)
    {
        for(int i = 0; i< listRankingImage.Length; i++)
        {
            listRankingImage[i].sprite = listSpriteByID[list[i]];
        }
    }

    public void UpdateImageByIndex(int index, Sprite sprite)
    {
        listRankingImage[index].sprite = sprite;
    }

    public Sprite GetSprite(int id)
    {
        return listSpriteByID[id];
    }
}
