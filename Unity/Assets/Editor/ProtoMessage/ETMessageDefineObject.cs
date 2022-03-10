using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using ET;
using UnityEditor;

[CreateAssetMenu(fileName = "消息协议配置", menuName = "消息协议/消息协议配置")]
public class ETMessageDefineObject : SerializedScriptableObject
{
    [LabelText("协议文件类型")]
    public ETProtoFileType FileName = ETProtoFileType.InnerMessage;
    [Space(20)]
    [LabelText("消息类列表")]
    [ListDrawerSettings(Expanded = true, NumberOfItemsPerPage = 30)]
    public List<MessageClass> MessageClasses;

    [Button("生成Proto代码", ButtonSizes.Large)]
    public static void AllProto2CS()
    {
        var messageDict = new Dictionary<ETProtoFileType, List<MessageClass>>();
        messageDict.Add(ETProtoFileType.InnerMessage, new List<MessageClass>());
        messageDict.Add(ETProtoFileType.OuterMessage, new List<MessageClass>());
        messageDict.Add(ETProtoFileType.MongoMessage, new List<MessageClass>());
        var arr = AssetDatabase.FindAssets("t:ETMessageDefineObject");
        foreach (var item in arr)
        {
            var path = AssetDatabase.GUIDToAssetPath(item);
            var obj = AssetDatabase.LoadAssetAtPath<ETMessageDefineObject>(path);
            messageDict[obj.FileName].AddRange(obj.MessageClasses);
        }

        foreach (var item in messageDict)
        {
            GenerateMessage(item.Key, item.Value);
        }

        ProcessHelper.Run("Tools.exe", "--AppType=Proto2CS", "../Bin/");
    }
    
    public static void GenerateMessage(ETProtoFileType FileName, List<MessageClass> MessageClasses)
    {
        var path = $"../Proto/{FileName}.proto";
		var sb = new StringBuilder();
        sb.Append("syntax = \"proto3\";\n");
        sb.Append("package ET;\n\n");
        foreach (var message in MessageClasses)
        {
            if (!string.IsNullOrEmpty(message.Annotation))
            {
                sb.Append("/// <summary>\n");
                sb.Append($"/// {message.Annotation}\n");
                sb.Append("/// </summary>\n");
            }

            if (!string.IsNullOrEmpty(message.ResponseType))
            {
                sb.Append($"//ResponseType {message.ResponseType}\n");
            }
            
            if (message.MessageType == ETMessageType.None)
                sb.Append($"message {message.ClassName}\n");
            else
                sb.Append($"message {message.ClassName} // {message.MessageType}\n");
            sb.Append("{\n");
            foreach (var paramConfig in message.MessageParamConfigs)
            {
                var type = "int32";
                switch (paramConfig.ParamType)
                {
                    case Proto3Type.Int32:
                        type = "int32";
                        break;
                    case Proto3Type.Int64:
                        type = "int64";
                        break;
                    case Proto3Type.Float:
                        type = "float";
                        break;
                    case Proto3Type.String:
                        type = "string";
                        break;
                    case Proto3Type.Bytes:
                        type = "bytes";
                        break;
                    case Proto3Type.Message:
                        type = paramConfig.Custom? paramConfig.CustomMessageClassName : paramConfig.MessageClassName;
                        break;
                    case Proto3Type.RepeatedInt32:
                        type = "repeated int32";
                        break;
                    case Proto3Type.RepeatedInt64:
                        type = "repeated int64";
                        break;
                    case Proto3Type.RepeatedFloat:
                        type = "repeated float";
                        break;
                    case Proto3Type.RepeatedString:
                        type = "string";
                        break;
                    case Proto3Type.RepeatedBytes:
                        type = "repeated bytes";
                        break;
                    case Proto3Type.RepeatedMessage:
                        if (paramConfig.Custom)
                            type = $"repeated {paramConfig.CustomMessageClassName}";
                        else
                            type = $"repeated {paramConfig.MessageClassName}";
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrEmpty(paramConfig.Comment))
                {
                    sb.Append($"\t// {paramConfig.Comment}\n");
                }
                sb.Append($"\t{type} {paramConfig.ParamName} = {paramConfig.MemberID};\n");
            }
            sb.Append("}\n\n");
        }
		File.WriteAllText(path, sb.ToString());
    }

    [Space(20)]
    [ToggleGroup("ImportMessagesGroup")]
    public bool ImportMessagesGroup;

    [ToggleGroup("ImportMessagesGroup")]
    [Button("反向导入消息类列表", ButtonHeight = 30)]
    public void ImportMessages()
    {
        MessageClasses = ParseMessages(FileName.ToString());
    }

    [Button("打开Proto目录", ButtonHeight = 30)]
    public void OpenProtoPath()
    {
        EditorUtility.RevealInFinder($"../Proto/{ETProtoFileType.OuterMessage}.proto");
    }

