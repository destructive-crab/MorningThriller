using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

public class FileDataHandler
{
    private readonly string dataDirPath;
    private readonly string dataFileName;
    
    private bool useEncryption = false;
    
    private readonly string encryptionCodeWord = "word";

    public FileDataHandler(string dataDirPath, string dataFileName) 
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public TObject Load<TObject>()
        where TObject : class
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        TObject loadedData = null;

        if (File.Exists(fullPath)) 
        {
            try 
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (useEncryption) 
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadedData = JsonConvert.DeserializeObject<TObject>(dataToLoad);
            }
            catch (Exception e) 
            {
                Debug.LogError("Error occured when trying to load file at path: " 
                               + fullPath  + " and backup did not work.\n" + e);
            }
        }
        
        return loadedData;
    }

    public void Save(object data) 
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try 
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            
            string dataToStore = JsonConvert.SerializeObject(data);

            if (useEncryption) 
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream)) 
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e) 
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    public void Delete(string profileId) 
    {
        if (profileId == null) 
        {
            return;
        }

        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        try 
        {
            if (File.Exists(fullPath)) 
            {
                Directory.Delete(Path.GetDirectoryName(fullPath), true);
            }
            else 
            {
                Debug.LogWarning("Tried to delete profile data, but data was not found at path: " + fullPath);
            }
        }
        catch (Exception e) 
        {
            Debug.LogError("Failed to delete profile data for profileId: " 
                + profileId + " at path: " + fullPath + "\n" + e);
        }
    }

    private string EncryptDecrypt(string data) 
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++) 
        {
            modifiedData += (char) (data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
