using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOMAIN.Interface;
using DOMAIN.Models;
using Newtonsoft.Json;

namespace DAL.Services
{
    public class SmileIdService : ISmileIdService
    {

        public SmileIdService()
        {

        }

        public void WriteObjectToFile2(object obj)
        {
            List<object> objects;

            var filePath = Directory.GetCurrentDirectory() + "\\MPesaCallbacks\\Callbacks.txt";
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                objects = System.Text.Json.JsonSerializer.Deserialize<List<object>>(json) ?? new List<object>();
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                objects = new List<object>();
            }

            objects.Add(obj);

            var updatedJson = JsonConvert.SerializeObject(objects, Formatting.Indented); //System.Text.Json.JsonSerializer.Serialize(JsonConvert.SerializeObject(objects), new JsonSerializerOptions { WriteIndented = false });
            File.WriteAllText(Path.GetFullPath(filePath), updatedJson);
        }

        public object ReadObjectFromFile(string smileJobId)
        {
            var filePath = Directory.GetCurrentDirectory() + "\\SmileRequests\\Callbacks.txt";

            var count = 0;

            //do {

            //    Thread.Sleep(1000);
            //    count ++;
            //} while(!File.Exists(filePath) || count < 300); 

            if (!File.Exists(filePath))
            {
                return null;
            }

            var json = File.ReadAllText(filePath);
            var objects = System.Text.Json.JsonSerializer.Deserialize<List<SmileIDCallBackResponse>>(json);
            var myobj = objects?.Find(obj => obj.PartnerParams?.job_id == smileJobId);

            count = 0;
            if (myobj == null)
            {
                do
                {

                    Thread.Sleep(1000);
                    json = File.ReadAllText(filePath);
                    objects = System.Text.Json.JsonSerializer.Deserialize<List<SmileIDCallBackResponse>>(json);
                    myobj = objects?.Find(obj => obj.PartnerParams?.job_id == smileJobId);

                    count++;
                } while (count < 3000 && myobj == null);
            }
            return myobj;
        }

    }
}