    private static readonly char[] splitChars = { ' ', '\t' };
    public static List<MessageClass> ParseMessages(string fileName)
    {
        var MessageClasses = new List<MessageClass>();
        var lines = File.ReadAllLines($"../Proto/{fileName}.proto");
        MessageClass message = null;
        StringBuilder annotation = new StringBuilder();
        string responseType = "";
        string comment = "";
        foreach (var item in lines)
        {
            var str = item.Trim();
            var repeated = false;
            if (str.StartsWith("///"))
            {
                var split = str.Split(splitChars);
                if(split[1].Contains("<summary>") || split[1].Contains("</summary>")) continue;
                annotation.Append(split[1]);
            }
            else if (str.StartsWith("//ResponseType"))
            {
                var split = str.Split(splitChars);
                responseType = split[1];
            }
            else if (str.StartsWith("message "))
            {
                message = new MessageClass();
                if (annotation.Length > 0)
                {
                    message.Annotation = annotation.ToString();
                    annotation.Clear();
                }

                if (!string.IsNullOrEmpty(responseType))
                {
                    message.ResponseType = responseType;
                    responseType = "";
                }
                
                var match = Regex.Match(str, @"(?<=message ).*?(?= )");
                var className = match.Value;
                if (string.IsNullOrEmpty(className))
                {
                    match = Regex.Match(str, @"(?<=message ).*?(?=\n)");
                    className = match.Value;
                    if (string.IsNullOrEmpty(className))
                    {
                        className = str.Replace("message ", "");
                    }
                }
                if (!string.IsNullOrEmpty(className))
                {
                    message.ClassName = className;
                }

                var typeName = str.Replace($"message {className} // ", "");
                var type = ETMessageType.None;
                switch (typeName)
                {
                    case "IMessage": type = ETMessageType.IMessage;break;
                    case "IRequest": type = ETMessageType.IRequest;break;
                    case "IResponse": type = ETMessageType.IResponse;break;
                    case "IActorMessage": type = ETMessageType.IActorMessage;break;
                    case "IActorRequest": type = ETMessageType.IActorRequest;break;
                    case "IActorResponse": type = ETMessageType.IActorResponse;break;
                    case "IActorLocationMessage": type = ETMessageType.IActorLocationMessage;break;
                    case "IActorLocationRequest": type = ETMessageType.IActorLocationRequest;break;
                    case "IActorLocationResponse": type = ETMessageType.IActorLocationResponse; break;
                    default:break;
                }
                message.MessageType = type;
                MessageClasses.Add(message);
                continue;
            }
            else if (str.StartsWith("repeated "))
            {
                str = str.Replace("repeated ", "").Trim();
                repeated = true;
            }
            if (message == null)
                continue;
            var paramName = "";
            var paramMemberID = "";
            var messageType = "";
            var paramType = Proto3Type.Int32;
            //Debug.Log(str);
            
            if (str.StartsWith("int32 "))
            {
                // var match = Regex.Match(str, @"(?<=int32 ).*?(?==)");
                // paramName = match.Value;
                GetParamMemberID(str, ref paramName, ref paramMemberID);
                paramType = Proto3Type.Int32;
                if (repeated) paramType = Proto3Type.RepeatedInt32;
            }
            else if (str.StartsWith("int64 "))
            {
                // var match = Regex.Match(str, @"(?<=int64 ).*?(?==)");
                // paramName = match.Value;
                GetParamMemberID(str, ref paramName, ref paramMemberID);
                paramType = Proto3Type.Int64;
                if (repeated) paramType = Proto3Type.RepeatedInt64;
            }
            else if (str.StartsWith("float "))
            {
                // var match = Regex.Match(str, @"(?<=float ).*?(?==)");
                // paramName = match.Value;
                GetParamMemberID(str, ref paramName, ref paramMemberID);
                paramType = Proto3Type.Float;
                if (repeated) paramType = Proto3Type.RepeatedFloat;
            }
            else if (str.StartsWith("string "))
            {
                // var match = Regex.Match(str, @"(?<=string ).*?(?==)");
                // paramName = match.Value;
                GetParamMemberID(str, ref paramName, ref paramMemberID);
                paramType = Proto3Type.String;
                if (repeated) paramType = Proto3Type.RepeatedString;
            }
            else if (str.StartsWith("bytes "))
            {
                // var match = Regex.Match(str, @"(?<=bytes ).*?(?==)");
                // paramName = match.Value;
                GetParamMemberID(str, ref paramName, ref paramMemberID);
                paramType = Proto3Type.Bytes;
                if (repeated) paramType = Proto3Type.RepeatedBytes;
            }
            else if (str.StartsWith("//") && !str.StartsWith("///") && !str.StartsWith("//ResponseType"))
            {
                var split = str.Split(splitChars);
                for (int i = 1; i < split.Length; i++)
                {
                    comment += split[i] + " ";
                }
                continue;
            }
            else
            {
                if (!str.StartsWith("//") && str.EndsWith(";"))
                {
                    GetParamMemberID(str, ref paramName, ref paramMemberID);
                    var arr = str.Split(splitChars);
                    if (arr.Length > 2)
                    {
                        //paramName = arr[1];
                        messageType = arr[0];
                        paramType = Proto3Type.Message;
                        if (repeated) paramType = Proto3Type.RepeatedMessage;
                    }
                }
                else
                {
                    continue;
                }
            }
            //Debug.Log(paramName);
            message.MessageParamConfigs.Add(new MessageParamConfig()
            {
                ParamName = paramName.Trim(),
                ParamType = paramType,
                MessageClassName = messageType,
                CustomMessageClassName = messageType,
                MemberID = paramMemberID,
                Comment = comment
            });
            comment = "";
        }
        return MessageClasses;
    }

