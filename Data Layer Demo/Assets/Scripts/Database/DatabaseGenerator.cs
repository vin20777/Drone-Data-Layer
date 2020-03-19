using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpressionGenerator : MonoBehaviour
{
    #region script setting
    [SerializeField]
    private bool debug = false;
    [SerializeField]
    private bool usePrefab = false;
    [SerializeField]
    private bool horizontalEvaluation;
    #endregion

    #region prefabs
    [Tooltip("Must be assigned")]
    [SerializeField]
    private GameObject sandBox = null;
    [Tooltip("Must be assigned if use prefab is been checked")]
    [SerializeField]
    private GameObject OperatorPlus = null;
    [Tooltip("Must be assigned if use prefab is been checked")]
    [SerializeField]
    private GameObject OperatorMinus = null;
    [Tooltip("Must be assigned if use prefab is been checked")]
    [SerializeField]
    private GameObject OperatorMultiply = null;
    [Tooltip("Must be assigned if use prefab is been checked")]
    [SerializeField]
    private GameObject OperatorDivisor = null;
    [Tooltip("Must be assigned if use prefab is been checked")]
    [SerializeField]
    private GameObject Numbers = null;
    #endregion

    #region private variables
    private LinkedList<string> expression;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region private methods
    //Scan sndbox and store each element to a list
    private void ScanSandBox()
    {
        expression = new LinkedList<string>();
        GameObject[] childrenList = GetChildren(sandBox);
        if (usePrefab)
            ScanPrefabs(childrenList);
        else
            ScanGameObjects(childrenList);

    }

    private void ScanPrefabs(GameObject[] childrenList)
    {
        foreach (GameObject child in childrenList)
        {
            //switch statement requires constant to compare
            if (child.name.Contains(OperatorPlus.name))
            {
                expression.AddLast("+");
            }
            else if (child.name.Contains(OperatorMinus.name))
            {
                expression.AddLast("-");
            }
            else if (child.name.Contains(OperatorMultiply.name))
            {
                expression.AddLast("*");
            }
            else if (child.name.Contains(OperatorDivisor.name))
            {
                expression.AddLast("/");
            }
            else if (child.name.Contains(Numbers.name))
            {
                //TODO Check Numbers Prefab to make sure where to get text
                expression.AddLast(child.GetComponentInChildren<Text>().text);
            }
            else
            {
                Debug.LogError("Unexpected GameObject in SandBox", child);
            }
        }
    }

    private void ScanGameObjects(GameObject[] childrenList)
    {
        //Text text;
        foreach (GameObject child in childrenList)
        {
            //text = GetSubTextComponent(child);
            expression.AddLast(GetSubTextComponent(child));
        }
    }

    private string GetSubTextComponent(GameObject gameObject)
    {
        if(gameObject.GetComponent<Text>() !=null)
        {
            //this gameObject hsa Text component
            return gameObject.GetComponent<Text>().text;
        }
        else if(gameObject.GetComponentInChildren<InputField>() !=null)
        {
            return gameObject.GetComponentInChildren<InputField>().text;
            //this gameObject's child has Text component
            //return gameObject.GetComponentInChildren<Text>();
        }
        else if(gameObject.GetComponentInChildren<Text>() != null)
        {
            return gameObject.GetComponentInChildren<Text>().text;
            //search each children
            /*
            foreach (Transform child in gameObject.transform)
            {
                GetSubTextComponent(child.gameObject);
            }*/
        }

        return null;
    }

    private GameObject[] GetChildren(GameObject parent)
    {
        if (debug)
        {
            Debug.Log("GetChildren == null " + parent == null ? "True" : "False");
            Debug.Log(parent.name + ": has " + parent.transform.childCount.ToString() + " children");
        }
        ArrayList childrenList = new ArrayList();
        foreach(Transform child in parent.transform)
        {
            childrenList.Add(child.gameObject);
        }
        if(horizontalEvaluation)
            childrenList.Sort(new GameObjectTransformHorizontalCompare());
        else
            childrenList.Sort(new GameObjectTransformVerticalCompare());
        return (GameObject[])(childrenList.ToArray(typeof(GameObject)));
    }



    private class GameObjectTransformVerticalCompare:IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            /*
             * Taiga task #100 Improved directional evaluation
             * Author Yijian Hu
             * Edited 11/20/2019
             */
            float diffX = ((GameObject)x).transform.localPosition.x - ((GameObject)y).transform.localPosition.x;
            float diffY = ((GameObject)y).transform.localPosition.y - ((GameObject)x).transform.localPosition.y;
            return diffY > 0 ? 1 : diffY == 0 ? (diffX > 0 ? -1 : 1) : -1;
        }
    }

    private class GameObjectTransformHorizontalCompare : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            /*
             * Taiga task #100 Improved directional evaluation
             * Author Yijian Hu
             * Edited 11/20/2019
             */
            float diffX = ((GameObject)x).transform.localPosition.x - ((GameObject)y).transform.localPosition.x;
            float diffY = ((GameObject)y).transform.localPosition.y - ((GameObject)x).transform.localPosition.y;
            return diffX > 0 ? 1 : diffX == 0 ? (diffY > 0 ? 1 : -1) : -1;
        }
    }
    #endregion

    #region public methods
    public LinkedList<string> GetExpression()
    {
        ScanSandBox();
        if(debug)
            foreach (string s in expression)
                print(s);
            return expression;
    }

    public void SwitchEvaluationDirection()
    {
        horizontalEvaluation = !horizontalEvaluation;
    }

    public bool IsHorizontalEvaluating()
    {
        return horizontalEvaluation;
    }

    public string ListToString()
    {
        ScanSandBox();
        string res = "";
        foreach (string s in expression)
            res += s;

        return res;
    }
    #endregion
}