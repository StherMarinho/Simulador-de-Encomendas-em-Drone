using Dapper;
using MySql.Data.MySqlClient;
using Simulador_de_Encomendas_em_Drone.DTOs.Drone;
using Simulador_de_Encomendas_em_Drone.Models;

namespace Simulador_de_Encomendas_em_Drone.Repositories
{
    public class DroneRepository
    {
        private readonly IConfiguration _configuration;
        public DroneRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string connectionString => _configuration.GetConnectionString("DefaultConnection");
        public List<Drone> ObtemDrones()
        {
            using (var conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();
                var ret = conexao.Query<Drone>("SELECT * FROM Drones").AsList();
                conexao.Dispose();
                conexao.Close();
                return ret;
            }
        }
        public Drone ObtemDrone(int id)
        {
            using (var conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();
                var ret = conexao.Query<Drone>($"SELECT * FROM Drones WHERE Id = {id}").FirstOrDefault();
                conexao.Dispose();
                conexao.Close();
                return ret;
            }
        }
        public int CriaDrone(CriaDroneDTO drone)
        {
            using (var conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();
                int id = conexao.ExecuteScalar<int>("INSERT INTO Drones (Nome, DistanciaMax, CargaMax) VALUES (@Nome, @DistanciaMax, @CargaMax);SELECT LAST_INSERT_ID();", drone);
                conexao.Dispose();
                conexao.Close();
                return id;
            }
        }
    }
}
