
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using netcoreMVC.Infraestructure;
using netcoreMVC.Models;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.Reflection;
using System.Security.Claims;

namespace netcoreMVC.Controllers
{

    public class HomeController : Controller
    {      

        private readonly HanaOdbcConnectionFactory _connectionFactory;        

        public HomeController(HanaOdbcConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        
        public ActionResult Index()
        {          

            //HttpContext.Session.SetString("Usuario", "Joel");

            return View("Test");

        }

        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{

        //    using var connection = _connectionFactory.CreateConnection();
        //    await connection.OpenAsync();

        //    using var command = new OdbcCommand("SELECT * FROM SMX_SRGC", connection);
        //    var result = await command.ExecuteScalarAsync();

        //    return Content($"Conexion OK. Usuario actual: {result}");

        //}

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> signin()
        {

            ////1: Obtiene la conexion
            //using var connection = _connectionFactory.CreateConnection();

            ////2: Abre la conexion
            //await connection.OpenAsync();

            ////3: CADENA DE CONEXIÓN
            //using var command = new OdbcCommand("SELECT * FROM SMX_SRGC", connection);

            ////4: EJECUTAR EL COMMANDO
            //var result = await command.ExecuteScalarAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "admin"),                
                new Claim(ClaimTypes.Role, "admin"), 
                new Claim("IdUsuario", "1") 
            };

            // 3. Crear la identidad y el principal
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // 4. Firmar (esto genera la cookie cifrada)
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = false,  // false = equivalente al segundo param de SetAuthCookie
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                }
            );
            
            //var connection = _connectionFactory.CreateConnection();
            //await connection.OpenAsync();

            //using var command = new OdbcCommand("SELECT * FROM SMX_SRGC", connection);
            //using var reader = await command.ExecuteReaderAsync();

            //var dataTable = new DataTable();
            //dataTable.Load(reader);

            //string fr = dataTable.Rows.Count.ToString();

            //using var command0 = new OdbcCommand("SELECT * FROM SMX_SRGC", connection);            
            //var result = await command0.ExecuteScalarAsync();

            return RedirectToAction("Dashboard");

        }

        public async Task<IActionResult> Dashboard()
        {                
         
            return View("Dashboard");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}
