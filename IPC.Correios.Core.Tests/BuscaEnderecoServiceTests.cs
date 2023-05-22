using System;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using FluentAssertions;
using IPC.Correios.Application.Data;
using IPC.Correios.Application.Services;
using IPC.Correios.Core.Data;
using IPC.Correios.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using RichardSzalay.MockHttp;

namespace IPC.Correios.Middleware.Core.Tests
{
    //[TestClass]
    [TestFixture]
    public class BuscaEnderecoServiceTests
    {
        private ILocalidadesRepository _localidadesRepository;
        private IEnderecoRepository _enderecoRepository;
        private IBuscaEnderecoService _buscaEnderecoService;
        private IEstadosService _estadosService;

        [SetUp]
        public void InitService()
        {
            _estadosService = new EstadosService();
            _localidadesRepository = new LocalizadesRepository();
            _enderecoRepository = new EnderecoRepository(_localidadesRepository);
            _buscaEnderecoService = new BuscaEnderecoService(_enderecoRepository, _estadosService, _localidadesRepository);
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"bin/Debug/App_Data");
            _enderecoRepository.SetAppDataPath(path);
            _localidadesRepository.SetAppDataPath(path);

            var pathEstados = Path.Combine(path, @"Correios/retorno_estados.json");
            var _msgHandler = new MockHttpMessageHandler();
            _msgHandler.When("https://servicodados.ibge.gov.br/api/v1/localidades/estados").Respond("application/json", File.ReadAllText(pathEstados));

            _estadosService.SetHttpClient(_msgHandler.ToHttpClient());
            
        }

        [Test]
        public void Should_ReturnEndereco_When_CepIsValid()
        {
            //Arrange
            var cep = "89254690";

            //Act
            var endereco = _buscaEnderecoService.BuscarEnderecoByCep(cep);

            //Assert
            endereco.Should().NotBe(null);
            endereco.Bairro.Should().Be(String.Empty);
            endereco.Cep.Should().Be(cep);
            endereco.CodLogradouro.Should().Be("429952");
            endereco.CodMunicipio.Should().Be("8518");
            endereco.Logradouro.Should().Be("R Bento S Anacleto                  ");
            endereco.UF.Should().Be("SC");
            endereco.Municipio.Should().Be("Jaraguá do Sul");
        }

        [Test]
        public void Should_ReturnNull_When_CepIsInvalid()
        {
            //Arrange
            var cep = "03589001";

            //Act
            var endereco = _buscaEnderecoService.BuscarEnderecoByCep(cep);

            //Assert
            endereco.Should().Be(null);
        }


        [Test]
        public void Should_ReturnMunicipios_When_UFIsValid()
        {
            //Arrange
            var uf = "SC";

            //Act
            var municipios = _buscaEnderecoService.BuscaMunicipiosByUf(uf);

            //Assert
            municipios.Count.Should().Be(451);
        }

        [Test]
        public void Should_ReturnNull_When_UFIsInvalid()
        {
            //Arrange
            var uf = "SS";

            //Act
            var municipios = _buscaEnderecoService.BuscaMunicipiosByUf(uf);

            //Assert
            municipios.Count.Should().Be(0);
        }

        [Test]
        public void Should_ReturnEnderecos_When_CodMunicipioIsValid()
        {
            //Arrange
            var codMunicipio = "8518";

            //Act
            var enderecos = _buscaEnderecoService.BuscarEnderecosByCodMunicipio(codMunicipio);

            //Assert
            enderecos.Count.Should().Be(2002);
        }

        [Test]
        public void Should_ReturnNull_When_CodMunicipioIsInvalid()
        {
            //Arrange
            var codMunicipio = "851866";

            //Act
            var enderecos = _buscaEnderecoService.BuscarEnderecosByCodMunicipio(codMunicipio);

            //Assert
            enderecos.Count.Should().Be(0);
        }

        [Test]
        public void Should_ReturnEnderecos_When_CodLogradouroIsValid()
        {
            //Arrange
            var codLogradouro = "429952";

            //Act
            var endereco = _buscaEnderecoService.GetEnderecoByCodLogradouro(codLogradouro);

            //Assert
            endereco.Should().NotBe(null);
            endereco.Bairro.Should().Be(String.Empty);
            endereco.Cep.Should().Be("89254690");
            endereco.CodLogradouro.Should().Be(codLogradouro);
            endereco.CodMunicipio.Should().Be("8518");
            endereco.Logradouro.Should().Be("R Bento S Anacleto                  ");
            endereco.UF.Should().Be("SC");
            endereco.Municipio.Should().Be("Jaraguá do Sul");
        }

        [Test]
        public void Should_ReturnNull_When_CodLogradouroIsInvalid()
        {
            //Arrange
            var codLogradouro = "429952342";

            //Act
            var endereco = _buscaEnderecoService.GetEnderecoByCodLogradouro(codLogradouro);

            //Assert
            endereco.Should().Be(null);
        }

        [TestCase("AC")]
        [TestCase("AL")]
        [TestCase("AP")]
        [TestCase("AM")]
        [TestCase("BA")]
        [TestCase("CE")]
        [TestCase("DF")]
        [TestCase("ES")]
        [TestCase("GO")]
        [TestCase("MA")]
        [TestCase("MS")]
        [TestCase("MT")]
        [TestCase("MG")]
        [TestCase("PA")]
        [TestCase("PB")]
        [TestCase("PR")]
        [TestCase("PE")]
        [TestCase("PI")]
        [TestCase("RJ")]
        [TestCase("RN")]
        [TestCase("RS")]
        [TestCase("RO")]
        [TestCase("RR")]
        [TestCase("SC")]
        [TestCase("SP")]
        [TestCase("SE")]
        [TestCase("TO")]
        public void Should_ReturnEstados(string uf)
        {
            //Arrange
            

            //Act
            var estados = _buscaEnderecoService.GetEstados();

            //Assert
            estados.Count.Should().Be(27);
            estados.Select(e => e.Sigla).ToList().Should().Contain(uf);
        }

    }
}
