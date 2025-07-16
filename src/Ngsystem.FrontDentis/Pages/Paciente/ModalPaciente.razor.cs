using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Ngsystem.FrontDentis.Components;
using Ngsystem.Infrastructure.Dtos;
using Ngsystem.Infrastructure.Infrastructure.Http;
using System.Linq;
namespace Ngsystem.FrontDentis.Pages.Paciente;

public class ModalPacienteBase: ComponentBase
{
    [Parameter] public LisPacienteResponseDto objPaciente { get; set; } = new LisPacienteResponseDto();
    [Inject] IPaciente _pacienteServicio { get; set; }
    [Inject] ISnackbar _snackBar { get; set; }
    [Inject] IDialogService _dialogServicio { get; set; }
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    public DateTime? fechaInicio = DateTime.Now;
    public string noneFecha = "d-block";
    public List<TipoSexo> listaGenero =new List<TipoSexo>();
    public int tipoSeleccionado=1;
    public bool requiereNombre=false;



    protected override async Task OnInitializedAsync()
    {

        

        Console.WriteLine("Entro dialog");
        Console.WriteLine(objPaciente.Id.ToString());

        listaGenero.Add(new TipoSexo { Id = 1, Name = "Masculino" });
        listaGenero.Add(new TipoSexo { Id = 2, Name = "Femenino" });
        
        if (objPaciente.Id != null) 
        {
            var genero = listaGenero.Where(c => c.Name == objPaciente.Genero).FirstOrDefault();
            tipoSeleccionado = genero.Id;
        }
        
    }
    public void Cancel()
    {
        MudDialog.Cancel();
    }
   
    public IEnumerable<string> ValidateEmail(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            yield return "El campo no puede estar vacío.";
    }
    public bool campoObligatorio=false;

    public async Task MostrarConfirmacion()
    {
        objPaciente.FechaNacimiento = fechaInicio?.ToString("dd/MM/yyyy");
        var reg = listaGenero.Where(c => c.Id == tipoSeleccionado).FirstOrDefault();
        if (reg == null) { return; }
        if (string.IsNullOrWhiteSpace(objPaciente.Nombre)) { return; }
        if (string.IsNullOrWhiteSpace(objPaciente.Apellido)) { return; }
        if (string.IsNullOrWhiteSpace(objPaciente.Dni)) { return; }
        if (string.IsNullOrWhiteSpace(objPaciente.Telefono)) { return; }
        if (string.IsNullOrWhiteSpace(objPaciente.Direccion)) { return; }
        objPaciente.Genero = reg.Name;
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small };
            var dialog = _dialogServicio.Show<ModalDialog>("Grabar Registro", new DialogParameters
            {
            { "Message", "¿Estás seguro de que quieres grabar este registro?" }
             }, options);

            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await Guardar();
            }
        
    }
 

    public async Task Guardar()
    {
        string _mensaje = "";
        bool _resultado=false;
        if (objPaciente.Id == null)
        {            
            var response = await _pacienteServicio.GrabarPaciente(objPaciente);
            
            if (response.Status == true)
            {
                _resultado = response.Status;
                _mensaje = response.Registro.Dni;
            }
        }
        else
        {
            var response = await _pacienteServicio.UpdatePaciente(objPaciente);

            if (response.Status == true)
            {
                _resultado = response.Status;
                _mensaje = response.Registro.Dni;
            }

        }
        if (_resultado)
        {
            _snackBar.Add(_mensaje, Severity.Success, a => a.VisibleStateDuration = 500);
            MudDialog.Close(DialogResult.Ok(true));
        }
        else
            _snackBar.Add("Error al guardar cambios", Severity.Error, a => a.VisibleStateDuration = 500);

    }
}

public class ConfirmDialog
{
}

public class TipoSexo()
{
    public int Id { get; set; }
    public string Name { get; set; }
}