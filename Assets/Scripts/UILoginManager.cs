using BackEnd;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

using TMPro;
public class UILoginManager : MonoBehaviour
{
    public TMP_InputField Username = null;

    public TMP_InputField Password = null;

    public TMP_InputField Property = null;

    public Button LoginButton = null;

    public GameObject StartButton = null;

    // Start is called before the first frame update
    void Start()
    {
        if (LoginButton != null)
        {
            LoginButton.onClick.AddListener(Login);
        }

        StartButton.SetActive(false);
    }

    void Login()
    {
        if (Username != null && Password != null)
        {
            if (Username.text.Length > 0 && Password.text.Length > 0)
            {
                if (Property != null)
                {
                    if (Property.text.Length > 0)
                    {
                        List<string> list = new List<string>();
                        list.AddRange(Property.text.Split(','));

                        if (list.Count == 4)
                        {
                            Backend.LocationProperties.CustomizeLocationProperties(list[0], list[1], list[2], list[3]);
                        }

                        //Backend.LocationProperties.CustomizeLocationProperties("Seoul", "South Korea", "Seoul", "ko-KR");
                    }
                }

                bool isMultiCharacter = false;



                if (isMultiCharacter)
                {
                    var returnObject = Backend.MultiCharacter.Account.LoginAccount(Username.text, Password.text);

                    if (returnObject.IsSuccess())
                    {
                        Debug.Log("계정 로그인 : " + returnObject);
                    }
                    else
                    {
                        Debug.LogError("계정 로그인 : " + returnObject);

                        returnObject = Backend.MultiCharacter.Account.CreateAccount(Username.text, Password.text);

                        if (returnObject.IsSuccess())
                        {
                            Debug.Log("계정 생성 : " + returnObject);
                        }
                        else
                        {
                            Debug.LogError("계정 생성 : " + returnObject);
                            return;
                        }

                        returnObject = Backend.MultiCharacter.Character.CreateCharacter(Username.text);

                        if (returnObject.IsSuccess())
                        {
                            Debug.Log("캐릭터 생성 : " + returnObject);
                        }
                        else
                        {
                            Debug.LogError("캐릭터 생성 : " + returnObject);
                            return;
                        }
                    }

                    var bro = Backend.MultiCharacter.Character.GetCharacterList();

                    if (bro.IsSuccess())
                    {
                        Debug.Log("캐릭터 리스트 불러오기 : " + bro);
                    }
                    else
                    {
                        Debug.LogError("캐릭터 리스트 불러오기 : " + bro);
                    }

                    LitJson.JsonData characterJson = bro.GetReturnValuetoJSON()["characters"][0];

                    string uuid = characterJson["uuid"].ToString();
                    string inDate = characterJson["inDate"].ToString();

                    Debug.Log($"캐릭터 정보 : uuid : {uuid} / inDate : {inDate}");

                    var bro2 = Backend.MultiCharacter.Character.SelectCharacter(uuid, inDate);

                    if (bro2.IsSuccess())
                    {
                        Debug.Log("캐릭터 로그인 성공 : " + bro2);
                    }
                    else
                    {
                        Debug.LogError("캐릭터 로그인 실패 : " + bro2);
                    }
                }
                else
                {
                    var returnObject = Backend.BMember.CustomLogin(Username.text, Password.text);
                    if (false == returnObject.IsSuccess())
                    {
                        if (returnObject.IsSuccess())
                        {
                            Debug.Log("로그인 성공 : " + returnObject);
                        }
                        else
                        {
                            Debug.LogError("로그인 실패 : " + returnObject);
                        }

                        returnObject = Backend.BMember.CustomSignUp(Username.text, Password.text);
                        if (returnObject.IsSuccess())
                        {
                            Debug.Log("회원가입 성공 : " + returnObject);

                        }
                        else
                        {
                            Debug.LogError("회원가입 실패 : " + returnObject);
                        }

                        var bro = Backend.BMember.UpdateNickname(Username.text);
                        if (bro.IsSuccess())
                        {
                            Debug.Log("닉네임 변경 성공 : " + bro);

                        }
                        else
                        {
                            Debug.LogError("닉네임 변경 실패 : " + bro);
                        }
                    }
                    else
                    {
                        gameObject.SetActive(false);
                        StartButton.SetActive(true);

                    }
                }

            }
        }
    }

    public void JoinButton()
    {
        SceneManager.LoadScene("Main");
    }
}
