﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiForm.Repository.Models;

public partial class SubPregunta
{
    [Key]
    [Column("cod_subPregunta")]
    [StringLength(100)]
    [Unicode(false)]
    public string CodSubPregunta { get; set; } = null!;

    [Column("sub_preguntas")]
    [StringLength(255)]
    [Unicode(false)]
    public string? SubPreguntas { get; set; }

    [InverseProperty("CodSubPreguntaNavigation")]
    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();
}
