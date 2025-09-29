namespace Simulador_de_Encomendas_em_Drone.DTOs.Pedido
{
    public class CriaPedidoDTO
    {
        public required double Peso { get; set; }
        public required int ClienteX { get; set; }
        public required int ClienteY { get; set; }
        public required int Prioridade { get; set; }
    }
}
