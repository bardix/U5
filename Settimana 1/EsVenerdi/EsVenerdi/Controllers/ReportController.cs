using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using EsVenerdi.Models;

namespace EsVenerdi.Controllers
{
    public class ReportController : Controller
    {
        private readonly IConfiguration _configuration;

        public ReportController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public IActionResult VerbaliPerTrasgressore()
        {
            var list = new List<AnagraficaReport>();
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT a.Cognome, a.Nome, COUNT(v.IDVerbale) AS TotaleVerbali FROM ANAGRAFICA a JOIN VERBALE v ON a.IDAnagrafica = v.IDAnagrafica GROUP BY a.Cognome, a.Nome", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AnagraficaReport
                        {
                            Cognome = reader.GetString(0),
                            Nome = reader.GetString(1),
                            TotaleVerbali = reader.GetInt32(2)
                        });
                    }
                }
            }
            return View(list);
        }

        public IActionResult PuntiDecurtatiPerTrasgressore()
        {
            var list = new List<AnagraficaReport>();
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT a.Cognome, a.Nome, SUM(v.DecurtamentoPunti) AS TotalePunti FROM ANAGRAFICA a JOIN VERBALE v ON a.IDAnagrafica = v.IDAnagrafica GROUP BY a.Cognome, a.Nome", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AnagraficaReport
                        {
                            Cognome = reader.GetString(0),
                            Nome = reader.GetString(1),
                            TotalePunti = reader.GetInt32(2)
                        });
                    }
                }
            }
            return View(list);
        }

        public IActionResult ViolazioniOltre10Punti()
        {
            var list = new List<VerbaleReport>();
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT a.Cognome, a.Nome, v.DataViolazione, v.Importo, v.DecurtamentoPunti FROM ANAGRAFICA a JOIN VERBALE v ON a.IDAnagrafica = v.IDAnagrafica WHERE v.DecurtamentoPunti > 10", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new VerbaleReport
                        {
                            Cognome = reader.GetString(0),
                            Nome = reader.GetString(1),
                            DataViolazione = reader.GetDateTime(2),
                            Importo = reader.GetDecimal(3),
                            DecurtamentoPunti = reader.GetInt32(4)
                        });
                    }
                }
            }
            return View(list);
        }

        public IActionResult ViolazioniOltre400Euro()
        {
            var list = new List<VerbaleReport>();
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT a.Cognome, a.Nome, v.DataViolazione, v.Importo, v.DecurtamentoPunti FROM ANAGRAFICA a JOIN VERBALE v ON a.IDAnagrafica = v.IDAnagrafica WHERE v.Importo > 400", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new VerbaleReport
                        {
                            Cognome = reader.GetString(0),
                            Nome = reader.GetString(1),
                            DataViolazione = reader.GetDateTime(2),
                            Importo = reader.GetDecimal(3),
                            DecurtamentoPunti = reader.GetInt32(4)
                        });
                    }
                }
            }
            return View(list);
        }
    }
}