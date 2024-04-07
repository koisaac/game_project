using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public enum isEnemy //아군 0, 적 1, 중립 2로 나타냄. 유닛한테도 이런 식으로 사용 가능할듯
    {
        Ally, //아군의 공격,0
        Enemy, //적의 공격,1
        neutral //피아구분 x,2
    }
    public enum AttackType
    {
        Physical, //0
        Spiritual, //1
        Physiological //2
    }
    public isEnemy Is_Enemy_Attack { get; set; } //구조체는 바로 프로퍼티로 못 해서 구조체 형식의 변수 하나를 만들어서 {get; set;} 넣어줌
    public AttackType Attack_Type { get; set; }
    public int deal { get; set; }
    public double Critical_Chance { get; set; }
    public List<Effect> Attack_Effect { get; set; }

    public Damage(isEnemy IEA, AttackType AT, int D, double CC, List<Effect> AE)
    {
        Is_Enemy_Attack = IEA;
        Attack_Type = AT;
        deal = D;
        Critical_Chance = CC;
        Attack_Effect = AE;
    }
}

public class Attack_damager : MonoBehaviour
{
    public bool Hit;
    List<Effect> effects;
    public Damage tile_attack { get; set; }
    void Start()
    {
        tile_attack = new Damage(0, 0, 0, 0, effects);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDestroy() //공격 확정 시에는 파괴하는 거 말고 다른 처리를 해야할듯하다. 파괴 직전에 뭘 처리하려니까 잘 안된다...
    {
        Hit = true;
    }
}
