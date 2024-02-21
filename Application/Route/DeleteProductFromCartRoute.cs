
using Microsoft.AspNetCore.Mvc;

namespace Application.Route
{
    public class DeleteProductFromCartRoute
    {
        [FromRoute(Name = "id")] public int Id { get; set; }
    }
}
