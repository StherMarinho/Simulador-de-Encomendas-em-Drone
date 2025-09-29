using Simulador_de_Encomendas_em_Drone.DTOs.Pedido;
using Simulador_de_Encomendas_em_Drone.Models;
using Simulador_de_Encomendas_em_Drone.Repositories;

namespace Simulador_de_Encomendas_em_Drone.Services
{
    public class PedidoService
    {
        private readonly PedidoRepository _pedidoRepository;
        private readonly DroneRepository _droneRepository;

        public PedidoService(PedidoRepository pedidoRepository, DroneRepository droneRepository)
        {
            _pedidoRepository = pedidoRepository;
            _droneRepository = droneRepository;
        }
        public int CriarPedido(CriaPedidoDTO pedidoDto)
        {
            if (pedidoDto.Peso <= 0)
            {
                throw new Exception("O peso deve ser maior que zero.");
            }
            if (pedidoDto.Prioridade < 1 || pedidoDto.Prioridade > 3)
            {
                throw new Exception("A prioridade deve ser entre 1 e 3.");
            }
            return _pedidoRepository.CriaPedido(pedidoDto);
        }

        public List<Drone> DistribuiPedidoAosDrones()
        {
            var todosDrones = _droneRepository.ObtemDrones()
                                              .OrderByDescending(d => d.CargaMax)
                                              .ToList();

            foreach (var drone in todosDrones)
            {
                if (drone.PedidosAtribuidos == null)
                    drone.PedidosAtribuidos = new List<Pedido>();
            }

            var pedidosSemDrone = _pedidoRepository.ObtemPedidosOrdemPrioridade()
                                                   .OrderByDescending(p => p.Prioridade)
                                                   .ThenByDescending(p => p.Peso)
                                                   .ToList();

            int i = 0;
            while (i < pedidosSemDrone.Count)
            {
                var pedido = pedidosSemDrone[i];
                Drone melhorDrone = null;
                double melhorUso = double.MinValue;

                foreach (var drone in todosDrones)
                {
                    if (drone.PodeAtribuirPedidoPorPeso(pedido.Peso) &&
                        drone.PodeAlcancarCliente(pedido.ClienteX, pedido.ClienteY))
                    {
                        double usoAtual = drone.SomaPesoAtribuido() + pedido.Peso;
                        if (usoAtual > melhorUso)
                        {
                            melhorUso = usoAtual;
                            melhorDrone = drone;
                        }
                    }
                }

                if (melhorDrone != null)
                {
                    melhorDrone.PedidosAtribuidos.Add(pedido);
                    pedidosSemDrone.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            return todosDrones;
        }
    }
}