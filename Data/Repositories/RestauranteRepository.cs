using System;
using Cursomongo.Api.Domain.Entities;
using CursoMongo.Api.Data.Schemas;
using MongoDB.Driver;

namespace Cursomongo.Api.Data.Repositories
{
    public class RestauranteRepository
    {
        // criei um obj do tipo IMongoCollection para o tipo <RestauranteSchema>
        IMongoCollection<RestauranteSchema> _restaurantes;

        public RestauranteRepository(MongoDB mongoDB)
        {
            _restaurantes = mongoDB.DB.GetCollection<RestauranteSchema>("restaurantes");
        }

        public void Inserir(Restaurante restaurante)
        {
            var document = new RestauranteSchema
            {
                Nome = restaurante.Nome,
                Cozinha = restaurante.Cozinha,
                Endereco = new EnderecoSchema
                {
                    Logradouro = restaurante.Endereco.Logradouro,
                    Numero = restaurante.Endereco.Numero,
                    Cidade = restaurante.Endereco.Cidade,
                    Cep = restaurante.Endereco.Cep,
                    UF = restaurante.Endereco.UF
                }
            };

            _restaurantes.InsertOne(document);
        }
    }
}