    private static void GetParamMemberID(string str,ref string paramName,ref string paramMemberID)
    {
        int index = str.IndexOf(";",StringComparison.CurrentCulture);
        str = str.Remove(index);
            
        var nameSplit = str.Split(splitChars);
        paramName = nameSplit[1];
        paramMemberID = nameSplit[3];
    }
}

[Serializable]
public class MessageClass
{
    [ToggleGroup("Enabled", "$ClassName", CollapseOthersOnExpand = false)]
    public bool Enabled;

    [ToggleGroup("Enabled", CollapseOthersOnExpand = false)]
    [LabelText("消息类型")]
    public ETMessageType MessageType;

    [ToggleGroup("Enabled", CollapseOthersOnExpand = false)]
    [LabelText("类名")]
    public string ClassName = "C2M_Message";
    
    [ToggleGroup("Enabled", CollapseOthersOnExpand = false)]
    [ShowIf("ShowMessageCustom")]
    [LabelText("返回类型")]
    public string ResponseType = "";

    [ToggleGroup("Enabled", CollapseOthersOnExpand = false)]
    [LabelText("注释")]
    public string Annotation = "";

    [ToggleGroup("Enabled", CollapseOthersOnExpand = true)]
    [LabelText("消息参数列表")]
    //[ListDrawerSettings(Expanded = true, NumberOfItemsPerPage = 10)]
    [TableList]
    public List<MessageParamConfig> MessageParamConfigs = new List<MessageParamConfig>();
    
    public bool ShowMessageCustom
    {
        get
        {
            return (MessageType == ETMessageType.IRequest || MessageType == ETMessageType.IActorRequest || MessageType == ETMessageType.IActorLocationRequest);
        }
    }
}

[Serializable]
public class MessageParamConfig
{
    [VerticalGroup("Name")]
    [HideLabel]
    public string ParamName = "Param1";
    
    [VerticalGroup("Type")]
    [HideLabel]
    public Proto3Type ParamType;
    
    [VerticalGroup("ID")]
    [HideLabel]
    public string MemberID;
    
    [VerticalGroup("Type")]
    [HideLabel]
    [ShowIf("ShowMessageCustom", true)]
    public bool Custom = false;
    
    [VerticalGroup("Type")]
    [HideLabel]
    [ShowIf("Custom", true)]
    public string CustomMessageClassName;
    
    [VerticalGroup("Type")]
    [HideLabel]
    [ShowIf("ShowMessageClassName", true)]
    [ValueDropdown("GetAllMessages", DropdownWidth = 150, NumberOfItemsBeforeEnablingSearch = 10)]
    public string MessageClassName;

    [TextArea]
    public string Comment;

    public bool ShowMessageClassName
    {
        get
        {
            return ShowMessageCustom && Custom == false;
        }
    }

    public bool ShowMessageCustom
    {
        get
        {
            return (ParamType == Proto3Type.Message || ParamType == Proto3Type.RepeatedMessage);
        }
    }

    public static IEnumerable GetAllMessages()
    {
        var obj = (ETMessageDefineObject)UnityEditor.Selection.activeObject;
        return obj.MessageClasses.Select(x => new ValueDropdownItem(x.ClassName, x.ClassName));
    }
}

[Serializable]
public enum ETMessageType
{
    None,
    IMessage,
    IRequest,
    IResponse,
    IActorMessage,
    IActorRequest,
    IActorResponse,
    IActorLocationMessage,
    IActorLocationRequest,
    IActorLocationResponse,
}

[Serializable]
public enum Proto3Type
{
    Int32,
    Int64,
    Float,
    String,
    Bytes,
    Message,
    RepeatedInt32,
    RepeatedInt64,
    RepeatedFloat,
    RepeatedString,
    RepeatedBytes,
    RepeatedMessage,
}

[Serializable]
public enum ETProtoFileType
{
    InnerMessage,
    OuterMessage,
    MongoMessage
}