using Simulador_de_Encomendas_em_Drone.DTOs.Drone;
using Simulador_de_Encomendas_em_Drone.Models;
using Simulador_de_Encomendas_em_Drone.Repositories;

namespace Simulador_de_Encomendas_em_Drone.Services
{
    public class DroneService
    {
        private readonly DroneRepository _droneRepository;

        public DroneService(DroneRepository droneRepository)
        {
            _droneRepository = droneRepository;
        }

        public int RegistrarDrone(CriaDroneDTO droneDto)
        {
            if(droneDto.Nome == null)
            {
                throw new ArgumentException("Nome do Drone não pode ser nulo!");
            }
            if (droneDto.DistanciaMax <= 0)
            {
                throw new Exception("Distâcia não pode ser menor ou igual a 0!");
            }
            if (droneDto.CargaMax <= 0)
            {
                throw new Exception("Capacidade não pode ser menor ou igual a 0!");
            }
            return _droneRepository.CriaDrone(droneDto);
        }
        public List<Drone> ObterDronesDisponiveis()
        {
            return _droneRepository.ObtemDrones();
        }
        public Drone ObterDrone(int id)
        {
            return _droneRepository.ObtemDrone(id) ?? throw new Exception("Drone não encontrado!");
        }
    }
}
