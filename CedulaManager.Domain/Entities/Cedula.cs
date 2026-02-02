using CedulaManager.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Domain.Entities
{
    public class Cedula: AuditableEntity
    {
        public Guid Id { get; set; }
        public string CedulaNumero { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string FechaNacimiento { get; set; } = string.Empty;
        public string EstadoCivil {  get; set; } = string.Empty;
        public string Conyuge {  get; set; } = string.Empty;
        public string Nacionalidad {  get; set; } = string.Empty;
        public string FechaCedulacion {  get; set; } = string.Empty;
        public string LugarDomicilio {  get; set; } = string.Empty;
        public string CalleDomicilio { get; set; } = string.Empty;
        public string NumeracionDomicilio { get; set; } = string.Empty;
        public string NombreMadre {  get; set; } = string.Empty;
        public string NombrePadre {  get; set; } = string.Empty;
        public string LugarNacimiento { get; set; } = string.Empty;
        public string Instruccion {  get; set; } = string.Empty;
        public string Profesion {  get; set; } = string.Empty;
        public DateTime Fecha_ultima_actualizacion {  get; set; }

        public virtual ICollection<HistorialConsultas> HistorialConsultas { get; set; } = new List<HistorialConsultas>();

    }
}
