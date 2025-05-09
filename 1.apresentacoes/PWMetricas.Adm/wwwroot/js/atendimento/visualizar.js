﻿$(document).ready(function () {
    $('#Whatsapp').mask('(00) 00000-0000');
    $('#ValorPedido').mask('000.000.000.000.000,00', { reverse: true });

    const canalDropdown = document.getElementById("CanalId");
    const origemDropdown = document.getElementById("OrigemId");
    const produtoDropdown = document.getElementById("ProdutoId");
    const tamanhoDropdown = document.getElementById("TamanhoId");
    const statusDropdown = document.getElementById("StatusAtendimentoId");
    const cidadeDropdown = $('#Cidade'); // Dropdown de cidades
    const dropdowns = ['#CanalId', '#OrigemId', '#ProdutoId', '#TamanhoId'];

    // Carregar cidades ao carregar a página
    const ufSelecionada = $('#Uf').val(); // Obtém a UF selecionada
    const cidadeSelecionada = $('#Cidade').data('cidade-selecionada'); // Obtém a cidade cadastrada (via atributo data)

    if (ufSelecionada) {
        carregarCidades(ufSelecionada, cidadeSelecionada);
    }

    if ($("#IsVendedor").val() == "True") {
        $("#LojaId").prop("disabled", true);
        $("#UsuarioId").prop("disabled", true);
    }
    else {
        $("#LojaId").prop("disabled", false);
        $("#UsuarioId").prop("disabled", false);
    }


    // Função para aplicar a cor de fundo ao carregar a página
    function aplicarCorDeFundo(dropdownId) {
        const dropdown = document.querySelector(dropdownId);
        if (dropdown) {
            const selectedOption = dropdown.options[dropdown.selectedIndex];
            const color = selectedOption.getAttribute("data-color");
            dropdown.style.backgroundColor = color || "transparent"; // Cor padrão caso não tenha
        }
    }

    // Aplica a cor de fundo para cada dropdown
    dropdowns.forEach(function (dropdownId) {
        aplicarCorDeFundo(dropdownId);
    });

    // Adiciona evento para atualizar a cor ao alterar o valor
    dropdowns.forEach(function (dropdownId) {
        const dropdown = document.querySelector(dropdownId);
        if (dropdown) {
            dropdown.addEventListener("change", function () {
                const selectedOption = dropdown.options[dropdown.selectedIndex];
                const color = selectedOption.getAttribute("data-color");
                dropdown.style.backgroundColor = color || "transparent"; // Cor padrão caso não tenha
            });
        }
    });

    if (canalDropdown) { // Verifica se o elemento existe
        canalDropdown.addEventListener("change", function () {
            const selectedOption = canalDropdown.options[canalDropdown.selectedIndex];
            const color = selectedOption.getAttribute("data-color");
            canalDropdown.style.backgroundColor = color || "transparent"; // Cor padrão caso não tenha
        });
    }

    if (origemDropdown) { // Verifica se o elemento existe
        origemDropdown.addEventListener("change", function () {
            const selectedOption = origemDropdown.options[origemDropdown.selectedIndex];
            const color = selectedOption.getAttribute("data-color");
            origemDropdown.style.backgroundColor = color || "transparent"; // Cor padrão caso não tenha
        });
    }


    if (produtoDropdown) { // Verifica se o elemento existe
        produtoDropdown.addEventListener("change", function () {
            const selectedOption = produtoDropdown.options[produtoDropdown.selectedIndex];
            const color = selectedOption.getAttribute("data-color");
            produtoDropdown.style.backgroundColor = color || "transparent"; // Cor padrão caso não tenha
        });
    }


    if (tamanhoDropdown) { // Verifica se o elemento existe
        tamanhoDropdown.addEventListener("change", function () {
            const selectedOption = tamanhoDropdown.options[tamanhoDropdown.selectedIndex];
            const color = selectedOption.getAttribute("data-color");
            tamanhoDropdown.style.backgroundColor = color || "transparent"; // Cor padrão caso não tenha
        });
    }

    if (statusDropdown) { // Verifica se o elemento existe
        statusDropdown.addEventListener("change", function () {
            const selectedOption = statusDropdown.options[statusDropdown.selectedIndex];
            const color = selectedOption.getAttribute("data-color");
            statusDropdown.style.backgroundColor = color || "transparent"; // Cor padrão caso não tenha
        });
    }

    // Evento para carregar cidades ao selecionar UF
    $('#Uf').on('change', function () {
        const uf = $(this).val(); // Obtém o valor selecionado no campo UF
        if (uf) {
            carregarCidades(uf, null); // Carrega as cidades sem cidade selecionada
        } else {
            cidadeDropdown.empty().append('<option value="">Selecione uma cidade</option>'); // Limpa o dropdown
        }
    });

    function carregarCidades(uf, cidadeSelecionada) {
        $.get('/Cep/ConsultarCidadePorEstado', { siglaUF: uf }, function (data) {
            cidadeDropdown.empty(); // Limpa as opções existentes
            cidadeDropdown.append('<option value="">Selecione uma cidade</option>'); // Adiciona a opção padrão

            // Adiciona as cidades retornadas ao dropdown
            data.forEach(function (cidade) {
                const isSelected = cidade.nome === cidadeSelecionada ? 'selected' : '';
                cidadeDropdown.append(`<option value="${cidade.nome}" ${isSelected}>${cidade.nome}</option>`);
            });
        }).fail(function () {
            alert('Erro ao carregar as cidades. Tente novamente.');
        });
    }

});

