using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public enum isEnemy //�Ʊ� 0, �� 1, �߸� 2�� ��Ÿ��. �������׵� �̷� ������ ��� �����ҵ�
    {
        Ally, //�Ʊ��� ����,0
        Enemy, //���� ����,1
        neutral //�ǾƱ��� x,2
    }
    public enum AttackType
    {
        Physical, //0
        Spiritual, //1
        Physiological //2
    }
    public isEnemy Is_Enemy_Attack { get; set; } //����ü�� �ٷ� ������Ƽ�� �� �ؼ� ����ü ������ ���� �ϳ��� ���� {get; set;} �־���
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
    void OnDestroy() //���� Ȯ�� �ÿ��� �ı��ϴ� �� ���� �ٸ� ó���� �ؾ��ҵ��ϴ�. �ı� ������ �� ó���Ϸ��ϱ� �� �ȵȴ�...
    {
        Hit = true;
    }
}
