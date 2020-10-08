using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using WebMotors.Anuncio.External.Impl;
using WebMotors.Anuncio.External.Model;
using Xunit;

namespace WebMotors.Anuncio.External.Test
{
    public class CarsClientTests
    {
        private readonly ICarsClient client;

        public CarsClientTests()
        {
            client = new DefaultCarsClient();
        }


        [Fact]
        public void GetMarcasTest()
        {
            var result = client.MarcaAsync(new ViewModel.MarcasRequestModel(), CancellationToken.None).Result;
            Assert.Equal(HttpStatusCode.OK, result.ResponseStatus);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void GetModelosTest(int marcaID)
        {
            var result = client.ModeloAsync(new ViewModel.ModeloRequestModel { MarcaID = marcaID }, CancellationToken.None).Result;
            Assert.Equal(HttpStatusCode.OK, result.ResponseStatus);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(6)]
        [InlineData(5)]
        public void GetVersoesTest(int modeloID)
        {
            var result = client.VersaoAsync(new ViewModel.VersaoRequestModel { ModeloID = modeloID }, CancellationToken.None).Result;
            Assert.Equal(HttpStatusCode.OK, result.ResponseStatus);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(18)]
        [InlineData(7)]
        public void GetVeiculosTest(int pagina)
        {
            var result = client.VeiculoAsync(new ViewModel.VeiculoRequestModel { Pagina = pagina }, CancellationToken.None).Result;
            Assert.Equal(HttpStatusCode.OK, result.ResponseStatus);
        }
    }
}
