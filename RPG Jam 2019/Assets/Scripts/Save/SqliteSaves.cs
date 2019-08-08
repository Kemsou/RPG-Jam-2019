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
                switch (data.type)
                {
                    case "Character":
                        InsertCharacter((CharacterData)data);
                        InsertInventory((CharacterData)data);
                        break;
                    case "2":

                        break;
                    default:
                        InsertData(data);
                        break;
                }

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

                result = cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "CREATE TABLE 'Character' ( " +
                            " 'SceneName'  TEXT , " +
                            " 'GameObjectName' TEXT ," +
                            " 'ComponentName'  TEXT ," +
                            " 'ValueName'      TEXT , " +
                            " 'Value'          TEXT ," +
                            " 'Type'          TEXT ," +
                            " 'name'          TEXT ," +
                            " 'lvl'           INTEGER ," +
                            " 'isAlly'        BOOLEAN ," +
                            " 'currentHealth' INTEGER ," +
                            " 'currentMana'   INTEGER ," +
                            " 'armorName'     TEXT ," +
                            " 'weaponName'    TEXT ," +
                            " 'gold'          INTEGER ," +
                            "  PRIMARY KEY ( 'SceneName', 'GameObjectName','ComponentName','ValueName') " +
                            ");";
                result = cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "CREATE TABLE 'Inventory' ( " +
                            " 'Id'  INTEGER , " +
                            " 'ItemName' TEXT ," +
                            " 'ItemQuantity'  INTEGER," +
                            "  PRIMARY KEY ( 'Id') " +
                            ");";
                result = cmd.ExecuteNonQuery();

                Debug.Log("create schema: " + result);
            }
        }
    }


    public void InsertData(Data data)
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
                    Value = data.sceneName
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "GameObjectName",
                    Value = data.gameObjectName
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "ComponentName",
                    Value = data.componentName
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "ValueName",
                    Value = data.valueName
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "Value",
                    Value = data.value
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "Type",
                    Value = data.type
                });

                var result = cmd.ExecuteNonQuery();
                Debug.Log("insert Data: " + result);
            }
        }
    }

    public void InsertCharacter(CharacterData character)
    {
        using (var conn = new SqliteConnection(dbPath))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO 'Character' ('SceneName', 'GameObjectName', 'ComponentName', 'ValueName', 'Value' , 'Type', 'name', 'lvl', 'isAlly', 'currentHealth', 'currentMana', 'armorName', 'weaponName', 'gold' " +
                                  "VALUES (@SceneName, @GameObjectName, @ComponentName , @ValueName , @Value, @Type, @name, @lvl, @isAlly, @currentHealth, @currentMana, @armorName, @weaponName, @gold );";

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "SceneName",
                    Value = character.sceneName
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "GameObjectName",
                    Value = character.gameObjectName
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "ComponentName",
                    Value = character.componentName
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "ValueName",
                    Value = character.valueName
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "Value",
                    Value = character.value
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "Type",
                    Value = character.type
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "name",
                    Value = character.name
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "lvl",
                    Value = character.lvl
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "isAlly",
                    Value = character.isAlly
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "currentHealth",
                    Value = character.currentHealth
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "currentMana",
                    Value = character.currentMana
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "armorName",
                    Value = character.armorName
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "weaponName",
                    Value = character.weaponName
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "gold",
                    Value = character.gold
                });

                var result = cmd.ExecuteNonQuery();
                Debug.Log("insert Data: " + result);
            }
        }
    }

    public void InsertInventory(CharacterData character){
        using (var conn = new SqliteConnection(dbPath))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {

                foreach (var Names in character.inventoryNames)
                {
                    
                }
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO 'Data' ('SceneName', 'GameObjectName', 'ComponentName', 'ValueName', 'Value' , 'Type') " +
                                  "VALUES (@SceneName, @GameObjectName, @ComponentName , @ValueName , @Value, @Type);";

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "SceneName",
                    Value = character.sceneName
                });

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "GameObjectName",
                    Value = character.gameObjectName
                });

                var result = cmd.ExecuteNonQuery();
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
