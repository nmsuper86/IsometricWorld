using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricSpriteControl : MonoBehaviour
{
    private bool isEnter;   //判断是否进入上层
    private SpriteRenderer spriteRenderer; 
    public GameObject col1;     //进入上层后的碰撞器
    public GameObject col2;     //离开上层后的碰撞器
    public bool isExitUp = false;      //判断人物是否离开upCol碰撞器

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); 
    }


    void Update()
    {
        if (isEnter)                //进入上层后，使IsometricSprite脚本未激活，手动使人物图层顺序调高，激活相应的碰撞器
        {
            gameObject.GetComponent<IsometricSprite>().enabled = false;
            spriteRenderer.sortingOrder = 100;
            col1.SetActive(true);
            col2.SetActive(false);
        }
        else                       //离开上层后，使IsometricSprite脚本激活，激活相应的碰撞器
        {
            gameObject.GetComponent<IsometricSprite>().enabled = true;
            col1.SetActive(false);
            col2.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "up")           //进入上层
        {
            isEnter = true;
        }

        if (isExitUp)                               //人物离开upCol碰撞器后
        {
            if (collision.gameObject.tag == "in")       //人物还在上层
            {
                isEnter = true;
                isExitUp = false;
            }
            else if (collision.gameObject.tag == "out")   //人物离开上层
            {
                isEnter = false;
                isExitUp = false;
            }


        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isExitUp)                               //人物离开upCol碰撞器后
        {
            if (collision.gameObject.tag == "in")       //人物还在上层
            {
                isEnter = true;
                isExitUp = false;
            }
            else if (collision.gameObject.tag == "out")   //人物离开上层
            {
                isEnter = false;
                isExitUp = false;
            }


        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "up")   //人物离开upCol碰撞器
        {
            isExitUp = true;
        }

    }



}
