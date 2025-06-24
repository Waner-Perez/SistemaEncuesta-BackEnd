﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiForm.Repository.Models;

[Index("Usuario", Name = "UQ__Registro__9AFF8FC6198A6C6B", IsUnique = true)]
[Index("Email", Name = "UQ__Registro__AB6E616493B341D1", IsUnique = true)]
public partial class RegistroUsuario
{
    [Key]
    [Column("id_usuarios")]
    [StringLength(100)]
    [Unicode(false)]
    public string? IdUsuarios { get; set; }

    [Column("nombre_apellido")]
    [StringLength(200)]
    [Unicode(false)]
    public string NombreApellido { get; set; } = null!;

    [Column("usuario")]
    [StringLength(50)]
    [Unicode(false)]
    public string Usuario { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("passwords")]
    [StringLength(300)]
    [Unicode(false)]
    public string Passwords { get; set; } = null!;

    [Column("foto")]
    public byte[]? Foto { get; set; }

    [Column("fecha_creacion")]
    [StringLength(100)]
    [Unicode(false)]
    public string? FechaCreacion { get; set; }

    [Column("rol")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Rol { get; set; }

    [InverseProperty("IdUsuariosNavigation")]
    public virtual ICollection<Formulario> Formularios { get; set; } = new List<Formulario>();

    [InverseProperty("IdUsuariosNavigation")]
    public virtual ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = new List<PasswordResetToken>();

    [InverseProperty("IdUsuariosNavigation")]
    public virtual ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
}
