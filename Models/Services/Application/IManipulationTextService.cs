using API_Cifra_Decifra_Testo.Models.InputModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace API_Cifra_Decifra_Testo.Models.Services.Application
{
    public interface IManipulationTextService
    {
        string TestoCifratoGeneratorAsync(InputTesto model, [FromServices] IWebHostEnvironment env);
        string TestoCifratoRestoreAsync(InputTesto model, [FromServices] IWebHostEnvironment env);
    }
}