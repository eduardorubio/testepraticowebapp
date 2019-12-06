using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TestePraticoWebApp.Models
{
    public class UsuarioViewModel
    {
        [Display(Name ="CPF")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$", ErrorMessage = "Formato do {0} é inválido (XXX.XXX.XXX-XX)")]
        [Remote("ValidaCpf", "Usuarios", ErrorMessage = "{0} não é válido")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Nome { get; set; }
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [EmailAddress(ErrorMessage = "{0} não é válido")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!\"#$%&'()*+,-./:;?@[\\\\\\]_`{|}~])[\\da-zA-Z!\"#$%&'()*+,-./:;?@[\\\\\\]_`{|}~]{4,10}$",
            ErrorMessage = "Senha deve ter de 4 à 10 caracteres, com no mínimo um número, uma letra maiúscula e um caractere especial.")]
        public string Senha { get; set; }
        [Display(Name = "Confirme à Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [Compare("Senha", ErrorMessage = "'{0}' e '{1}' devem ser iguais")]
        public string ConfirmeSenha { get; set; }
    }
}
