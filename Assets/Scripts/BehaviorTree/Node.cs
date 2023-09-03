using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {

    public enum NodeState {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class Node
    {
        public NodeState state;

        public Node parent;
        public List<Node> children = new List<Node>();
        public Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        // 构造函数
        public Node()
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            foreach(Node child in children)
                _Attach(child);
        }

        public void _Attach(Node node)
        {
            children.Add(node);
        }

        // evaluate
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if(_dataContext.TryGetValue(key, out value))
                return value;
            
            Node node = parent;
            while(node != null)
            {
                value = node.GetData(key);
                if(value != null) 
                    return value;
                
                node = node.parent;
            }

            return null;
        }

        public bool ClearData(string key) {
            if( _dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            if(node == null)
                return false;
            bool cleared = node.ClearData(key);
            return cleared;
        }

    }

}
