//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MeSegundaUnidade
{
    using System;
    using System.Collections.Generic;
    
    public partial class funcionario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public funcionario()
        {
            this.registroDaLavagems = new HashSet<registroDaLavagem>();
        }
    
        public System.Guid id { get; set; }
        public string nome { get; set; }
        public Nullable<System.DateTimeOffset> datadenascimento { get; set; }
        public string cpf { get; set; }
        public string endereco { get; set; }
        public Nullable<bool> Disponivel { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<registroDaLavagem> registroDaLavagems { get; set; }
    }
}
