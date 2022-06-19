using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cursomongo.Api.Controllers.Inputs;
using Cursomongo.Api.Data.Repositories;
using Cursomongo.Api.Domain.Entities;
using Cursomongo.Api.Domain.Enums;
using Cursomongo.Api.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cursomongo.Api.Controllers
{
    [ApiController]
    public class RestauranteController : ControllerBase
    {
        private readonly RestauranteRepository _restauranteRepository;

        public RestauranteController(RestauranteRepository restauranteRepository)
        {
            _restauranteRepository = restauranteRepository;
        }

        [HttpPost("restaurante")]
        public ActionResult IncluirRestaurante([FromBody] RestauranteInclusao restauranteInclusao)
        {
            var cozinha = ECozinhaHelper.ConverterDeInteiro(restauranteInclusao.Cozinha);
            var restaurante = new Restaurante(restauranteInclusao.Nome, cozinha);

            var endereco = new Endereco(
                restauranteInclusao.Logradouro,
                restauranteInclusao.Numero,
                restauranteInclusao.Cidade,
                restauranteInclusao.UF,
                restauranteInclusao.Cep);

            restaurante.AtribuirEndereco(endereco);

            if (!restaurante.Validar())
            {
                return BadRequest(
                    new
                    {
                        errors = restaurante.ValidationResult.Errors.Select(_ => _.ErrorMessage)
                    });
            }

            _restauranteRepository.Inserir(restaurante);

            return Ok(
                new
                {
                    data = "Restaurante inserido com sucesso"
                }
            );
        }
    }
}
