using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebMotors.Anuncio.External.Model
{
    public class Versao
    {
        [JsonProperty(PropertyName = "ModelID")]
        public int ModeloID { get; set; }

        [JsonProperty(PropertyName = "ID")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "Nome")]
        public string Name { get; set; }
    }
}
