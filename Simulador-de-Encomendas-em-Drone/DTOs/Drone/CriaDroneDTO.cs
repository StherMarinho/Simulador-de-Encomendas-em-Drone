namespace Simulador_de_Encomendas_em_Drone.DTOs.Drone
{
    public class CriaDroneDTO
    {
        public required string Nome { get; set; }
        public required double DistanciaMax { get; set; }
        public required double CargaMax { get; set; }
    }
}
