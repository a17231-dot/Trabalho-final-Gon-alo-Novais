using System;
using System.Collections.Generic;

namespace GestaoFrota
{
    internal class Program
    {
        static List<Veiculo> frota = new List<Veiculo>();

        static void Main(string[] args)
        {
            bool sair = false;

            while (!sair)
            {
                MostrarMenu();

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                Console.Clear();

                switch (opcao)
                {
                    case "1":
                        CriarVeiculo();
                        break;

                    case "2":
                        ListarVeiculos();
                        break;

                    case "3":
                        AbastecerVeiculo();
                        break;

                    case "4":
                        RealizarViagem();
                        break;

                    case "5":
                        ConsultarVeiculo();
                        break;

                    case "6":
                        AlterarQuilometragem();
                        break;

                    case "0":
                        sair = true;
                        Console.WriteLine("Programa terminado.");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        Pausar();
                        break;
                }
            }
        }

        static void MostrarMenu()
        {
            Console.Clear();

            Console.WriteLine("==========================================");
            Console.WriteLine("      SISTEMA DE GESTÃO DE FROTA");
            Console.WriteLine("==========================================");
            Console.WriteLine("1 - Criar veículo");
            Console.WriteLine("2 - Listar veículos");
            Console.WriteLine("3 - Abastecer veículo");
            Console.WriteLine("4 - Realizar viagem");
            Console.WriteLine("5 - Consultar veículo");
            Console.WriteLine("6 - Alterar quilometragem");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("==========================================");
        }

        static void CriarVeiculo()
        {
            Console.WriteLine("=== CRIAÇÃO DE VEÍCULO ===");
            Console.WriteLine();

            Console.Write("Introduza a matrícula: ");
            string matricula = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(matricula))
            {
                Console.WriteLine("ERRO: A matrícula não pode estar vazia.");
                Pausar();
                return;
            }

            Console.Write("Introduza a capacidade do depósito: ");

            if (!int.TryParse(Console.ReadLine(), out int capacidade))
            {
                Console.WriteLine("ERRO: Capacidade inválida.");
                Pausar();
                return;
            }

            if (capacidade <= 0)
            {
                Console.WriteLine(
                    "ERRO: A capacidade deve ser superior a zero."
                );

                Pausar();
                return;
            }

            Veiculo veiculo = new Veiculo(
                matricula,
                capacidade
            );

            // Inscrição no evento
            veiculo.ReservaAtingida += Veiculo_ReservaAtingida;

            frota.Add(veiculo);

            Console.WriteLine();
            Console.WriteLine("Veículo criado com sucesso.");
            Console.WriteLine($"Matrícula: {veiculo.Matricula}");
            Console.WriteLine(
                $"Depósito: {veiculo.CapacidadeDeposito} L"
            );

            Pausar();
        }

        static void ListarVeiculos()
        {
            Console.WriteLine("=== LISTA DE VEÍCULOS ===");
            Console.WriteLine();

            if (frota.Count == 0)
            {
                Console.WriteLine("Não existem veículos registados.");
                Pausar();
                return;
            }

            for (int i = 0; i < frota.Count; i++)
            {
                Console.WriteLine($"VEÍCULO {i + 1}");
                frota[i].MostrarInformacoes();
            }

            Pausar();
        }

        static void AbastecerVeiculo()
        {
            Console.WriteLine("=== ABASTECIMENTO ===");
            Console.WriteLine();

            Veiculo veiculo = SelecionarVeiculo();

            if (veiculo == null)
            {
                Pausar();
                return;
            }

            Console.WriteLine();
            Console.WriteLine(
                $"Combustível atual: {veiculo.NivelCombustivel:F2} L"
            );

            Console.Write("Litros a abastecer: ");

            if (!double.TryParse(
                Console.ReadLine(),
                out double litros))
            {
                Console.WriteLine("ERRO: Valor inválido.");
                Pausar();
                return;
            }

            veiculo.Abastecer(litros);

            Pausar();
        }

        static void RealizarViagem()
        {
            Console.WriteLine("=== REALIZAR VIAGEM ===");
            Console.WriteLine();

            Veiculo veiculo = SelecionarVeiculo();

            if (veiculo == null)
            {
                Pausar();
                return;
            }

            Console.WriteLine();
            Console.WriteLine(
                $"Combustível disponível: {veiculo.NivelCombustivel:F2} L"
            );

            Console.Write("Distância da viagem em km: ");

            if (!int.TryParse(
                Console.ReadLine(),
                out int distancia))
            {
                Console.WriteLine("ERRO: Distância inválida.");
                Pausar();
                return;
            }

            veiculo.RealizarViagem(distancia);

            Pausar();
        }

        static void ConsultarVeiculo()
        {
            Console.WriteLine("=== CONSULTA DE VEÍCULO ===");
            Console.WriteLine();

            Veiculo veiculo = SelecionarVeiculo();

            if (veiculo == null)
            {
                Pausar();
                return;
            }

            Console.WriteLine();
            veiculo.MostrarInformacoes();

            Pausar();
        }

        static void AlterarQuilometragem()
        {
            Console.WriteLine("=== ALTERAÇÃO DE QUILOMETRAGEM ===");
            Console.WriteLine();

            Veiculo veiculo = SelecionarVeiculo();

            if (veiculo == null)
            {
                Pausar();
                return;
            }

            Console.WriteLine(
                $"Quilometragem atual: {veiculo.Quilometragem} km"
            );

            Console.Write("Nova quilometragem: ");

            if (!int.TryParse(
                Console.ReadLine(),
                out int novaQuilometragem))
            {
                Console.WriteLine("ERRO: Valor inválido.");
                Pausar();
                return;
            }

            veiculo.Quilometragem = novaQuilometragem;

            Pausar();
        }

        static Veiculo SelecionarVeiculo()
        {
            if (frota.Count == 0)
            {
                Console.WriteLine("Não existem veículos registados.");
                return null;
            }

            Console.WriteLine("Veículos disponíveis:");
            Console.WriteLine();

            for (int i = 0; i < frota.Count; i++)
            {
                Console.WriteLine(
                    $"{i + 1} - {frota[i].Matricula}"
                );
            }

            Console.WriteLine();
            Console.Write("Escolha o número do veículo: ");

            if (!int.TryParse(
                Console.ReadLine(),
                out int escolha))
            {
                Console.WriteLine("ERRO: Escolha inválida.");
                return null;
            }

            if (escolha < 1 || escolha > frota.Count)
            {
                Console.WriteLine("ERRO: Esse veículo não existe.");
                return null;
            }

            return frota[escolha - 1];
        }

        // Método executado quando o evento é disparado
        static void Veiculo_ReservaAtingida(
            object sender,
            EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine();
            Console.WriteLine("****************************************");
            Console.WriteLine("*       ALERTA DE AUTONOMIA!           *");
            Console.WriteLine("* Combustível abaixo dos 10% do depósito*");
            Console.WriteLine("****************************************");

            Console.ResetColor();
        }

        static void Pausar()
        {
            Console.WriteLine();
            Console.WriteLine("Prima ENTER para continuar...");
            Console.ReadLine();
        }
    }
}