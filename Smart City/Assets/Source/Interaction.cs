using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    //Variables

    //Creating tiles
    public Texture2D[] textureArray; //textures for sprites
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;

    private Sprite sprite;
    private GameObject spriteObject;

    private int posX = 0;
    private int posY = 0;
    //private double posZ = 0.07318394;

    //Generating tiles
    public int nb;

   /// <summary>
   /// Démarrage de la scène
   /// </summary>
    void Start()
    {
        //Grid for tiles
        GameObject gridObject = GameObject.Find("Grid");
        Grid grid = gridObject.GetComponent<Grid>();
        //Number of differents tiles types
        spriteArray = new Sprite[4];
        nb = spriteArray.Length;

        //Load all textures for sprites tiles
        textureArray = Resources.LoadAll<Texture2D>("Textures");

        //Creating new sprites with the textures
        sprite = Sprite.Create(textureArray[0], new Rect(0.0f, 0.0f, textureArray[0].width, textureArray[0].height), new Vector2(0.5f, 0.25f), 32, 1, SpriteMeshType.Tight);
        sprite.name = "Sprite1";
        spriteArray[0] = sprite;

        sprite = Sprite.Create(textureArray[1], new Rect(0.0f, 0.0f, textureArray[1].width, textureArray[1].height), new Vector2(0.5f, 0.25f), 32, 1, SpriteMeshType.Tight);
        sprite.name = "Sprite2";
        spriteArray[1] = sprite;

        sprite = Sprite.Create(textureArray[2], new Rect(0.0f, 0.0f, textureArray[2].width, textureArray[2].height), new Vector2(0.5f, 0.25f), 32, 1, SpriteMeshType.Tight);
        sprite.name = "Sprite3";
        spriteArray[2] = sprite;

        sprite = Sprite.Create(textureArray[3], new Rect(0.0f, 0.0f, textureArray[3].width, textureArray[3].height), new Vector2(0.5f, 0.25f), 32, 1, SpriteMeshType.Tight);
        sprite.name = "Sprite4";
        spriteArray[3] = sprite;

        //Generating random sprite image
        System.Random rnd = new System.Random();

        for(posX = 0; posX < 6; posX++)
        {
            for(posY = 0; posY < 6; posY++)
            {
                //Generating new tiles (GameObject)
                spriteObject = new GameObject("Tile" + posX + ':' + posY);

                //Position (x,y,z)
                spriteObject.transform.parent = gridObject.transform;
                spriteObject.transform.position = grid.CellToWorld(new Vector3Int(posX, posY, 0));
                //Visual aspect
                spriteRenderer = spriteObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
                spriteRenderer.sprite = spriteArray[rnd.Next(0, nb)];
            }
        }

    }

    /// <summary>
    /// Modification de la scène
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Generating random sprite image
            System.Random rnd = new System.Random();

            ChangeAllSprite(spriteArray[rnd.Next(0, nb)]);
        }
    }

    /* 
     * ChangeAllSprite : fonction
     * Paramètre : newSprite : Sprite
     * Permet d'ajouter des sprites
     */
    /// <summary>
    /// Permet d'ajouter des sprites
    /// </summary>
    /// <param name="newSprite"></param>
    void ChangeAllSprite(Sprite newSprite)
    {
        //Grid for tiles
        GameObject gridObject = GameObject.Find("Grid");
        Grid grid = gridObject.GetComponent<Grid>();

        foreach (SpriteRenderer sr in gridObject.GetComponentsInChildren<SpriteRenderer>()) {
            sr.sprite = newSprite;
        }
    }
}
