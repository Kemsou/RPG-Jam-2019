using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Collections.Generic;


public class SqliteSaves
{

    private string dbPath;

    public SqliteSaves()
    {
        dbPath = "URI=file:" + Application.persistentDataPath + "/Saves";
        //C:/Users/akitl/AppData/LocalLow/DefaultCompany/RPG Jam 2019/Saves

    }

    public void Save(Dictionary<string, List<Data>> datas)
    {
        CreateSchema();

        foreach (KeyValuePair<string, List<Data>> entry in datas)
        {
            foreach (Data data in entry.Value)
            {
                InsertData(data.sceneName,data.gameObjectName,data.componentName,data.valueName,data.value,data.type);
            }
        }


    }



    public void CreateSchema()
    {
        using (var conn = new SqliteConnection(dbPath))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " DROP TABLE IF EXISTS 'Data';";

                var result = cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "CREATE TABLE 'Data' ( " +
                            " 'SceneName'  TEXT , " +
                            " 'GameObjectName' TEXT ," +
                            " 'ComponentName'  TEXT ," +
                            " 'ValueName'      TEXT , " +
                            " 'Value'          TEXT ," +
                            " 'Type'          TEXT ," +
                            "  PRIMARY KEY ( 'SceneName', 'GameObjectName','ComponentName','ValueName') " +
                            ");";

                Debug.Log(cmd.CommandText);
                result = cmd.ExecuteNonQuery();

                Debug.Log("create schema: " + result);
            }
        }
    }


    public void InsertData(string sceneName, string gameObjectName, string componentName, string valueName, string value, string type)
    {
        using (var conn = new SqliteConnection(dbPath))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO 'Data' ('SceneName', 'GameObjectName', 'ComponentName', 'ValueName', 'Value' , 'Type') " +
                                  "VALUES (@SceneName, @GameObjectName, @ComponentName , @ValueName , @Value, @Type);";

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "SceneName",
                    Value = sceneName
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "GameObjectName",
                    Value = gameObjectName
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "ComponentName",
                    Value = componentName
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "ValueName",
                    Value = valueName
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "Value",
                    Value = value
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "Type",
                    Value = type
                });

                var result = cmd.ExecuteNonQuery();
                Debug.Log("insert Data: " + result);
            }
        }
    }

    public Dictionary<string, List<Data>> GetDatas()
    {
        Dictionary<string, List<Data>> datas = new Dictionary<string, List<Data>>();
        using (var conn = new SqliteConnection(dbPath))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM 'Data';";

                Debug.Log("scores (begin)");
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (datas.ContainsKey(reader.GetString(0)))
                    {
                        datas[reader.GetString(0)].Add(new Data(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5)));
                        //                                             sceneName       gameObjectName       componentName      valueName             value                 type
                    }
                    else
                    {
                        datas.Add(reader.GetString(0), new List<Data>());
                        datas[reader.GetString(0)].Add(new Data(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5)));
                    }
                }
            }
        }
        Debug.Log("Datas load");
        return datas;
    }
}
