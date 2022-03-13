﻿using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using OrganizaTudo.Models;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace OrganizaTudo.Controllers
{
    public class NotasController
    {
        public static readonly string baseURL = "https://webhooks.mongodb-realm.com/api/client/v2.0/app/organiza-tudo-luhho/service/API/incoming_webhook";

        public async Task<List<Nota>> BuscarNotas(string Token)
        {
            try
            {
                RestClient client = new RestClient(baseURL);
                RestRequest request = new RestRequest("buscarNotas", Method.POST);
                request.AddHeader("Authorization", Token);
                //request.AddParameter("Authorization", Token, ParameterType.HttpHeader);

                IRestResponse response = client.Execute<object>(request);
                if (response.IsSuccessful)
                {
                    List<Nota> notas = JsonConvert.DeserializeObject<List<Nota>>(response.Content, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    });
                    return notas;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Nota>> PesquisarNotas(string Token)
        {
            try
            {
                RestClient client = new RestClient(baseURL);
                RestRequest request = new RestRequest("buscarNotas", Method.POST);
                
                request.AddHeader("Authorization", Token);

                IRestResponse response = client.Execute<object>(request);
                if (response.IsSuccessful)
                {
                    List<Nota> notas = JsonConvert.DeserializeObject<List<Nota>>(response.Content, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    });
                    return notas;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InserirNota(string Token, Nota nota)
        {
            try
            {
                RestClient client = new RestClient(baseURL);
                RestRequest request = new RestRequest("inserirNota", Method.POST);

                request.AddJsonBody(new { nota = nota });
                request.AddHeader("Authorization", Token);
                // request.AddParameter("application/json; charset=utf-8", JObject.Parse("{ nota: { \"titulo\": \"" + nota.titulo + "\" , \"nota\": \"" + nota.nota + "\" } }"), ParameterType.RequestBody);

                IRestResponse response = client.Execute<object>(request);

                if (response.IsSuccessful && response.Content.Equals("200")) return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EditarNota(string Token, Nota nota, string notaID)
        {
            try
            {
                RestClient client = new RestClient(baseURL);
                RestRequest request = new RestRequest("editarNota", Method.POST);

                request.AddJsonBody(new { notaID = notaID, notaNova = nota });;
                request.AddHeader("Authorization", Token);

                IRestResponse response = client.Execute<object>(request);

                if (response.IsSuccessful && response.Content.Equals("\"200\"")) return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeletarNota(string Token, string notaID)
        {
            try
            {
                RestClient client = new RestClient(baseURL);
                RestRequest request = new RestRequest("deletarNota", Method.POST);
                
                request.AddJsonBody(new { notaID = notaID });
                request.AddHeader("Authorization", Token);

                IRestResponse response = client.Execute<object>(request);

                if (response.IsSuccessful && response.Content.Equals("\"200\"")) return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AtualizarPrivacidadeNota(string Token, string notaID, bool publica)
        {
            try
            {
                bool privacidade = !publica;
                RestClient client = new RestClient(baseURL);
                RestRequest request = new RestRequest("atualizarPrivacidadeNota", Method.POST);

                request.AddJsonBody(new { privacidade = privacidade, notaID = notaID });
                request.AddHeader("Authorization", Token);

                IRestResponse response = client.Execute<object>(request);

                if (response.IsSuccessful && response.Content.Equals("\"200\"")) return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
