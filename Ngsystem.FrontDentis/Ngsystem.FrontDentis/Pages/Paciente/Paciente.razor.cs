using Microsoft.AspNetCore.Components;
using Ngsystem.Infrastructure.Infrastructure.Http;
using Ngsystem.Infrastructure.Dtos;
using MudBlazor;

namespace Ngsystem.FrontDentis.Pages.Paciente
{
    public class WeatherBase : ComponentBase
    {
        [Inject] IPaciente? _listaPaciente { get; set; }
        [Inject] IDialogService? _dialogServicio { get; set; }
        [Inject] ISnackbar? _snackBar { get; set; }
        public string searchString1 = "";
        public LisPacienteResponseDto selectedItem1 = null;
        public bool _loading = false;
        public bool estadoLoad = false;
        
        public IEnumerable<LisPacienteResponseDto>? listaPacienteDto { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await CargaLista();
        }
        public async Task CargaLista()
        {
            this.estadoLoad = true;
            var response = await _listaPaciente.ListaPaciente();
            if (response.Status == true)
            {
                this.listaPacienteDto = response.Lista.ToList();
            }
            this.estadoLoad = false;
        }
        public async Task AbrirEditarPacientes(LisPacienteResponseDto model)
        {
            var parametros = new DialogParameters { ["objPaciente"] = model };
            var options = new DialogOptions
            {
                MaxWidth = MaxWidth.Medium, // Tamaño máximo del modal
                FullWidth = true // Hace que ocupe todo el ancho disponible
            };
            var dialogo = _dialogServicio.Show<ModalPaciente>("Editar Paciente", parametros, options);
            var resultado = await dialogo.Result;

            if (!resultado.Canceled)
            {
                await CargaLista();
            }
        }
        public async Task MostrarDetalle()
        {
        }
        public async Task NuevoPaciente()
        {
            var options = new DialogOptions
            {
                MaxWidth = MaxWidth.Medium, // Tamaño máximo del modal
                FullWidth = true // Hace que ocupe todo el ancho disponible
            };
            //options.FullScreen = true;
            var dialogo = _dialogServicio.Show<ModalPaciente>("Nuevo Paciente", options);

            var resultado = await dialogo.Result;

            if (!resultado.Canceled)
            {
                await CargaLista();
            }
        }
        public bool FilterFunc1(LisPacienteResponseDto element) => FilterFunc(element, searchString1);

        public bool FilterFunc(LisPacienteResponseDto element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Nombre.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Apellido.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if ($"{element.Nombre} {element.Telefono}".Contains(searchString))
                return true;

            return false;
        }
    }
}
