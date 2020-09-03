using Newtonsoft.Json;
using System;

namespace CosmosDB.Domain
{
    public class Cliente
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        [JsonProperty(PropertyName = "nome")]
        public string Nome { get; set; }

        [JsonProperty(PropertyName = "sexo")]
        public string Sexo { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "endereco")]
        public Endereco Endereco { get; set; }
    }

    public class Endereco
    {
        [JsonProperty(PropertyName = "logradouro")]
        public string Logradouro { get; set; }
        [JsonProperty(PropertyName = "numero")]
        public string Numero { get; set; }
        [JsonProperty(PropertyName = "bairro")]
        public string Bairro { get; set; }
        [JsonProperty(PropertyName = "cidade")]
        public string Cidade { get; set; }
        [JsonProperty(PropertyName = "estado")]
        public string Estado { get; set; }
    }
}
