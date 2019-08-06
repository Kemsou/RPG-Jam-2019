using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Collections.Generic;


public class SqliteSaves : MonoBehaviour
{

    private string dbPath;

    private void Start()
    {
        dbPath = "URI=file:" + Application.persistentDataPath + "/Saves";
        //C:/Users/akitl/AppData/LocalLow/DefaultCompany/RPG Jam 2019/Saves

        Save();
        // InsertScore("GG Meade", 3701);
        // InsertScore("US Grant", 4242);
        // InsertScore("GB McClellan", 107);
        // GetHighScores(10);
    }

    public void Save()
    {
        CreateSchema();



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
                            "  PRIMARY KEY ( 'SceneName', 'GameObjectName','ComponentName','ValueName') " +
                            ");";

                Debug.Log(cmd.CommandText);
                result = cmd.ExecuteNonQuery();

                Debug.Log("create schema: " + result);
            }
        }
    }


    public void InsertData(string sceneName, string gameObjectName, string componentName, string valueName, string value)
    {
        using (var conn = new SqliteConnection(dbPath))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO 'Data' ('SceneName', 'GameObjectName', 'ComponentName', 'ValueName', 'Value' ) " +
                                  "VALUES (@SceneName, @GameObjectName, @ComponentName , @ValueName , @Value);";

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

                var result = cmd.ExecuteNonQuery();
                Debug.Log("insert score: " + result);
            }
        }
    }

    public List<List<string>> GetHighScores(int limit)
    {
        List<List<string>> datas = new List<List<string>>();
        using (var conn = new SqliteConnection(dbPath))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM 'Data';";

                Debug.Log("scores (begin)");
                var reader = cmd.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    datas[i].Add(reader.GetString(0)); //sceneName
                    datas[i].Add(reader.GetString(1)); //gameObjectName
                    datas[i].Add(reader.GetString(2)); //componentName
                    datas[i].Add(reader.GetString(3)); //valueName
                    datas[i].Add(reader.GetString(4)); // value
                    i++;
                }
            }
        }
          Debug.Log("Datas load");
        return datas;
    }
}
