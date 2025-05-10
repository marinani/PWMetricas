$(document).ready(function () {

    const today = new Date();

    $('#Ano').datepicker({
        format: "yyyy",
        viewMode: "years",
        minViewMode: "years",
        autoclose: true
    });

    $("#Mes").val(today.getMonth() + 1);

    $('#Ano').val(today.getFullYear());


   
    // Carregar Lista de Atendimentos ao carregar a página
    $.get('/Home/ListaTarefas', { pagina: 1 }, function (data) {
        $('#lista-tarefas').html(data);
    });


    // Paginação dinâmica para Atendimentos e Orçamentos
    $(document).on('click', '.pagination a', function (e) {
        e.preventDefault();
        const url = $(this).attr('href'); // URL da página clicada
        const target = $(this).closest('div').attr('id'); // Identifica qual tabela atualizar 

        $.get(url, function (data) {
            $(`#${target}`).html(data); // Atualiza a tabela correspondente
        });
    });

    $(document).on('change', '#LojaId', function () {
        const lojaId = $(this).val(); // Obtém o valor selecionado no campo LojaId
        debugger
        // URLs para atualizar as tabelas com base no filtro LojaId
        const urlTarefas = '/Home/ListaTarefas?LojaId=' + lojaId;

        // Atualiza a tabela de tarefas
        $.get(urlTarefas, function (data) {
            $('#lista-tarefas').html(data);
        });


    });


});