using System;
using System.Collections.Generic;
using System.Text;
using clientes_api.Entidades;
using Microsoft.EntityFrameworkCore;

namespace clientes_api
{
    public class Contexto : DbContext
    { 
        public DbSet<Cliente> Clientes { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options){}
    }
}
