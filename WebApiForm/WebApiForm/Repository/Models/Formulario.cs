using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiForm.Repository.Models;

[Table("Formulario")]
public partial class Formulario
{
    [Key]
    [Column("identifacador_form")]
    public int? IdentifacadorForm { get; set; }

    [Column("id_usuarios")]
    [StringLength(100)]
    [Unicode(false)]
    public string IdUsuarios { get; set; } = null!;

    [Column("fecha")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Fecha { get; set; }

    [Column("hora")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Hora { get; set; }

    [Column("id_estacion")]
    public int? IdEstacion { get; set; }

    [Column("id_linea")]
    [StringLength(20)]
    [Unicode(false)]
    public string? IdLinea { get; set; }

    [ForeignKey("IdEstacion")]
    [InverseProperty("Formularios")]
    public virtual Estacion? IdEstacionNavigation { get; set; }

    [ForeignKey("IdLinea")]
    [InverseProperty("Formularios")]
    public virtual Linea? IdLineaNavigation { get; set; }

    [ForeignKey("IdUsuarios")]
    [InverseProperty("Formularios")]
    public virtual RegistroUsuario? IdUsuariosNavigation { get; set; }

    [InverseProperty("IdentifacadorFormNavigation")]
    public virtual ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
}
