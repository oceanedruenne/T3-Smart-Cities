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

    private double posX = 0;
    private double posY = 0;
    private double posZ = 0.07318394;

    //Generating tiles
    public int nb = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Grid for tiles
        GameObject GO = GameObject.Find("Grid");
        Grid grid = GO.GetComponent<Grid>();
        //Number of differents tiles types
        spriteArray = new Sprite[4];

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

        //Generating new tiles (GameObject)
        GameObject spriteObject = new GameObject("Test");

        //Position (x,y,z)
        spriteObject.transform.position = grid.CellToWorld(new Vector3Int());
            //new Vector3((float)posX, (float)posY, (float)posZ);
        //Visual aspect
        spriteRenderer = spriteObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        spriteRenderer.sprite = spriteArray[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (nb >= 4)
            {
                nb = 0;
            }
            ChangeSprite(spriteArray[nb]);
            nb += 1;
        }
    }

    void ChangeSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite;
    }
}
