$(document).ready(function () {
    // Carregar Lista de Atendimentos ao carregar a página
    $.get('/Atendimento/ListaAtendimento', { pagina: 1 }, function (data) {
        $('#lista-atendimento').html(data);
    });

    // Carregar Lista de Orçamentos ao carregar a página
    $.get('/Atendimento/ListaOrcamento', { pagina: 1 }, function (data) {
        $('#lista-orcamento').html(data);
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

    // Atualiza a tabela de atendimentos
    $.get(urlAtendimento, function (data) {
        $('#lista-atendimento').html(data);
    });

    // Atualiza a tabela de orçamentos
    $.get(urlOrcamento, function (data) {
        $('#lista-orcamento').html(data);
    });
});