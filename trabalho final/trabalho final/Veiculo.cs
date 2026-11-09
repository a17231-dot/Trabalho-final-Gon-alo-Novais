using System;

namespace GestaoFrota
{
    public class Veiculo
    {
        // Campos privados
        private int _quilometragem;
        private double _nivelCombustivel;

        // Propriedades automáticas
        public string Matricula { get; set; }
        public int CapacidadeDeposito { get; set; }

        // Propriedade completa da quilometragem
        public int Quilometragem
        {
            get
            {
                return _quilometragem;
            }
            set
            {
                if (value >= _quilometragem)
                {
                    _quilometragem = value;
                }
                else
                {
                    Console.WriteLine(
                        "ERRO: A quilometragem não pode diminuir."
                    );
                }
            }
        }

        // Propriedade completa do combustível
        public double NivelCombustivel
        {
            get
            {
                return _nivelCombustivel;
            }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine(
                        "ERRO: O nível de combustível não pode ser inferior a 0 litros."
                    );

                    return;
                }

                if (value > CapacidadeDeposito)
                {
                    Console.WriteLine(
                        "ERRO: O nível de combustível não pode ultrapassar a capacidade do depósito."
                    );

                    return;
                }

                _nivelCombustivel = value;

                // Verificação da reserva
                double limiteReserva = CapacidadeDeposito * 0.10;

                if (_nivelCombustivel < limiteReserva)
                {
                    ReservaAtingida?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        // Evento
        public event EventHandler ReservaAtingida;

        // Construtor
        public Veiculo(string matricula, int capacidadeDeposito)
        {
            Matricula = matricula;
            CapacidadeDeposito = capacidadeDeposito;

            _quilometragem = 0;
            _nivelCombustivel = capacidadeDeposito;
        }

        // Método para abastecer
        public void Abastecer(double litros)
        {
            if (litros <= 0)
            {
                Console.WriteLine(
                    "ERRO: A quantidade de combustível deve ser superior a zero."
                );

                return;
            }

            double novoNivel = NivelCombustivel + litros;

            if (novoNivel > CapacidadeDeposito)
            {
                NivelCombustivel = CapacidadeDeposito;

                Console.WriteLine(
                    "AVISO: A quantidade ultrapassava a capacidade."
                );

                Console.WriteLine(
                    $"Depósito atestado até aos {CapacidadeDeposito} litros."
                );
            }
            else
            {
                NivelCombustivel = novoNivel;

                Console.WriteLine(
                    $"Abastecimento efetuado. Combustível atual: {NivelCombustivel:F2} L"
                );
            }
        }

        // Método para realizar uma viagem
        public bool RealizarViagem(int kmDistancia)
        {
            if (kmDistancia <= 0)
            {
                Console.WriteLine(
                    "ERRO: A distância deve ser superior a zero."
                );

                return false;
            }

            // Consumo: 0,07 litros por quilómetro
            double consumo = kmDistancia * 0.07;

            if (NivelCombustivel < consumo)
            {
                Console.WriteLine(
                    "VIAGEM RECUSADA: combustível insuficiente."
                );

                return false;
            }

            Quilometragem += kmDistancia;
            NivelCombustivel -= consumo;

            Console.WriteLine("Viagem realizada com sucesso.");
            Console.WriteLine($"Distância: {kmDistancia} km");
            Console.WriteLine($"Combustível gasto: {consumo:F2} L");
            Console.WriteLine($"Combustível restante: {NivelCombustivel:F2} L");
            Console.WriteLine($"Quilometragem: {Quilometragem} km");

            return true;
        }

        // Método para apresentar informações
        public void MostrarInformacoes()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Matrícula: {Matricula}");
            Console.WriteLine($"Capacidade do depósito: {CapacidadeDeposito} L");
            Console.WriteLine($"Quilometragem: {Quilometragem} km");
            Console.WriteLine($"Combustível: {NivelCombustivel:F2} L");
            Console.WriteLine("------------------------------------------");
        }
    }
}