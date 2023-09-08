using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int money;
    // 动画组件
    public Animator ani;

    // 刚体组件
    public Rigidbody2D rBody;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 获取水品轴 -1 0 1
        float horizontal = Input.GetAxisRaw("Horizontal");

        // 获取垂直轴
        float vertical = Input.GetAxisRaw("Vertical");
        // 这套代码 好像也能八方向移动 只是没有斜着走的动画 
        
        // 按下左或者右
        if(horizontal != 0) {
            ani.SetFloat("Horizontal", horizontal);
            // 只支持四方向 不支持八方向移动 不仅和代码有关 也和动画有关
            ani.SetFloat("Vertical", 0);
        }

        // 按下上或者下
        if(vertical != 0) {
            ani.SetFloat ("Vertical", vertical);
            ani.SetFloat ("Horizontal", 0);
        }

        // 切换跑步状态
        Vector2 dir = new Vector2(horizontal, vertical);
        ani.SetFloat("Speed", dir.magnitude);

        // 朝该方向移动
        rBody.velocity = dir * 0.51f;
    }

    public void getMoney()
    {
        money++;
        Debug.Log("Player get money");
    }
}
