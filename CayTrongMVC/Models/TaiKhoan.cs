using System.ComponentModel.DataAnnotations;

namespace CayTrongMVC.Models
{
    public class TaiKhoan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TenDangNhap { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; } = string.Empty;

        public bool IsAdmin { get; set; } 
    }
}