using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebMotors.Anuncio.External.Model
{
    public class Veiculo
    {
        [JsonProperty(PropertyName = "ID")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "Make")]
        public string Marca { get; set; }

        [JsonProperty(PropertyName = "Model")]
        public string Modelo { get; set; }

        [JsonProperty(PropertyName = "Version")]
        public string Versao { get; set; }

        [JsonProperty(PropertyName = "Image")]
        public string Imagem { get; set; }

        [JsonProperty(PropertyName = "KM")]
        public string Quilometragem { get; set; }

        [JsonProperty(PropertyName = "Price")]
        public string Preco { get; set; }

        [JsonProperty(PropertyName = "YearModel")]
        public int AnoModelo { get; set; }

        [JsonProperty(PropertyName = "YearFab")]
        public int AnoFabricacao { get; set; }

        [JsonProperty(PropertyName = "Color")]
        public string Cor { get; set; }
    }
}
