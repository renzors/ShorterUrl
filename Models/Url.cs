using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace ExamMinEdu.Models
{
    public class Url
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required]
        [DataType(DataType.Url, ErrorMessage="Ingrese una URL válida.")]
        [Display(Name = "URL Origen")]
        public string UrlOrigen { get; set; }

        [Required]
        [Display(Name = "URL Destino")]
        public string UrlDestino { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Creación")]
        public DateTime FCreacion { get; set; }

        [Display(Name = "Número de Visitas")]
        public int NVisitas { get; set; }
    }
    
    public class LocalContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            List<DbValidationError> validationErrors = new List<DbValidationError>();
            
            if (entityEntry.Entity is Url)
            {
                Url objurl = entityEntry.Entity as Url;

                var existingLocation = (from l in direcciones
                                        where l.UrlDestino == objurl.UrlDestino && l.ID != objurl.ID
                                        select l).FirstOrDefault();

                if (existingLocation != null)
                {
                    validationErrors.Add(new DbValidationError("UrlDestino", "Ya existe una URL corta con el mismo nombre '" + objurl.UrlDestino + "'"));
                    return new DbEntityValidationResult(entityEntry, validationErrors);
                }
            }

            return base.ValidateEntity(entityEntry, items);
        }

        public DbSet<Url> direcciones { get; set; }
    }

}