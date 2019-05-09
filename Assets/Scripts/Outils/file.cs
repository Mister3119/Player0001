using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SafeGuard
{
    public class file
    {
        public static void Save(string _path, object _data)
        {
            string name = "";

            for (int i = 0; i < _path.Length - 1; i++)
            {
                if (_path[i].ToString() == "/")
                {
                    name = "";
                    for (int x = 0; x < i; x++)
                    {
                        name += _path[x].ToString();
                    }
                }
            }

            if (!Directory.Exists(Application.dataPath + name))
            {
                Directory.CreateDirectory(Application.dataPath + name);
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.dataPath + _path);
            bf.Serialize(file, _data);
            file.Close();
        }

        public static object Load(string _path)
        {
            BinaryFormatter bf = new BinaryFormatter();

            if (File.Exists(Application.dataPath + _path))
            {
                FileStream file = File.Open(Application.dataPath + _path, FileMode.Open);
                object _data = (object)bf.Deserialize(file);
                file.Close();
                return _data;
            }
            else
            {
                return new object();
            }
        }
    }
}

