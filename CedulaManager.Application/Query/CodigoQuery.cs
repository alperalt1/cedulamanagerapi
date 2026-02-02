using CedulaManager.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Application.Query
{
    public record CodigoQuery(
        string Codigo
    ) : IRequest<BaseResponse<bool>>;
}
