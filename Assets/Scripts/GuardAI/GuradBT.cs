using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class GuradBT : BehaviorTree.Tree
{
    public UnityEngine.Transform[] wayPoints;

    protected override Node SetUpTree()
    {
        Node root = new TaskPatrol(transform, wayPoints);
        return root;
    }
}
