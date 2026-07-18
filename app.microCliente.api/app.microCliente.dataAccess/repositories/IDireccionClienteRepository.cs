using app.microCliente.entities.models;

namespace app.microCliente.dataAccess.repositories
{
    public interface IDireccionClienteRepository
    {
        Task<DireccionCliente> SeleccionarUno(int id);

        Task<DireccionCliente> Insertar(DireccionCliente cliente);

        Task<List<DireccionCliente>> SeleccionarTodos();

        Task Actualizar(DireccionCliente cliente);

        Task Eliminar(int id);
    }
}
