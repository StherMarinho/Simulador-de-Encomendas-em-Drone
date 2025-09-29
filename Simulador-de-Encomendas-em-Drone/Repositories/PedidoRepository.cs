using Dapper;
using MySql.Data.MySqlClient;
using Simulador_de_Encomendas_em_Drone.DTOs.Pedido;
using Simulador_de_Encomendas_em_Drone.Models;
using System.Data;

namespace Simulador_de_Encomendas_em_Drone.Repositories
{
    public class PedidoRepository
    {
        private readonly IConfiguration _configuration;
        public PedidoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string connectionString => _configuration.GetConnectionString("DefaultConnection");
        public List<Pedido> ObtemPedidos()
        {
            using (var conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();
                var ret = conexao.Query<Pedido>("SELECT * FROM Pedidos").AsList();
                conexao.Dispose();
                conexao.Close();
                return ret;
            }
        }
        public Pedido ObtemPedido(int id)
        {
            using (var conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();
                var ret = conexao.Query<Pedido>($"SELECT * FROM Pedidos WHERE Id = {id}").FirstOrDefault();
                conexao.Dispose();
                conexao.Close();
                return ret;
            }
        }
        public int CriaPedido(CriaPedidoDTO pedidoDto)
        {
            using (var conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();
                int id = conexao.ExecuteScalar<int>("INSERT INTO Pedidos (Peso, ClienteX, ClienteY, Prioridade) VALUES (@Peso, @ClienteX, @ClienteY, @Prioridade);SELECT LAST_INSERT_ID();", pedidoDto);
                conexao.Dispose();
                conexao.Close();
                return id;
            }
        }
        public void AtribuirDrone(int pedidoId, int droneId)
        {
            using (var conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();
                conexao.Execute("UPDATE Pedidos SET DroneId = @DroneId WHERE Id = @PedidoId", new { DroneId = droneId, PedidoId = pedidoId });
                conexao.Dispose();
                conexao.Close();
            }
        }
        public List<Pedido> ObtemPedidosOrdemPrioridade()
        {
            using (var conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();
                var ret = conexao.Query <Pedido>("SELECT * FROM Pedidos WHERE DroneId IS NULL").AsList();
                conexao.Dispose();
                conexao.Close();
                return ret;
            }
        }
    }
}
