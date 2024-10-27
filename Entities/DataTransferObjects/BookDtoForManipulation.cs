using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public abstract record BookDtoForManipulation
    {
        [Required(ErrorMessage ="Başlık Zorunlu Alan")]
        [MinLength(2,ErrorMessage ="Başlık en az 2 karakter olmalıdır")]
        [MaxLength(50,ErrorMessage ="başlık en çok 50 karakter olmalıdır")]
        public string  Title { get; init; }

        [Required(ErrorMessage = "Fiyat Zorunlu Alan")]
        [Range(10,1000)]
        public decimal Price { get; init; }


    }
}
