using Microsoft.AspNetCore.Mvc;

namespace Application.Route
{
    public class DeleteProductRoute
    {
        [FromRoute(Name = "id")] public int Id { get; set; }
    }
}
