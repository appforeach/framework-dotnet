using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EscapeHit.WebApi
{
    public interface IOperationResult
    {
        Task<ObjectResult> Ok();

        Task<ObjectResult> OkOrNotFound();
    }
}
