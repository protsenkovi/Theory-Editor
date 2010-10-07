using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

using Protsenko.TheoryEditor.Core.Debug;
using Protsenko.TheoryEditor.Core.Languages;

namespace Protsenko.TheoryEditor.Core
{
    public class SaveLoad
    {
        public static readonly string currentFoldierPath;
        public static string fileExtension { get; set; }

        static SaveLoad()
        {
            currentFoldierPath = Environment.CurrentDirectory;
            fileExtension = ".th";
        }

        class ErrorMessage
        {
            static string result;

            public static string Convert(SerializationException e)
            {
                return (e.Message);
            }

            public static string Convert(IOException e)
            {
                return (e.Message);
            }

            public static string Convert(Exception e)
            {
                return (e.Message);
            }
        }


        public enum Mod
        {
            Save,
            Load,
        }

        public static bool SaveData(string FILE_NAME, object gathereddata)
        {
            Operation operation = new Operation("SaveData(string FILE_NAME, object gathereddata):bool");
            OperationsHandler.OperationStarts(operation);
            Boolean result = true;
            try
            {
                FileStream fs = new FileStream(currentFoldierPath + "\\" + FILE_NAME + fileExtension, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, gathereddata);
                }
                catch (SerializationException e)
                {
                    throw new SerializationException(LanguageResource.currentDictionary["errorSavingData"] + ErrorMessage.Convert(e), e);
                }
                finally
                {
                    fs.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(LanguageResource.currentDictionary["errorSavingData"] + ErrorMessage.Convert(e), e);
            }
            OperationsHandler.OperationComlete(operation);
            return (result);
        }

        public static bool SaveData(string fileName, string filePath, string fileExt, object gathereddata)
        {
            Operation operation = new Operation("SaveData(string fileName, string filePath, string fileExt, object gathereddata):bool");
            OperationsHandler.OperationStarts(operation);

            Boolean result = true;
            FileStream fs = null;
            StreamWriter fss = null;
           
            fileName = fileName.Replace(".","_").Replace(" ","__").Replace(":","_");

            if (fileExt != "")
            {
                if (!fileExt.Contains("."))
                    fileExt = "." + fileExt;
            }
            else
            {
                filePath = fileExtension;
            }

            if (filePath != "")
            {
                if (!filePath.Contains(":\\"))
                    filePath = currentFoldierPath + "\\" + filePath;
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
            }

            string finalPath = filePath + "\\" + fileName + fileExt;

            try
            {
                if (gathereddata is string)
                {
                    using (fss = File.AppendText(finalPath))
                    {
                        fss.Write((string)gathereddata);
                    }
                }
                else
                {
                    using (fs = new FileStream(finalPath, FileMode.Create))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(fs, gathereddata);
                    }
                }
            }

            catch (Exception e)
            {
                throw new Exception(LanguageResource.currentDictionary["errorSavingData"] + ErrorMessage.Convert(e), e);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
                if (fss != null)
                    fss.Close();
            }
            OperationsHandler.OperationComlete(operation);
            return (result);
        }

        public static bool LoadData(string fileName, out object ob)
        {
            Operation operation = new Operation("LoadData(string fileName, out object ob):bool");
            OperationsHandler.OperationStarts(operation);

            Boolean result = true;
            
            try
            {
                if (!fileName.Contains(fileExtension))
                    fileName += fileExtension;
                FileStream fs = new FileStream(currentFoldierPath + "\\" + fileName, FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    ob = formatter.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    throw new SerializationException(LanguageResource.currentDictionary["errorLoadingData"] + ErrorMessage.Convert(e), e);
                }
                finally
                {
                    fs.Close();
                }
            }
            catch (Exception e)
            {
                throw new SerializationException(LanguageResource.currentDictionary["errorLoadingData"], e);
            }
            OperationsHandler.OperationComlete(operation);
            return (result);
        }

        public static object LoadData(string fileName, string filePath, string fileExt)
        {
            Operation operation = new Operation("LoadData(string fileName, string filePath, string fileExt):object");
            OperationsHandler.OperationStarts(operation);

            object loadedData = null;
            try
            {
                if (!fileName.Contains(fileExtension))
                    fileName += fileExtension;

                if (fileExt != "")
                {
                    if (!fileExt.Contains("."))
                        fileExt = "." + fileExt;
                }

                if (!filePath.Contains(":\\"))
                    filePath = currentFoldierPath + "\\" + filePath;

                FileStream fs = new FileStream(filePath + "\\" + fileName + 
                                               ((fileExt == "")? fileExtension : fileExt), FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    loadedData = formatter.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    throw new SerializationException(LanguageResource.currentDictionary["errorLoadingData"] + ErrorMessage.Convert(e), e);
                }
                finally
                {
                    fs.Close();
                }
            }
            catch (Exception e)
            {
                throw new SerializationException(LanguageResource.currentDictionary["errorLoadingData"], e);
            }
            OperationsHandler.OperationComlete(operation);
            return (loadedData);
        }

        public static string LoadData(string fileName)
        {
            Operation operation = new Operation("LoadData(string fileName):string");
            OperationsHandler.OperationStarts(operation);

            string text = null;
            try
            {
                if (!File.Exists(fileName))
                    throw new IOException(LanguageResource.currentDictionary["errorFileNotExists"]);

                using (StreamReader reader = new StreamReader(fileName))
                {
                    text = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw new SerializationException(LanguageResource.currentDictionary["errorLoadingData"], e);
            }
            OperationsHandler.OperationComlete(operation);
            return text;
        }

        public static bool DeleteFile(string fileName)
        {
            Operation operation = new Operation("DeleteData");
            OperationsHandler.OperationStarts(operation);

            Boolean result = true;
            try
            {
                FileInfo f = new FileInfo(fileName);
                f.Delete();
            }
            catch (Exception e)
            {
                throw new SerializationException(LanguageResource.currentDictionary["errorDeletingData"] + ErrorMessage.Convert(e), e);
            }
            OperationsHandler.OperationComlete(operation);
            return (result);
        }
        //public static T CreateShallowCopy<T>(T o)
        //{
        //    MethodInfo memberwiseClone = o.GetType().
        //    GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);

        //    return (T)memberwiseClone.Invoke(o, null);
        //}
        //public static T CreateDeepCopy<T>(T o)
        //{
        //    T copy = CreateShallowCopy(o);
        //    foreach (FieldInfo f in typeof(T).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        //    {
        //        object original = f.GetValue(o);
        //        f.SetValue(copy, CreateDeepCopy(original));
        //    }
        //    return copy;
        //}
        public static string[] GetNamesOfSavedFiles()
        {
            return (Directory.GetFiles(currentFoldierPath + "/saves/", "*" + fileExtension));
        }
    }
}
