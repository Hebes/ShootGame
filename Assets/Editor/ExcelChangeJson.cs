using ExcelDataReader;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 参考：https://blog.csdn.net/dark_tone/article/details/102791985
/// ExcelDataReader-3.7.0 github:https://github.com/ExcelDataReader/ExcelDataReader  https://zhuanlan.zhihu.com/p/396766170 
/// Unity技术沉淀--Excel表格导出json、lua：https://www.jianshu.com/p/879f2233a1a2
/// Unity 之 Excel表格转换为Unity用的文件格式 -- ScriptableObject，Json，XML 全部搞定:https://developer.aliyun.com/article/802128
/// </summary>
/*使用阅读器方法
扩展方法是快速获取数据的AsDataSet()便捷助手，但并不总是可用或不希望使用。IExcelDataReader 扩展System.Data.IDataReader和IDataRecord接口以在较低级别导航和检索数据。最重要的读取器方法和属性：
Read()          从当前工作表中读取一行。
NextResult()    使光标前进到下一张纸。
ResultsCount    返回当前工作簿中的工作表数。
Name            返回当前工作表的名称。
CodeName        返回当前工作表的 VBA 代码名称标识符。
FieldCount      返回当前工作表中的列数。
RowCount        返回当前工作表中的行数。这包括被 AsDataSet() 排除的终端空行。与. InvalidOperationException_AnalyzeInitialCsvRows
HeaderFooter    返回一个对象，其中包含有关页眉和页脚的信息，或者null如果没有。
MergeCells      返回当前工作表中合并单元格区域的数组。
RowHeight       返回当前行的视觉高度（以磅为单位）。如果该行被隐藏，则可能为 0。
GetColumnWidth()返回以字符为单位的列宽。如果该列被隐藏，则可能为 0。
GetFieldType()  返回当前行中值的类型。总是 Excel 支持的类型之一：double, int, bool, DateTime, TimeSpan, string, 或者null如果没有值。
IsDBNull()      检查当前行中的值是否为空。
GetValue()      从当前行返回一个值作为object，或者null如果没有值。
GetDouble(), GetInt32(), GetBoolean(), GetDateTime(),GetString()从当前行返回一个值，转换为它们各自的类型。
GetNumberFormatString()返回一个字符串，其中包含当前行中值的格式代码，或者null如果没有值。另请参阅下面的格式部分。
GetNumberFormatIndex()返回当前行中值的数字格式索引。低于 164 的索引值指的是内置数字格式，否则表示自定义数字格式。
GetCellStyle()  返回包含当前行中单元格样式信息的对象：缩进、水平对齐、隐藏、锁定。
除非类型完全匹配，否则类型化的Get*()方法会抛出。InvalidCastException
*/
public class ExcelChangeJson : Editor
{
    private static string OutPath;
    private static string CSharpPath;
    private static List<string> dataType = new List<string>();//数据类型  int  flot
    private static List<string> dataName = new List<string>();//数据名称  ID   name 
    private static List<string[]> ExcelDateList = new List<string[]>();//数据


    private static string xlsxFilrPath = "/Editor/ConfigItemData.xlsx";
    private static string outPath = "/Resources/Config_Json/";//输出的路径
    private static string CoutPath = "/Test/";//输出的路径

    [MenuItem("我自己的工具/转化/Excel转json")]
    public static void Excel_Change_Json() => ReadExcel(Application.dataPath + xlsxFilrPath);




