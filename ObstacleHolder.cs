using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHolder : MonoBehaviour
{
    public GameObject[] childs;

    public float limitAxisX;

    public Vector3
        firstPos,
        secondPos;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-GameplayController.instance.moveSpeed * Time.deltaTime,0f,0f);

        if(transform.localPosition.x <= limitAxisX)
        {
            //obstacle'lar belli bir x limitini aştığında - yani player'ın çok gerisinde kaldıklarında- silinecekler

            //inform Gameplay controller that the obstacle is not active
            GameplayController.instance.obstacles_Is_Active = false;
            gameObject.SetActive(false);
        }

    }

    void OnEnable()
    {
        for(int i = 0 ; i < childs.Length; i++)
        {
            childs[i].SetActive(true);
        }

        if(Random.value <= 0.5f)
        {
            transform.localPosition = firstPos;
        }
        else
        {
            transform.localPosition = secondPos;
        }
    }

}//class


/*  void Awake() -> called first when the game starts
 *  void Start() -> called third
 *  void OnEnable() -> called second when the game starts. But also called everytime 
 *  the game object this script is attached is enabled for the game
 */
















