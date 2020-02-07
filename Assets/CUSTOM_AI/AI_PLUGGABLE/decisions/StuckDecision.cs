using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "pluggableAI/decisions/stuck")]
public class StuckDecision : Decision
{
    public float stuckMinDistance = 0.1f;
    public float maxTime = 0.5f;

    float timer;
    bool running;
    public override bool Decide(StateController controller)
    {
        if (!running)
            controller.StartCoroutine(SavePosition(controller));
        timer += 1 * Time.deltaTime;
        return timer == maxTime ? true : false;
    }

    IEnumerator SavePosition(StateController controller)
    {
        Debug.Log("stuck check coroutine start... waiting for 0.2secs to compare position...");
        running = false;
        var initPos = controller.transform.position;
        yield return new WaitForSeconds(0.2f);
        var storedPos = controller.transform.position;
        if (Vector3.Distance(initPos, storedPos) < stuckMinDistance)
            timer = 0;
        running = true;
        Debug.Log("stuck check complete... hopefully rerunning coroutine...");
    }
}