using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Child : MonoBehaviour
{
    [Header("Kid SpriteRenderer")]
    public SpriteRenderer childHair;
    public SpriteRenderer childShirt;
    public SpriteRenderer childFace;
    public SpriteRenderer childBangs;
    public SpriteRenderer childEyes;
    public SpriteRenderer childBrows;
    public SpriteRenderer childMouth;
    
    [Header("Kid Sprites List")]
    public Sprite[] childHairSprites;
    public Sprite[] childShirtSprites;
    public Sprite[] childFaceSprites;
    public Sprite[] childBangsSprites;
    public Sprite[] childEyesSprites;
    public Sprite[] childBrowsSprites;
    public Sprite[] childMouthSprites;

    private void Start()
    {
        AssignTextures();
    }

    private void AssignTextures()
    {
        childHair.sprite = childHairSprites[Random.Range(0, childHairSprites.Length)];
        childShirt.sprite = childShirtSprites[Random.Range(0, childShirtSprites.Length)];
        childFace.sprite = childFaceSprites[Random.Range(0, childFaceSprites.Length)];
        childBangs.sprite = childBangsSprites[Random.Range(0, childBangsSprites.Length)];
        childEyes.sprite = childEyesSprites[Random.Range(0, childEyesSprites.Length)];
        childBrows.sprite = childBrowsSprites[Random.Range(0, childBrowsSprites.Length)];
        childMouth.sprite = childMouthSprites[Random.Range(0, childMouthSprites.Length)];
        Color hairColor = Random.ColorHSV(0,1,0.3f,0.7f,0.5f,1,1,1);
        childHair.color = hairColor;
        childBangs.color = hairColor;
    }
}