    private static void ReadExcel(string path)
    {
        using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))//文件类型打开
        {
            /*
              ①可以读取xlsx，使用 ExcelReaderFactory.CreateOpenXmlReader()
              ②可以读取xls，使用ExcelReaderFactory.CreateBinaryReader()
              ③可以读取csv，使用ExcelReaderFactory.CreateCsvReader()
             */
            using (IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
            {
                var result = reader.AsDataSet();
                for (int i = 0; i < result.Tables.Count; i++)
                {
                    Clear();
                    string JsonName = result.Tables[i].Rows[0][0].ToString();
                    FileOutName(outPath, JsonName);


                    for (int rowNum = 0; rowNum < result.Tables[i].Rows.Count; rowNum++)//行
                    {
                        if (rowNum == 0 || rowNum == 3) continue;//跳过第1行和第4行
                        string[] table = new string[result.Tables[i].Columns.Count];
                        for (int columnNum = 0; columnNum < result.Tables[i].Columns.Count; columnNum++)//列
                        {
                            switch (rowNum)
                            {
                                case 1: dataType.Add(result.Tables[i].Rows[1][columnNum].ToString()); break;//添加类型
                                case 2: dataName.Add(result.Tables[i].Rows[2][columnNum].ToString()); break;//添加名字
                                default: table[columnNum] = result.Tables[i].Rows[rowNum][columnNum].ToString(); break;//添加数据
                            }
                        }
                        if (rowNum >= 4) ExcelDateList.Add(table);//将一行数据存入list
                    }
                    Debug.Log(string.Format($"<color=#FFFF00>{JsonName + "表，数据转化列表完成,进行转化Json数据"}</color>"));
                    NewFile(JsonName);//转json
                    CreatCSharp(JsonName);//转化c#脚本 
                }
            }
        }
        AssetDatabase.Refresh();//unity刷新
    }
    /// <summary>
    /// 清空列表
    /// </summary>
    private static void Clear()
    {
        dataType.Clear();
        dataName.Clear();
        ExcelDateList.Clear();
    }
    /// <summary>
    /// 输出json文件的名字和路径
    /// </summary>
    /// <param name="path"></param>
    /// <param name="result"></param>
    /// <param name="number"></param>
    private static void FileOutName(string path, string jsonFilrName)
    {
        string JsonPath = Application.dataPath + path;
        OutPath = JsonPath + jsonFilrName + ".json.txt";
        CSharpPath = Application.dataPath + CoutPath + jsonFilrName + ".cs";
    }

    /// <summary>
    /// 输出文件excel转json
    /// </summary>
    private static void NewFile(string JsonName)
    {
        if (File.Exists(OutPath))//删除文件
        {
            File.Delete(OutPath);
            Debug.Log(string.Format($"<color=#FFFF00>{"删除" + OutPath + "文件成功"}</color>", ""));
        }
        //数字（整数或浮点数）
        //字符串（在双引号中）
        //逻辑值（true 或 false）
        //数组（在方括号中）
        //对象（在花括号中）
        //null
        using (StreamWriter writer = new StreamWriter(OutPath, false, Encoding.GetEncoding("UTF-8")))
        {

            string[] str = new string[ExcelDateList.Count];
            for (int i = 0; i < ExcelDateList.Count; i++)
            {
                string[] dataNameStr = new string[dataName.Count];
                string str4;
                for (int j = 0; j < dataName.Count; j++)
                {
                    string str3 = "\"" + dataName[j] + "\":";

                    switch (dataType[j])
                    {
                        default: str4 = "\"" + ExcelDateList[i][j] + "\""; break;//默认string 
                        case "i": str4 = ExcelDateList[i][j]; break;
                        case "b": str4 = ExcelDateList[i][j].ToLower(); break;//字符串转换成小写
                        case "l": str4 = "[" + ExcelDateList[i][j] + "]"; break;//字符串转换成小写
                    }

                    dataNameStr[j] = str3 + str4;
                    str4 = null;
                }
                //字符串String操作类，操作字符串的方法:https://blog.csdn.net/Anoxial/article/details/123114751
                //string str1 = "{" + string.Join(",", dataNameStr) + "}";
                string str1 = "\"" + i + "\":{" + string.Join(",", dataNameStr) + "}";
                str[i] = str1;
            }

            writer.WriteLine("{\"" + JsonName + "\":{" + string.Join(",", str) + "}}");
            //writer.WriteLine("{\"" + JsonName + "\":[" + string.Join(",", str) + "]}");
            //writer.WriteLine("[" + string.Join(",", str) + "]");

            writer.Flush();//清理当前写入器的所有缓冲区，并使所有缓冲数据写入基础流。
        }



        //JsonData jsonDatas = new JsonData();
        //jsonDatas.SetJsonType(JsonType.Array);
        //for (int i = 0; i < ExcelDateList.Count; i++)
        //{
        //    JsonData jsonData = new JsonData();
        //    for (int j = 0; j < dataName.Count; j++)
        //    {
        //        jsonData[dataName[j]] = ExcelDateList[i][j].ToString();
        //    }
        //    jsonDatas.Add(jsonData);
        //}
        //string json = jsonDatas.ToJson();

        ////防止中文乱码
        //Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");

        //using (StreamWriter writer = new StreamWriter(OutPath, false, Encoding.GetEncoding("UTF-8")))
        //{
        //    writer.WriteLine("{" + "\"" + jsonFilrName + "\":");
        //    writer.WriteLine(reg.Replace(json, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); }));
        //    writer.WriteLine("}");
        //    writer.Flush();//清理当前写入器的所有缓冲区，并使所有缓冲数据写入基础流。
        //}
        //可写可不写  unity刷新
        AssetDatabase.Refresh();

        //String.format()的详细用法：https://blog.csdn.net/anita9999/article/details/82346552/
        Debug.Log(string.Format($"<color=#FFFF00>{JsonName + "表，转化Json数据完毕"}</color>"));
    }

    /// <summary>
    /// 代码生成类
    /// 参考：https://blog.csdn.net/weixin_43149049/article/details/124696902
    /// https://blog.csdn.net/wangtao19932008/article/details/108852227
    /// </summary>
    /// <param name="name"></param>
    private static void CreatCSharp(string name)
    {
        if (File.Exists(CSharpPath)) File.Delete(CSharpPath);

        //CodeTypeDeclaration 代码类型声明类
        CodeTypeDeclaration CSharpClass = new CodeTypeDeclaration(name);
        CSharpClass.IsClass = true;
        CSharpClass.TypeAttributes = TypeAttributes.Public;

        for (int i = 0; i < dataName.Count; i++)
        {
            // 创建字段
            //CodeMemberField 代码成员字段类 => (Type, string name)
            CodeMemberField member = new CodeMemberField(GetTypeForExcel(dataName[i], dataType[i]), dataName[i]);
            member.Attributes = MemberAttributes.Public;
            CSharpClass.Members.Add(member);
        }
        // 获取C#语言的实例
        CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
        // 代码生成器选项类
        CodeGeneratorOptions options = new CodeGeneratorOptions();
        //设置支撑的样式
        options.BracingStyle = "C";
        //在成员之间插入空行
        options.BlankLinesBetweenMembers = true;
        using (StreamWriter writer = new StreamWriter(CSharpPath, false, Encoding.GetEncoding("UTF-8")))
        {
            //生成最终代码
            provider.GenerateCodeFromType(CSharpClass, writer, options);
            writer.Flush();//清理当前写入器的所有缓冲区，并使所有缓冲数据写入基础流。
        }
        Debug.Log(string.Format($"<color=#FFFF00>{name + "转化c#代码成功"}</color>"));
    }

    private static Type GetTypeForExcel(string Name, string Type)
    {

        switch (Type)
        {
            default: return typeof(string);
            case "i": return typeof(int);
            case "f": return typeof(float);//float关键字是System.Single的别名
            case "d": return typeof(double);
            case "b": return typeof(bool);
            case "l": return typeof(List<int>);
        }
    }
}
