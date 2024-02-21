using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class CartDto
    {
        public int Id { get; private set; }
        public int ProductId { get; set; }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
