using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WheelManager : MonoBehaviour
{
    public Image Object,ObjectClone;
    public GameObject AudioSource;
   public GameObject AudioSourceP;
    public int TotalObjects;
    public Transform PObject;
    public Color[] Colors;
    public GameObject InputText;
    public bool Spining=false;
    public bool WaitingRes = false;
    float f, temp;
    float RotationValue;
    public int ResultIndex;
    public string[] ObjectArr;
    public string ContentText;
    public string[] PreMadeContent;
    public TextMeshProUGUI ResultText;
    public bool remove = false;
    public Button RemoveButoon;

    public GameObject Effect;

    private void Start()
    {
        PreMadeContent[0] = "1\n2\n3\n4\n5\n6\n7\n8";

        RemoveButoon.interactable = false;
        WheelCreator();
    }

    void Update()
    {
        if(PObject.GetComponent<Rigidbody2D>().angularVelocity <= -0.1f)
        {
            Spining = true;
            WaitingRes = true;
        }
        else
        {
            foreach (Transform child in AudioSourceP.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            Spining = false;
            if(WaitingRes)
            {
                float result = PObject.GetComponent<RectTransform>().eulerAngles.z + 90;
                if(result>360)
                {
                    int temp = (int)result / 360;
                    int temp2 = temp * 360;
                    result = result - temp2;
                }
                WaitingRes = false;
               // Debug.Log("waiting tunerd false");
                for(int i=0;i<TotalObjects;i++)
                {
                    int temp = (int)RotationValue * i+1;
                    if(temp>result)
                    {
                        ResultIndex = i-1;
                      //  Debug.Log("Result is " + ObjectArr[ResultIndex]);
                      //  Debug.Log("Result is " + ObjectArr[ResultIndex]);
                        ShowResult();
                        break;
                    }
                }
            }
        }
    }

    public void WheelCreator()
    { if (!remove)
        {
            if (!Spining)
            {
                if (!string.IsNullOrEmpty(InputText.GetComponent<TMP_InputField>().text.ToString()))
                {
                    ContentText = InputText.GetComponent<TMP_InputField>().text.ToString();
                    InputText.GetComponent<TMP_InputField>().text = "";
                }
                else
                {
                    ContentText = PreMadeContent[0];
                }
                // Debug.Log(string.IsNullOrEmpty(InputText.GetComponent<TMP_InputField>().text.ToString()));
                GetSpinObject(ContentText);
            }
        }
            remove = false;
            TotalObjects = ObjectArr.Length;
            foreach (Transform child in PObject)
            {
                GameObject.Destroy(child.gameObject);
            }
            RotationValue = (float)360 / TotalObjects;
            float fillValue = (float)RotationValue / 360;
            for (int i = 0; i < TotalObjects; i++)
            {
                ObjectClone = Instantiate(Object, PObject);
                ObjectClone.transform.Rotate(0f, 0f, (float)RotationValue * -i);
                ObjectClone.transform.GetChild(0).transform.Rotate(0f, 0f, (RotationValue / -2));
                ObjectClone.gameObject.SetActive(true);
                ObjectClone.GetComponentInChildren<Transform>().gameObject.GetComponentInChildren<TextMeshProUGUI>().text = ObjectArr[i].ToString();
                ObjectClone.fillAmount = fillValue;
                // ObjectClone.GetComponent<Image>().color = Colors[(int)i%6];
                ObjectClone.GetComponent<Image>().color = Colors[i];
            }
            Vector3 temp = new Vector3(0f, 0f, 0f);
            PObject.GetComponent<RectTransform>().eulerAngles = temp;
        }


    public void Spin()
    {
        RemoveButoon.interactable = false;
        if (PObject.GetComponent<Rigidbody2D>().angularVelocity == 0)
        {
            ResultText.text = " ";
            PObject.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-500f, -2500f));

        }
    }


    public void GetSpinObject(string s)
    {
        ObjectArr = s.Split(char.Parse("\n"));
        for(int positionofArray = 0;positionofArray<ObjectArr.Length;positionofArray++)
        {
            string temp = ObjectArr[positionofArray];
            int randomizeArray = Random.Range(0, positionofArray);
            ObjectArr[positionofArray] = ObjectArr[randomizeArray];
            ObjectArr[randomizeArray] = temp;
        }
    }

    public void ShowResult()
    {
        ResultText.text = ObjectArr[ResultIndex] + " !";
        RemoveButoon.interactable = true;
        StartCoroutine(ShowEffect());
    }


    public void EliminateObject()
    {
        remove = true;
        RemoveAt(ref ObjectArr, ResultIndex);
        WheelCreator();
        RemoveButoon.interactable = false;
        
    }

    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        arr[index] = arr[arr.Length - 1];
        System.Array.Resize(ref arr, arr.Length - 1);
    }


    public IEnumerator ShowEffect()
    {
        Effect.transform.Rotate(new Vector3(0, 0, Random.Range(0f, 360f)));
        Effect.SetActive(true);
        yield return new WaitForSeconds(2f);
        Effect.SetActive(false);
    }

}
