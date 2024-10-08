using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //static variables are accesible. instance is an object from the class. with the class name we can access all the public variables
    public static MapGenerator instance;

    public GameObject
        roadPrefab,
        grassPrefab,
        groundPrefab_1,
        groundPrefab_2,
        groundPrefab_3,
        groundPrefab_4,
        grass_Bottom_Prefab,
        land_Prefab_1,
        land_Prefab_2,
        land_Prefab_3,
        land_Prefab_4,
        land_Prefab_5,
        big_Grass_Prefab,
        big_Grass_Bottom_Prefab,
        treePrefab_1,
        treePrefab_2,
        treePrefab_3,
        big_Tree_Prefab;

    public GameObject //for parent objects that are going to hold the tiles
        roadHolder,
        top_Near_Side_Walk_Holder,
        top_Far_Side_Walk_Holder,
        bottom_Near_Side_Walk_Holder,
        bottom_Far_Side_Walk_Holder;

    public int
        num_start_Road_Tile, //initialization number of "road" tiles
        num_start_Grass_Tile, //initialization number of "grass" tiles
        num_start_Ground3_Tile, //initialization number of "ground3" tiles
        num_start_Land_Tile; //initialization number of "land" tiles

    //lists are resizable, arrays are not resizable
    public List<GameObject>
        road_Tiles,
        top_Near_Grass_Tiles,
        top_Far_Grass_Tiles,
        bottom_Near_Grass_Tiles,
        bottom_Far_Land_F1_Tiles,
        bottom_Far_Land_F2_Tiles,
        bottom_Far_Land_F3_Tiles,
        bottom_Far_Land_F4_Tiles,
        bottom_Far_Land_F5_Tiles;

    //positions for ground1 prefab on top from 0 to startGround3Tile prefab
    public int[] pos_For_Top_Ground_1;

    //positions for ground2 prefab on top from 0 to startGround3Tile prefab
    public int[] pos_For_Top_Ground_2;

    //positions for ground4 prefab on top from 0 to startGround3Tile prefab
    public int[] pos_For_Top_Ground_4;

    //positions for big grass with tree on top near grass from 0 to startGrassTile prefab
    public int[] pos_For_Top_Big_Grass;

    //positions for tree1 on top near grass from 0 to startGrassTile prefab
    public int[] pos_For_Top_Tree_1;

    //positions for tree2 on top near grass from 0 to startGrassTile prefab
    public int[] pos_For_Top_Tree_2;

    //positions for tree3 on top near grass from 0 to startGrassTile prefab
    public int[] pos_For_Top_Tree_3;

    //positions for road tile on road from 0 to startRoadTile prefab
    public int pos_For_Road_Tile_1;

    //positions for road tile on road from 0 to startRoadTile prefab
    public int pos_For_Road_Tile_2;

    //positions for road tile on road from 0 to startRoadTile prefab
    public int pos_For_Road_Tile_3;

    //positions for big grass with tree on bottom near grass from 0 to startGrassTile prefab
    public int[] pos_For_Bottom_Big_Grass;

    //positions for tree1 on bottom near grass from 0 to startGrassTile prefab
    public int[] pos_For_Bottom_Tree_1;

    //positions for tree2 on bottom near grass from 0 to startGrassTile prefab
    public int[] pos_For_Bottom_Tree_2;

    //positions for tree3 on bottom near grass from 0 to startGrassTile prefab
    public int[] pos_For_Bottom_Tree_3;

    [HideInInspector]
    public Vector3
        last_Pos_Of_Road_Tile,
        last_Pos_Of_Top_Near_Grass,
        last_Pos_Of_Top_Far_Grass,
        last_Pos_Of_Bottom_Near_Grass,
        last_Pos_Of_Bottom_Far_Land_F1,
        last_Pos_Of_Bottom_Far_Land_F2,
        last_Pos_Of_Bottom_Far_Land_F3,
        last_Pos_Of_Bottom_Far_Land_F4,
        last_Pos_Of_Bottom_Far_Land_F5;

    [HideInInspector]
    public int
        last_Order_Of_Road_Tile,
        last_Order_Of_Top_Near_Grass,
        last_Order_Of_Top_Far_Grass,
        last_Order_Of_Bottom_Near_Grass,
        last_Order_Of_Bottom_Far_Land_F1,
        last_Order_Of_Bottom_Far_Land_F2,
        last_Order_Of_Bottom_Far_Land_F3,
        last_Order_Of_Bottom_Far_Land_F4,
        last_Order_Of_Bottom_Far_Land_F5;

    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        Initialize();
    }

    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        } else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    void Initialize()
    {
        InitializePlatform(roadPrefab, ref last_Pos_Of_Road_Tile, roadPrefab.transform.position,
            num_start_Road_Tile, roadHolder, ref road_Tiles, ref last_Order_Of_Road_Tile, new Vector3(1.5f,0f,0f));

        InitializePlatform(grassPrefab, ref last_Pos_Of_Top_Near_Grass, grassPrefab.transform.position,
            num_start_Grass_Tile, top_Near_Side_Walk_Holder, ref top_Near_Grass_Tiles, ref last_Order_Of_Top_Near_Grass,
            new Vector3(1.2f,0f,0f));

        InitializePlatform(groundPrefab_3, ref last_Pos_Of_Top_Far_Grass, groundPrefab_3.transform.position,
                num_start_Ground3_Tile, top_Far_Side_Walk_Holder, ref top_Far_Grass_Tiles, ref last_Order_Of_Top_Far_Grass,
                new Vector3(4.8f, 0f, 0f));

        InitializePlatform(grass_Bottom_Prefab, ref last_Pos_Of_Bottom_Near_Grass, new Vector3(2.0f, grass_Bottom_Prefab.transform.position.y,0f),
        num_start_Grass_Tile, bottom_Near_Side_Walk_Holder, ref bottom_Near_Grass_Tiles, ref last_Order_Of_Bottom_Near_Grass,
        new Vector3(1.2f, 0f, 0f));

        InitializeBottomFarLand();

    }


    void InitializePlatform(GameObject prefab, ref Vector3 last_Pos, Vector3 last_Pos_Of_Tile,
        int amountTile, GameObject holder,ref List<GameObject> list_Tiles, ref int lastOrder, Vector3 offset)
    //Vector'ın olduğu pozisyonu reference yapıyoruz ki sürekli yeni ve yeni pozisyonları içinde saklayalım
    {
        int orderInLayer = 0;
        last_Pos = last_Pos_Of_Tile;

        for (int i = 0; i < amountTile; i++)
        {
            GameObject clone = Instantiate(prefab, last_Pos, prefab.transform.rotation) as GameObject;
            clone.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;

            if (clone.tag == MyTags.TOP_NEAR_GRASS)
            {

                SetNearScene(big_Grass_Prefab, ref clone, ref orderInLayer, pos_For_Top_Big_Grass,
                    pos_For_Top_Tree_1, pos_For_Top_Tree_2, pos_For_Top_Tree_3);

            }
            else if (clone.tag == MyTags.BOTTOM_NEAR_GRASS)
            {
                SetNearScene(big_Grass_Bottom_Prefab, ref clone, ref orderInLayer, pos_For_Bottom_Big_Grass,
                    pos_For_Bottom_Tree_1, pos_For_Bottom_Tree_2, pos_For_Bottom_Tree_3);
            }
            else if (clone.tag == MyTags.BOTTOM_FAR_LAND_2)
            {
                if(orderInLayer == 5)
                {
                    CreateTreeOrGround(big_Tree_Prefab, ref clone, new Vector3(-0.57f, -1.34f, 0f));
                }
            }
            else if (clone.tag == MyTags.TOP_FAR_GRASS)
            {
                CreateGround(ref clone, ref orderInLayer);
            }

            clone.transform.SetParent(holder.transform);
            list_Tiles.Add(clone);

            orderInLayer +=1 ;
            lastOrder = orderInLayer;

            last_Pos += offset;

        } //***FOR LOOP***

    }// **InitializePlatform Function End**

    void CreateScene(GameObject bigGrassPrefab, ref GameObject tileClone, int orderInLayer)
    {

        GameObject clone = Instantiate(big_Grass_Prefab, tileClone.transform.position,
            big_Grass_Prefab.transform.rotation) as GameObject;

        clone.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
        clone.transform.SetParent(tileClone.transform); //make a child of tileClone
        clone.transform.localPosition = new Vector3(-0.183f,0.106f,0f); //local Pos relative to the parent

        //turn of parent tile to show child tile
        tileClone.GetComponent<SpriteRenderer>().enabled = false;

        CreateTreeOrGround(treePrefab_1, ref clone, new Vector3(0f,1.52f,0f));

    } // **createScene Function End**

    void CreateTreeOrGround(GameObject prefab, ref GameObject tileClone, Vector3 localPos)
    {

        GameObject clone = Instantiate(prefab, tileClone.transform.position,
            prefab.transform.rotation) as GameObject;

        SpriteRenderer tileCloneRenderer = tileClone.GetComponent<SpriteRenderer>();
        SpriteRenderer cloneRenderer = clone.GetComponent<SpriteRenderer>();

        cloneRenderer.sortingOrder = tileCloneRenderer.sortingOrder;
        clone.transform.SetParent(tileClone.transform);
        clone.transform.localPosition = localPos;

        //hide parent & show child
        if(prefab == groundPrefab_1 || prefab == groundPrefab_2 || prefab == groundPrefab_4)
        {
            tileCloneRenderer.enabled = false;
        }


    } //  **CreateTreeOrGround func end**

    void CreateGround(ref GameObject clone, ref int orderInLayer)
    {

        for(int i = 0; i < pos_For_Top_Ground_1.Length; i++)
        {
            if(orderInLayer == pos_For_Top_Ground_1[i])
            {
                CreateTreeOrGround(groundPrefab_1, ref clone, Vector3.zero);
                break;
            }

        }

        for (int i = 0; i < pos_For_Top_Ground_2.Length; i++)
        {
            if (orderInLayer == pos_For_Top_Ground_2[i])
            {
                CreateTreeOrGround(groundPrefab_2, ref clone, Vector3.zero);
                break;
            }

        }

        for (int i = 0; i < pos_For_Top_Ground_4.Length; i++)
        {
            if (orderInLayer == pos_For_Top_Ground_4[i])
            {
                CreateTreeOrGround(groundPrefab_4, ref clone, Vector3.zero);
                break;
            }

        }

    } //  **CreateGround func end**

    void SetNearScene(GameObject bigGrassPrefab, ref GameObject clone, ref int orderInLayer,
        int[] pos_for_BigGrass, int[] pos_for_Tree1, int[] pos_for_Tree2, int[] pos_for_Tree3)
    {
        for(int i=0; i < pos_for_BigGrass.Length; i++)
        {
            if(orderInLayer == pos_for_BigGrass[i])
            {
                CreateScene(bigGrassPrefab, ref clone, orderInLayer);
                break;
            }
        }

        for (int i = 0; i < pos_for_Tree1.Length; i++)
        {
            if (orderInLayer == pos_for_Tree1[i])
            {
                CreateTreeOrGround( treePrefab_1, ref clone, new Vector3(0f,1.15f,0f));
                break;
            }
        }

        for (int i = 0; i < pos_for_Tree2.Length; i++)
        {
            if (orderInLayer == pos_for_Tree2[i])
            {
                CreateTreeOrGround(treePrefab_2, ref clone, new Vector3(0f, 1.15f, 0f));
                break;
            }
        }

        for (int i = 0; i < pos_for_Tree3.Length; i++)
        {
            if (orderInLayer == pos_for_Tree3[i])
            {
                CreateTreeOrGround(treePrefab_3, ref clone, new Vector3(0f, 1.15f, 0f));
                break;
            }
        }

    } //   **SetNearScene func end**

    void InitializeBottomFarLand()
    {

        InitializePlatform(land_Prefab_1, ref last_Pos_Of_Bottom_Far_Land_F1, land_Prefab_1.transform.position,
            num_start_Land_Tile, bottom_Far_Side_Walk_Holder, ref bottom_Far_Land_F1_Tiles,
            ref last_Order_Of_Bottom_Far_Land_F1, new Vector3(1.6f,0f,0f));

        InitializePlatform(land_Prefab_2, ref last_Pos_Of_Bottom_Far_Land_F2, land_Prefab_2.transform.position,
            (num_start_Land_Tile - 3), bottom_Far_Side_Walk_Holder, ref bottom_Far_Land_F2_Tiles,
            ref last_Order_Of_Bottom_Far_Land_F2, new Vector3(1.6f, 0f, 0f));

        InitializePlatform(land_Prefab_3, ref last_Pos_Of_Bottom_Far_Land_F3, land_Prefab_3.transform.position,
            (num_start_Land_Tile - 4), bottom_Far_Side_Walk_Holder, ref bottom_Far_Land_F3_Tiles,
            ref last_Order_Of_Bottom_Far_Land_F3, new Vector3(1.6f, 0f, 0f));

        InitializePlatform(land_Prefab_4, ref last_Pos_Of_Bottom_Far_Land_F4, land_Prefab_4.transform.position,
            (num_start_Land_Tile - 7), bottom_Far_Side_Walk_Holder, ref bottom_Far_Land_F4_Tiles,
             ref last_Order_Of_Bottom_Far_Land_F4, new Vector3(1.6f, 0f, 0f));

        InitializePlatform(land_Prefab_5, ref last_Pos_Of_Bottom_Far_Land_F5, land_Prefab_5.transform.position,
             (num_start_Land_Tile - 10), bottom_Far_Side_Walk_Holder, ref bottom_Far_Land_F5_Tiles,
              ref last_Order_Of_Bottom_Far_Land_F5, new Vector3(1.6f, 0f, 0f));

    } // **InitializeBottomFarLand end**

} //class































