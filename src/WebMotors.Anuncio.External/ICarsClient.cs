using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebMotors.Anuncio.External.ViewModel;

namespace WebMotors.Anuncio.External
{
    public interface ICarsClient
    {
        Task<MarcasResponseModel> MarcaAsync(MarcasRequestModel request, CancellationToken cancellationToken);
        Task<ModeloResponseModel> ModeloAsync(ModeloRequestModel request, CancellationToken cancellationToken);
        Task<VersaoResponseModel> VersaoAsync(VersaoRequestModel request, CancellationToken cancellationToken);
        Task<VeiculoResponseModel> VeiculoAsync(VeiculoRequestModel request, CancellationToken cancellationToken);
    }
}
