using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurnBaseManager : MonoBehaviour
{
    /// <summary>
    /// 进攻方
    /// </summary>
    private List<GameObject> _attackers;
    /// <summary>
    /// 防守方
    /// </summary>
    private List<GameObject> _defensers;
    
    //所有参战单位
    private List<GameObject> _allBattleUnits;
    /// <summary>
    /// 本回合内，战斗单位行动顺序
    /// </summary>
    public List<GameObject> _unitActionOrders;

    public void Awake()
    {
        InitFight();
    }

    public void InitFight()
    {

        //创建地格 地格也可能会有buff
        // 创建瓦片
        for (int i = 0; i < 4; i++)
        {
            GameObject  tileUnit= CreateCharacter(
                "Tile",GameObject.Find("AttackerParent/Pos"+i) ,1, Vector3.zero, new ChaProperty(100, Random.Range(5000,7000), 600, Random.Range(50,70)), 0,""
            );
            
            GameObject  tileUnit1= CreateCharacter(
                "Tile",GameObject.Find("DefenderParent/Pos"+i) ,1, Vector3.zero, new ChaProperty(100, Random.Range(5000,7000), 600, Random.Range(50,70)), 0,""
            );
        }
        
        _allBattleUnits = new List<GameObject>();
        _attackers = new List<GameObject>();
        
        //创建攻击方
        for (int i = 0; i < 4; i++)
        {
           GameObject  unit= CreateCharacter(
                "Human",GameObject.Find("AttackerParent/Pos"+i) ,0, Vector3.zero, new ChaProperty(100, Random.Range(5000,7000), 600, Random.Range(50,70)), 0
            );
           //初始化角色技能
           unit.GetComponent<ChaState>().LearnSkill(DesingerTables.Skill.data["fire"]);
            _attackers.Add(unit);
            _allBattleUnits.Add(unit);
        }

        _defensers = new List<GameObject>();
        //创建防守方
        for (int i = 0; i < 4; i++)
        {
            GameObject  unit= CreateCharacter(
                "Human",GameObject.Find("DefenderParent/Pos"+i) ,1,  Vector3.zero, new ChaProperty(100, Random.Range(5000,7000), 600, Random.Range(50,70)), 180
            );
            unit.GetComponent<ChaState>().LearnSkill(DesingerTables.Skill.data["fire"]);
            _defensers.Add(unit);
            _allBattleUnits.Add(unit);
        }

        //初始化上场角色Buff
        for (int i = 0; i < _allBattleUnits.Count; i++)
        {
            GameObject unit = _allBattleUnits[i];
            unit.GetComponent<ChaState>().AddBuff(new AddBuffInfo(DesingerTables.Buff.data["AutoCheckReload"], unit, null, 1, 10, true));
        }
        
        //初始化地格buff
        
        //根据速度初始化所有单位的行动顺序
        _InitRoundActionOrders();
        //通知表现层初始化角色
        
    }

    //初始化出手顺序
    private void _InitRoundActionOrders()
    {
        //所有参与的单位，value=速度
        List<KeyValuePair<GameObject, int>> allActionUnits = new List<KeyValuePair<GameObject, int>>();
        // 参战英雄
        foreach (GameObject unit in _allBattleUnits)
        {
            ChaProperty chaProperty = unit.GetComponent<ChaState>().property;
            int speed = chaProperty.speed;
            allActionUnits.Add(new KeyValuePair<GameObject, int>(unit, speed));
        }
        allActionUnits.Sort((a, b) => a.Value.CompareTo(b.Value));
        
        _unitActionOrders.Clear();
        for (int i = 0; i < allActionUnits.Count; i++)
        {
            _unitActionOrders.Add(allActionUnits[i].Key);
        }
    }
    
    private GameObject CreateFromPrefab(string prefabPath,GameObject parent, Vector3 position = new Vector3(), float rotation = 0.00f){
        GameObject go = Instantiate<GameObject>(
            Resources.Load<GameObject>("Prefabs/" + prefabPath),
            position,
            Quaternion.identity
        );
        if (rotation != 0){
            go.transform.Rotate(new Vector3(0, rotation, 0));
        }
        go.transform.SetParent(parent.transform);
        return go;
    }
    
    ///<summary>
    ///创建一个角色到场上
    ///<param name="prefab">特效的prefab文件夹，约定就在Prefabs/Character/下，所以路径不应该加这段</param>
    ///<param name="unitAnimInfo">角色的动画信息</param>
    ///<param name="side">所属阵营</param>
    ///<param name="pos">创建的位置</param>
    ///<param name="degree">角度</param>
    ///<param name="baseProp">初期的基础属性</param>
    ///<param name="tags">角色的标签，分类角色用的</param>
    ///</summary>
    public GameObject CreateCharacter(string prefab ,GameObject parent, int side, Vector3 pos, ChaProperty baseProp, float degree, string unitAnimInfo = "Default_Gunner", string[] tags = null){
        GameObject chaObj = CreateFromPrefab("Character/CharacterObj",parent);
        //Vector3 playerPos = SceneVariants.map.GetRandomPosForCharacter(new RectInt(0, 0, SceneVariants.map.MapWidth(), SceneVariants.map.MapHeight()));
        //cha.AddComponent<PlayerController>().mainCamera = Camera.main; //敌人没有controller
        ChaState cs = chaObj.GetComponent<ChaState>();
        if (cs){
            cs.InitBaseProp(baseProp);
            cs.side = side;
            Dictionary<string, AnimInfo> aInfo = new Dictionary<string, AnimInfo>();
            if (unitAnimInfo != "" && DesingerTables.UnitAnimInfo.data.ContainsKey(unitAnimInfo)){
                aInfo = DesingerTables.UnitAnimInfo.data[unitAnimInfo];
            }
            cs.SetView(CreateFromPrefab("Character/" + prefab,parent), aInfo);
            if (tags != null) cs.tags = tags;
        }
        
        chaObj.transform.localPosition = pos;
        chaObj.transform.RotateAround(chaObj.transform.position, Vector3.up, degree);
        return chaObj;
    }
}
