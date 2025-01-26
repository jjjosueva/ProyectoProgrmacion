using ProyectoProgrmacion.Models;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ProyectoProgrmacion.Services
{
    public class PiezaService
    {
        private readonly SQLiteAsyncConnection _database;

        public PiezaService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "piezas.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Pieza>().Wait();
        }

        public Task<List<Pieza>> ObtenerPiezasAsync()
        {
            return _database.Table<Pieza>().ToListAsync();
        }

        public Task<int> AgregarPiezaAsync(Pieza pieza)
        {
            return _database.InsertAsync(pieza);
        }

        public Task<int> EditarPiezaAsync(Pieza pieza)
        {
            return _database.UpdateAsync(pieza);
        }

        public Task<int> EliminarPiezaAsync(Pieza pieza)
        {
            return _database.DeleteAsync(pieza);
        }
    }
}
