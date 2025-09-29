namespace Simulador_de_Encomendas_em_Drone.Models
{
    public class Pedido
    {
        public int Id { get; set; } 
        public double Peso { get; set; }
        public int ClienteX { get; set; }
        public int ClienteY { get; set; }
        public int Prioridade { get; set; }  
        public int DroneId { get; set; }
    }
}
