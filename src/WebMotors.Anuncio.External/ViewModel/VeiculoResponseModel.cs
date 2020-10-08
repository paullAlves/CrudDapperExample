using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WebMotors.Anuncio.External.Model;

namespace WebMotors.Anuncio.External.ViewModel
{
    public class VeiculoResponseModel
    {
        public IList<Veiculo> data { get; set; }
        public HttpStatusCode ResponseStatus { get; set; }
    }
}
