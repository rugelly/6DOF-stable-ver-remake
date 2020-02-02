using UnityEngine;

// TODO: refactor this shit the second it turns out to be difficult to maintain
public class Priorities : MonoBehaviour
{
    /* public int level {get{return LOS + RANGE + HURT + etc}}

    components to watch

    public const int *inLOSDelta* <- how much does the target being in LOS change the level
    public const int *inRangeDelta* <- how much does " " " " in range " " "
    public const int hurtDelta

    **OR** is each thing worth 1? 
    and instead choose which functions are activated at certain level amounts

    private void Update()
    {
        randomMovement = 0;
        attackRange = 1;
        runAwayRange = 2;
        ** being hurt delta = 1 for a short period then back to 0
        ** so is at attack range level unless hurt triggers then runs away for a period
       
        switch (level)
        {
            case randomMovement
                 RandomMovement();
            case attackRange
                 KeepDistance(amount1);
            case runAwayRange
                 KeepDistance(amount2);
            default:
        }
    } */

    FollowType _follow;

    private void Awake()
    {
        _follow = GetComponent<FollowType>();
    }

    public int level;

    public BehaviourPriority[] behaviourPriorities;

    private void Update()
    {
        
    }
}