using Microsoft.AspNetCore.Mvc;

namespace Application.Route
{
    public class UpdateProductRoute
    {
        [FromRoute(Name = "id")] public int Id { get; set; }
    }
}
