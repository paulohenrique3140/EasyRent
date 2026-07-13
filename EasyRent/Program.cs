// INSTANCIANDO OBJETOS DAS CLASSES
using System.Net.Security;
using System.Reflection.Metadata;

Cliente? cliente = null;
Veiculo? veiculo = null;
Locacao? locacao = null;

while (true)
{
    // EXIBINDO MENU INICIAL 
    Console.WriteLine("\n### EASY RENT - LOCAÇÃO DE VEÍCULOS ###");
    Console.WriteLine("\nMenu: \n" +
                      "\n[1] Cadastro de cliente" +
                      "\n[2] Cadastro de veículo" +
                      "\n[0] Sair\n");
    Console.Write("\nDigite a opção desejada: ");
    var entrada = Console.ReadLine();

    // VALIDANDO A ENTRADA DIGITADA PARA O MENU
    bool conversaoValida = int.TryParse(entrada, out int opcaoMenuInicial);
    bool? validaEntrada = ValidaEntrada(conversaoValida, opcaoMenuInicial);
    if (validaEntrada == false) { break; }
    else if (validaEntrada == true) { continue; }

    // SWITCH MENU INICIAL
    switch (opcaoMenuInicial)
    {
        case 1: // CRIANDO O OBJETO DA CLASSE CLIENTE
            Console.WriteLine("\n@@@ Cadastro de Cliente @@@");
            Console.Write("\nNome completo: ");
            string? nome = Console.ReadLine();
            Console.Write("CPF: ");
            string? cpf = Console.ReadLine();
            Console.Write("Registro CNH: ");
            string? cnh = Console.ReadLine();
            DateTime dataNascimento;
            while (true)
            {
                Console.Write("Data de nascimento [DD/MM/AAAA]: ");
                string? entradaData = Console.ReadLine();
                if (DateTime.TryParse(entradaData, out dataNascimento))
                    break;
                Console.WriteLine("Data inválida. Tente novamente.\n");
            }
            cliente = new Cliente(nome, cpf, cnh, Cliente.CalculaIdade(dataNascimento));
            break;
        
        case 2: // CRIANDO O OBJETO DA CLASSE VEICULO
            Console.WriteLine("\n@@@ Cadastro de veiculo @@@");
            Console.Write("\nModelo: ");
            string? modelo = Console.ReadLine();
            Console.Write("Placa: ");
            string? placa = Console.ReadLine();
            Console.Write("Carroceria [1-Hatch / 2-Sedan / 3-SUV / 4-Utilitario]: ");
            int carroceria = Convert.ToInt32(Console.ReadLine());
            Console.Write("Valor da diária: R$ ");
            double valorDiaria = Convert.ToDouble(Console.ReadLine());
            Console.Write("Quilometragem atual: ");
            int kmAtual = Convert.ToInt32(Console.ReadLine());
            veiculo = new Veiculo(modelo, placa, (Carroceria)carroceria, valorDiaria, kmAtual);
            break;
        default:
            Console.Write("\nOpção inválida! Digite uma das opções listadas: \n");
            break;
    }

    // VALIDANDO SE CLIENTE E VEICULO FORAM PREENCHIDOS
    if (cliente != null && veiculo != null)
    {
        while (true)
        {
            // EXIBINDO SEGUNDO MENU 
            Console.WriteLine("\n### EASY RENT - LOCAÇÃO DE VEÍCULOS ###");
            Console.WriteLine("\nMenu: \n" +
                              "\n[1] Exibir cliente" +
                              "\n[2] Exibir veículo" +
                              "\n[3] Efetuar locação do veículo" +
                              "\n[0] Voltar ao menu anterior\n");
            Console.Write("\nDigite a opção desejada: ");
            entrada = Console.ReadLine();

            // VALIDANDO A ENTRADA DIGITADA PARA O SEGUNDO MENU
            conversaoValida = int.TryParse(entrada, out int opcaoSegundoMenu);
            validaEntrada = ValidaEntrada(conversaoValida, opcaoSegundoMenu);
            if (validaEntrada == false) { break; }
            else if (validaEntrada == true) { continue; }

            // SWITCH SEGUNDO MENU
            switch (opcaoSegundoMenu)
            {
                case 1: // EXIBINDO OBJETO DA CLASSE CLIENTE
                    Console.WriteLine("\n@@@ EXIBINDO CLIENTE @@@");
                    Console.WriteLine(cliente.ExibirCliente());
                    break;

                case 2: // EXIBINDO OBJETO DA CLASSE VEICULO
                    Console.WriteLine("\n@@@ EXIBINDO VEÍCULO @@@");
                    Console.WriteLine(veiculo.ExibirVeiculo());
                    break;
                case 3: // CRIANDO UM OBJETO DA CLASSE LOCAÇÃO
                    Console.WriteLine("\n@@@ Reservando veiculo @@@");
                    Console.WriteLine("\n" + veiculo.ExibirVeiculo() + "\n");
                    Console.Write("\nDigite a quantidade de diárias: ");
                    int quantidadeDiarias = Convert.ToInt32(Console.ReadLine());
                    Console.Write("\nDeseja adicionar seguro de R$50,00 por diaria? [s/n]: ");
                    var seguro = Console.ReadLine();
                    bool possuiSeguro = seguro == "s";
                    locacao = new Locacao(cliente, veiculo, quantidadeDiarias, possuiSeguro, veiculo.KmAtual, StatusLocacao.Aberta);
                    break;
                default:
                    Console.Write("\nOpção inválida! Digite uma das opções listadas: \n");
                    break;
            }

            if(locacao != null)
            {
                while (true)
                {
                    // EXIBINDO TERCEIRO MENU 
                    Console.WriteLine("\n### EASY RENT - LOCAÇÃO DE VEÍCULOS ###");
                    Console.WriteLine("\nMenu: \n" +
                                      "\n[1] Finalizar locação (devolver veículo)" +
                                      "\n[2] Cancelar reserva" +
                                      "\n[3] Exibir locação" +
                                      "\n[0] Voltar ao menu anterior\n");
                    Console.Write("\nDigite a opção desejada: ");
                    entrada = Console.ReadLine();

                    // VALIDANDO A ENTRADA DIGITADA PARA O TERCEIRO MENU
                    conversaoValida = int.TryParse(entrada, out int opcaoTerceiroMenu);
                    validaEntrada = ValidaEntrada(conversaoValida, opcaoTerceiroMenu);
                    if (validaEntrada == false) { break; }
                    else if (validaEntrada == true) { continue; }

                    // SWITCH TERCEIRO MENU
                    switch (opcaoTerceiroMenu)
                    {
                        case 1: // CHAMANDO O MÉTODO PARA FINALIZAR LOCAÇÃO (DEVOLVER VEÍCULO)
                            Console.WriteLine("@@@ Painel de Devolução @@@");
                            Console.Write("Informe a quilometragem atual do veículo: ");
                            int kmFinal = Convert.ToInt32(Console.ReadLine());
                            locacao.FinalizarLocacao(kmFinal);
                            Console.WriteLine("\nLocação encerrada!" + locacao.ExibirResumo(veiculo.KmAtual));
                            break;
                        case 2: // CHAMANDO O MÉTODO PARA CANCELAR RESERVA
                            locacao.CancelarLocacao();
                            Console.WriteLine("\n" + locacao.ExibirResumo(veiculo.KmAtual));
                            break;
                        case 3: // EXIBINDO DETALHES DA LOCAÇÃO
                            Console.WriteLine("@@@ Exibindo locação @@@");
                            Console.WriteLine("\n" + locacao.ExibirResumo(veiculo.KmAtual));
                            break;
                        default:
                            Console.Write("\nOpção inválida! Digite uma das opções listadas: \n");
                            break;
                    }
                    if (locacao.Status != StatusLocacao.Aberta)
                    {
                        break;
                    }
                }
            }
        }
    }
}

static bool? ValidaEntrada(bool conversaoValida, int opcaoMenu)
{
    if (!conversaoValida)
    {
        Console.WriteLine("\nOpção inválida! Digite apenas números.\n");
        return true;
    }

    if (opcaoMenu < 0 || opcaoMenu > 8)
    {
        Console.Write("\nOpção inválida! Digite uma das opções listadas: \n");
        return true;
    }

    if (opcaoMenu == 0)
    {
        Console.WriteLine("\nENCERRANDO ...");
        return false;
    }
    return null;
}
