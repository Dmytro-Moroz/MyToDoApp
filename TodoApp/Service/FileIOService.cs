using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TodoApp.Model;

namespace TodoApp.Service
{
    class FileIOService
    {
        private readonly string PATH;

        public FileIOService(string path)
        {
            PATH = path;
        }

        public BindingList<MainModel> LoadData()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<MainModel>();
            }

            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<MainModel>>(fileText);
            }
        }

        public void SaveData(object toDoModelData)
        {
            using (StreamWriter sw = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(toDoModelData);
                sw.Write(output);
            }
        }
    }
}
