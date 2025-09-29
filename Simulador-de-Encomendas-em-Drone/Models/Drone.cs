namespace Simulador_de_Encomendas_em_Drone.Models
{
    public class Drone
    {
        public int Id { get; set; } 
        public string Nome { get; set; }
        public double DistanciaMax { get; set; }    
        public double CargaMax { get; set; }

        public List<Pedido> PedidosAtribuidos { get; set; }

        public bool PodeAtribuirPedidoPorPeso(double pesoPedido)
        {
            return pesoPedido + SomaPesoAtribuido() <= CargaMax;
        }
        public bool PodeAlcancarCliente(double clienteX, double clienteY)
        {
            double distancia = Math.Sqrt(Math.Pow(clienteX, 2) + Math.Pow(clienteY, 2));
            return distancia <= DistanciaMax;
        }
        public double SomaPesoAtribuido() 
        {
            if(PedidosAtribuidos == null)
            {
                return 0;
            }
            return PedidosAtribuidos.Sum(p => p.Peso);
        }
    }
}
