using Microsoft.AspNetCore.Mvc;

namespace Application.Route
{
    public class GetProductRoute
    {
        [FromRoute(Name = "id")] public int Id { get; set; }
    }
}
