using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTOs.Category
{
    public class CheckBoxDTO
    {
        public bool SelectAll { get; set; } = false;
        public List<CategoryDTO> Categories { get; set; } 
    }
}
