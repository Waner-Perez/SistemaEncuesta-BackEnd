using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiForm.Repository.Models;

[Table("Estacion")]
public partial class Estacion
{
    [Key]
    [Column("id_estacion")]
    public int IdEstacion { get; set; }

    [Column("id_linea")]
    [StringLength(20)]
    [Unicode(false)]
    public string IdLinea { get; set; } = null!;

    [Column("nombre_estacion")]
    [StringLength(255)]
    [Unicode(false)]
    public string? NombreEstacion { get; set; }

    [InverseProperty("IdEstacionNavigation")]
    public virtual ICollection<Formulario> Formularios { get; set; } = new List<Formulario>();

    [ForeignKey("IdLinea")]
    [InverseProperty("Estacions")]
    public virtual Linea? IdLineaNavigation { get; set; }
}
