using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CayTrongMVC.Models
{
    public class CayTrong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Ma { get; set; }

        [StringLength(50, ErrorMessage = "Tên cây quá dài!")]
        [RegularExpression(@"^Cây.*", ErrorMessage = "Tên cây phải bắt đầu bằng từ 'Cây'.")]
        public string Ten { get; set; }

        [MinLength(10, ErrorMessage = "Mô tả quá ngắn!")]
        public string MoTa { get; set; }

        [RegularExpression(@"^(https?:\/\/[\w\-]+(\.[\w\-]+)+.*\.(jpg|jpeg|png|gif|bmp|webp))$|(^images\/[\w\-]+\.(jpg|jpeg|png|gif|bmp|webp)$)",
            ErrorMessage = "Vui lòng nhập URL hợp lệ hoặc đường dẫn từ thư mục 'images/'.")]
        public string HinhAnh { get; set; }
    }
}
