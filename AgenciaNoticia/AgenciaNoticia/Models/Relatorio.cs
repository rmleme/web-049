using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

namespace AgenciaNoticia.Models
{
    public partial class Relatorio
    {
        string relatorioId { get; set; }
        string dataRelatorio { get; set; }
        double totalDeVendas { get; set; }
        int totalDeNoticiasAcessadas { get; set; }
        int numeroDeNoticiasVendidas { get; set; }
    }
}