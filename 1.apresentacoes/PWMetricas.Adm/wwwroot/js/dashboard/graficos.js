
const graficos = {};

// Função para expandir/recolher
function toggleAccordion(header) {
    const body = header.nextElementSibling;
    body.classList.toggle("open");

    // Muda o ícone do título
    header.textContent = (body.classList.contains("open") ? '▲' : '▼') + header.textContent.slice(1);

    // Atualiza os gráficos após abrir (requer Chart.js forçar resize)
    if (body.classList.contains("open")) {
        setTimeout(() => {
            Chart.helpers.each(Chart.instances, function (instance) {
                instance.resize();
            });
        }, 300);
    }
}

$(document).ready(function () {

    const hoje = new Date();

    $('#ano').datepicker({
        format: "yyyy",
        viewMode: "years",
        minViewMode: "years",
        autoclose: true
    });

    $("#mes").val(hoje.getMonth() + 1);

    $('#ano').val(hoje.getFullYear());

   


    // ✅ Função genérica para carregar qualquer gráfico de pizza
    async function carregarGraficoPizza({ id, endpoint, parametros = {} }) {
        try {
            const queryString = new URLSearchParams(parametros).toString();
            const url = `${endpoint}?${queryString}`;

            const response = await fetch(url);
            const result = await response.json();

            const canvas = document.getElementById(id);

            // Se já existe gráfico neste canvas, destrua antes de criar outro
            if (graficos[id]) {
                graficos[id].destroy();
                graficos[id] = null;
            }

            if (!result || !result.data || result.data.length === 0) {
                // Limpa o canvas manualmente se quiser
                const ctx = canvas.getContext("2d");
                ctx.clearRect(0, 0, canvas.width, canvas.height);
                //showAlert('Não existe resultado para o gráfico: ' + id, "warning");
                return;
            }

            const dados = result.data.map(x => x.porcentagem);
            const cores = result.data.map(x => x.corHex);
            const labels = result.data.map(x => x.nome);

            criarGrafico(id, dados, cores, labels);
        } catch (error) {
            showAlert('Erro ao carregar gráfico: ' + id, "danger");
        }
    }



    async function carregarGraficoPizzaTradicional({ id, endpoint, parametros = {} }) {
        try {
            const queryString = new URLSearchParams(parametros).toString();
            const url = `${endpoint}?${queryString}`;

            const response = await fetch(url);
            const result = await response.json();

            const canvas = document.getElementById(id);

            // Se já existe gráfico neste canvas, destrua antes de criar outro
            if (graficos[id]) {
                graficos[id].destroy();
                graficos[id] = null;
            }

            if (!result || !result.data || result.data.length === 0) {
                // Limpa o canvas manualmente se quiser
                const ctx = canvas.getContext("2d");
                ctx.clearRect(0, 0, canvas.width, canvas.height);
                //showAlert('Não existe resultado para o gráfico: ' + id, "warning");
                return;
            }

            const dados = result.data.map(x => x.porcentagem);
            const cores = ['#FF4D79', '#D6B287', '#434343', '#EFEFEF'];
            const labels = result.data.map(x => x.nome);

            criarGrafico(id, dados, cores, labels);
        } catch (error) {
            showAlert('Erro ao carregar gráfico: ' + id, "danger");
        }
    }

    function criarGrafico(id, dados, cores, labels) {
        const ctx = document.getElementById(id).getContext('2d');

        // Destroi gráfico antigo, se existir
        if (graficos[id]) {
            graficos[id].destroy();
        }

        // Cria novo gráfico e armazena a instância
        graficos[id] = new Chart(ctx, {
            type: 'pie',
            data: {
                labels: labels,
                datasets: [{
                    data: dados,
                    backgroundColor: cores
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        position: 'bottom',
                        labels: {
                            color: '#ffffff'
                        }
                    }
                }
            }
        });
    }


    // Chamadas para carregar os gráficos com parâmetros
    carregarGraficoPizza({
        id: 'atendimentoOrigem',
        endpoint: '/Dashboard/ListarOrigensPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 1 }
    });

    carregarGraficoPizza({
        id: 'orcamentoOrigem',
        endpoint: '/Dashboard/ListarOrigensPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 2 }
    });


    carregarGraficoPizza({
        id: 'vendidoOrigem',
        endpoint: '/Dashboard/ListarOrigensPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 3 }
    });

    carregarGraficoPizza({
        id: 'negociadoOrigem',
        endpoint: '/Dashboard/ListarOrigensPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 4 }
    });

    carregarGraficoPizza({
        id: 'naorespondeOrigem',
        endpoint: '/Dashboard/ListarOrigensPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 5 }
    });


    //Canal

    carregarGraficoPizza({
        id: 'atendimentoCanal',
        endpoint: '/Dashboard/ListarCanaisPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 1 }
    });


    carregarGraficoPizza({
        id: 'orcamentoCanal',
        endpoint: '/Dashboard/ListarCanaisPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 2 }
    });

    carregarGraficoPizza({
        id: 'vendidoCanal',
        endpoint: '/Dashboard/ListarCanaisPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 3 }
    });

    carregarGraficoPizza({
        id: 'negociadoCanal',
        endpoint: '/Dashboard/ListarCanaisPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 4 }
    });

    carregarGraficoPizza({
        id: 'naorespondeCanal',
        endpoint: '/Dashboard/ListarCanaisPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 5 }
    });


    //Vendedor

    carregarGraficoPizzaTradicional({
        id: 'atendimentoVendedor',
        endpoint: '/Dashboard/ListarVendedoresPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 1 }
    });

    carregarGraficoPizzaTradicional({
        id: 'orcamentoVendedor',
        endpoint: '/Dashboard/ListarVendedoresPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 2 }
    });

    carregarGraficoPizzaTradicional({
        id: 'vendidoVendedor',
        endpoint: '/Dashboard/ListarVendedoresPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 3 }
    });

    carregarGraficoPizzaTradicional({
        id: 'negociadoVendedor',
        endpoint: '/Dashboard/ListarVendedoresPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 4 }
    });

    carregarGraficoPizzaTradicional({
        id: 'naorespondeVendedor',
        endpoint: '/Dashboard/ListarVendedoresPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 5 }
    });


    //Cidade

    carregarGraficoPizzaTradicional({
        id: 'atendimentoCidade',
        endpoint: '/Dashboard/ListarCidadesPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 1 }
    });

    carregarGraficoPizzaTradicional({
        id: 'orcamentoCidade',
        endpoint: '/Dashboard/ListarCidadesPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 2 }
    });

    carregarGraficoPizzaTradicional({
        id: 'vendidoCidade',
        endpoint: '/Dashboard/ListarCidadesPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 3 }
    });

    carregarGraficoPizzaTradicional({
        id: 'negociadoCidade',
        endpoint: '/Dashboard/ListarCidadesPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 4 }
    });

    carregarGraficoPizzaTradicional({
        id: 'naorespondeCidade',
        endpoint: '/Dashboard/ListarCidadesPorStatus',
        parametros: { mes: $("#mes").val(), ano: $("#ano").val(), loja: $("#LojaId").val(), status: 5 }
    });


    $('#btnBuscar').on('click', function () {
        const mesSelecionado = $("#mes").val();
        const anoSelecionado = $("#ano").val();
        const lojaSelecionada = $("#LojaId").val();

        // Atualiza os gráficos com os novos filtros
        const parametros = (status) => ({ mes: mesSelecionado, ano: anoSelecionado, loja: lojaSelecionada, status });

        // Origem
        carregarGraficoPizza({ id: 'atendimentoOrigem', endpoint: '/Dashboard/ListarOrigensPorStatus', parametros: parametros(1) });
        carregarGraficoPizza({ id: 'orcamentoOrigem', endpoint: '/Dashboard/ListarOrigensPorStatus', parametros: parametros(2) });
        carregarGraficoPizza({ id: 'vendidoOrigem', endpoint: '/Dashboard/ListarOrigensPorStatus', parametros: parametros(3) });
        carregarGraficoPizza({ id: 'negociadoOrigem', endpoint: '/Dashboard/ListarOrigensPorStatus', parametros: parametros(4) });
        carregarGraficoPizza({ id: 'naorespondeOrigem', endpoint: '/Dashboard/ListarOrigensPorStatus', parametros: parametros(5) });

        // Canal
        carregarGraficoPizza({ id: 'atendimentoCanal', endpoint: '/Dashboard/ListarCanaisPorStatus', parametros: parametros(1) });
        carregarGraficoPizza({ id: 'orcamentoCanal', endpoint: '/Dashboard/ListarCanaisPorStatus', parametros: parametros(2) });
        carregarGraficoPizza({ id: 'vendidoCanal', endpoint: '/Dashboard/ListarCanaisPorStatus', parametros: parametros(3) });
        carregarGraficoPizza({ id: 'negociadoCanal', endpoint: '/Dashboard/ListarCanaisPorStatus', parametros: parametros(4) });
        carregarGraficoPizza({ id: 'naorespondeCanal', endpoint: '/Dashboard/ListarCanaisPorStatus', parametros: parametros(5) });

        // Vendedor
        carregarGraficoPizzaTradicional({ id: 'atendimentoVendedor', endpoint: '/Dashboard/ListarVendedoresPorStatus', parametros: parametros(1) });
        carregarGraficoPizzaTradicional({ id: 'orcamentoVendedor', endpoint: '/Dashboard/ListarVendedoresPorStatus', parametros: parametros(2) });
        carregarGraficoPizzaTradicional({ id: 'vendidoVendedor', endpoint: '/Dashboard/ListarVendedoresPorStatus', parametros: parametros(3) });
        carregarGraficoPizzaTradicional({ id: 'negociadoVendedor', endpoint: '/Dashboard/ListarVendedoresPorStatus', parametros: parametros(4) });
        carregarGraficoPizzaTradicional({ id: 'naorespondeVendedor', endpoint: '/Dashboard/ListarVendedoresPorStatus', parametros: parametros(5) });

        // Cidade
        carregarGraficoPizzaTradicional({ id: 'atendimentoCidade', endpoint: '/Dashboard/ListarCidadesPorStatus', parametros: parametros(1) });
        carregarGraficoPizzaTradicional({ id: 'orcamentoCidade', endpoint: '/Dashboard/ListarCidadesPorStatus', parametros: parametros(2) });
        carregarGraficoPizzaTradicional({ id: 'vendidoCidade', endpoint: '/Dashboard/ListarCidadesPorStatus', parametros: parametros(3) });
        carregarGraficoPizzaTradicional({ id: 'negociadoCidade', endpoint: '/Dashboard/ListarCidadesPorStatus', parametros: parametros(4) });
        carregarGraficoPizzaTradicional({ id: 'naorespondeCidade', endpoint: '/Dashboard/ListarCidadesPorStatus', parametros: parametros(5) });
    });



});

