using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{

    /// <summary>
    /// モンスター名
    /// </summary>
    [SerializeField]
    private string Name;


    /// <summary>
    /// 生命力
    /// </summary>
    [SerializeField]
    private int HP;


    /// <summary>
    /// 攻撃力
    /// </summary>
    [SerializeField]
    private float Attack;


    /// <summary>
    /// 画像
    /// </summary>
    [SerializeField]
    private Texture Texture;


    /// <summary>
    /// 説明
    /// </summary>
    [SerializeField]
    private string Description;

}