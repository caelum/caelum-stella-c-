﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CaelumStellaCSharp.http
{
    public class ViaCEP
    {
        public ViaCEP()
        {
        }

        public Endereco GetEndereco(string cep)
        {
            var json = GetEndereco(cep, OutputType.Json);
            return TryConvertToEndereco(
                JsonConvert.DeserializeObject<Endereco>(json) as Endereco);
        }

        public async Task<Endereco> GetEnderecoAsync(string cep)
        {
            var json = await GetEnderecoAsync(cep, OutputType.Json);
            return TryConvertToEndereco(
                JsonConvert.DeserializeObject<Endereco>(json) as Endereco);
        }

        private static Endereco TryConvertToEndereco(Endereco endereco)
        {
            if (endereco.IsValid())
            {
                return endereco;
            }
            else
            {
                throw new ZipCodeDoesNotExist();
            }
        }

        public async Task<string> GetEnderecoXmlAsync(string cep)
        {
            return await GetEnderecoAsync(cep, OutputType.Xml);
        }

        public string GetEnderecoXml(string cep)
        {
            return GetEndereco(cep, OutputType.Xml);
        }

        public async Task<string> GetEnderecoJsonAsync(string cep)
        {
            return await GetEnderecoAsync(cep, OutputType.Json);
        }

        public string GetEnderecoJson(string cep)
        {
            return GetEndereco(cep, OutputType.Json);
        }

        public async Task<string> GetEnderecoPipedAsync(string cep)
        {
            return await GetEnderecoAsync(cep, OutputType.Piped);
        }

        public string GetEnderecoPiped(string cep)
        {
            return GetEndereco(cep, OutputType.Piped);
        }

        public async Task<string> GetEnderecoQuertyAsync(string cep)
        {
            return await GetEnderecoAsync(cep, OutputType.Querty);
        }

        public string GetEnderecoQuerty(string cep)
        {
            return GetEndereco(cep, OutputType.Querty);
        }

        private static async Task<string> GetEnderecoAsync(string cep, string outputType)
        {
            return await GetStringResponseAsync(string.Format("https://viacep.com.br/ws/{0}/{1}/", cep, outputType));
        }

        private static string GetEndereco(string cep, string outputType)
        {
            return GetStringResponse(string.Format("https://viacep.com.br/ws/{0}/{1}/", cep, outputType));
        }

        private static async Task<string> GetStringResponseAsync(string url)
        {
            using (var client = new HttpClient())
            {
                using (var r = await client.GetAsync(new Uri(url)))
                {
                    return await r.Content.ReadAsStringAsync();
                }
            }
        }

        private static string GetStringResponse(string url)
        {
            using (var client = new HttpClient())
            {
                using (var r = client.GetAsync(new Uri(url)).Result)
                {
                    return r.Content.ReadAsStringAsync().Result;
                }
            }
        }
    }
}