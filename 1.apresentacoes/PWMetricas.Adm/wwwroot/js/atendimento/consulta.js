$(document).ready(function () {
    // Carregar Lista de Atendimentos ao carregar a página
    $.get('/Atendimento/ListaAtendimento', { pagina: 1 }, function (data) {
        $('#lista-atendimento').html(data);
    });

    // Carregar Lista de Orçamentos ao carregar a página
    $.get('/Atendimento/ListaOrcamento', { pagina: 1 }, function (data) {
        $('#lista-orcamento').html(data);
    });

    $.get('/Atendimento/ListaVendido', { pagina: 1 }, function (data) {
        $('#lista-vendido').html(data);
    });

    $.get('/Atendimento/ListaNegociado', { pagina: 1 }, function (data) {
        $('#lista-negociado').html(data);
    });

    $.get('/Atendimento/ListaNaoResponde', { pagina: 1 }, function (data) {
        $('#lista-naoresponde').html(data);
    });
});

// Paginação dinâmica para Atendimentos e Orçamentos
$(document).on('click', '.pagination a', function (e) {
    e.preventDefault();
    const url = $(this).attr('href'); // URL da página clicada
    const target = $(this).closest('div').attr('id'); // Identifica qual tabela atualizar (lista-atendimento ou lista-orcamento)

    $.get(url, function (data) {
        $(`#${target}`).html(data); // Atualiza a tabela correspondente
    });
});

// Submeter o formulário de filtro via AJAX
$('form').on('submit', function (e) {
    e.preventDefault();
    const form = $(this);
    const urlAtendimento = '/Atendimento/ListaAtendimento?' + form.serialize(); // Serializa os dados do formulário
    const urlOrcamento = '/Atendimento/ListaOrcamento?' + form.serialize();
    const urlVendido = '/Atendimento/ListaVendido?' + form.serialize();
    const urlNegociado = '/Atendimento/ListaNegociado?' + form.serialize();
    const urlNaoResponde = '/Atendimento/ListaNaoResponde?' + form.serialize();

    // Atualiza a tabela de atendimentos
    $.get(urlAtendimento, function (data) {
        $('#lista-atendimento').html(data);
    });

    // Atualiza a tabela de orçamentos
    $.get(urlOrcamento, function (data) {
        $('#lista-orcamento').html(data);
    });

    // Atualiza a tabela de vendidos
    $.get(urlVendido, function (data) {
        $('#lista-vendido').html(data);
    });

    // Atualiza a tabela de perdidos negociados
    $.get(urlNegociado, function (data) {
        $('#lista-negociado').html(data);
    });

    // Atualiza a tabela de perdidos não responde
    $.get(urlNaoResponde, function (data) {
        $('#lista-naoresponde').html(data);
    });
});