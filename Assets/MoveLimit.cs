using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLimit : MonoBehaviour
{
    //追加　XとYの上限
    public float xmin;
    public float xmax;
    public float zmin;
    public float zmax;



    void Update()
    {
        //追加　現在のポジションを保持する
        Vector3 currentPos = this.transform.position;

        //追加　Mathf.ClampでX,Yの値それぞれが最小～最大の範囲内に収める。
        //範囲を超えていたら範囲内の値を代入する
        currentPos.x = Mathf.Clamp(currentPos.x, xmin, xmax);
        currentPos.z = Mathf.Clamp(currentPos.z, zmin, zmax);

        //追加　positionをcurrentPosにする
        this.transform.position = currentPos;

    }
}
