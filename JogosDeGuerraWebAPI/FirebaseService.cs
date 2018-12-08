using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using JogosDeGuerraModel;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace JogosDeGuerraWebAPI
{

    public class FirebaseService<T> where T : class
    {

        private static readonly IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "6vgkYE61BZrQWBMYgdOdRDlzkwukwkKUKe9M0fni",
            BasePath = "https://jogosdeguerraesparta.firebaseio.com/"
        };

        public void Add(T data, int id)
        {
            IFirebaseClient client = new FirebaseClient(config);
            string path = $"{data.ToString()}/{id}";

            SetResponse response = client.SetAsync(path, data).Result;
        }

        public void Update(T data, int id)
        {
            IFirebaseClient client = new FirebaseClient(config);
            string path = $"{data.ToString()}/{id}";

            FirebaseResponse response = client.UpdateAsync(path, data).Result;
        }
    }
    
    
}