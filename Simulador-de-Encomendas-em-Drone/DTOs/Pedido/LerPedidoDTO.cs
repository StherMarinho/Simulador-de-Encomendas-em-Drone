namespace Simulador_de_Encomendas_em_Drone.DTOs.Pedido
{
    public class LerPedidoDTO
    {
        public int Id { get; set; }
        public double Peso { get; set; }
        public int ClienteX { get; set; }
        public int ClienteY { get; set; }
        public string Prioridade { get; set; }
    }
}
