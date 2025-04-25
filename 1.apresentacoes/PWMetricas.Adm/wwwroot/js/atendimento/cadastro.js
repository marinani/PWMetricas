$(document).ready(function () {
    $('#Whatsapp').mask('(00) 00000-0000');
    $('#ValorPedido').mask('000.000.000.000.000,00', { reverse: true });

    const canalDropdown = document.getElementById("CanalId");
    const origemDropdown = document.getElementById("OrigemId");
    const produtoDropdown = document.getElementById("ProdutoId");
    const tamanhoDropdown = document.getElementById("TamanhoId");
    const statusDropdown = document.getElementById("StatusId");



    if (canalDropdown) { // Verifica se o elemento existe
        canalDropdown.addEventListener("change", function () {
            const selectedOption = canalDropdown.options[canalDropdown.selectedIndex];
            const color = selectedOption.getAttribute("data-color");
            canalDropdown.style.backgroundColor = color || "#ffffff"; // Cor padrão caso não tenha
        });
    }

    if (origemDropdown) { // Verifica se o elemento existe
        origemDropdown.addEventListener("change", function () {
            const selectedOption = origemDropdown.options[origemDropdown.selectedIndex];
            const color = selectedOption.getAttribute("data-color");
            origemDropdown.style.backgroundColor = color || "#ffffff"; // Cor padrão caso não tenha
        });
    }


    if (produtoDropdown) { // Verifica se o elemento existe
        produtoDropdown.addEventListener("change", function () {
            const selectedOption = produtoDropdown.options[produtoDropdown.selectedIndex];
            const color = selectedOption.getAttribute("data-color");
            produtoDropdown.style.backgroundColor = color || "#ffffff"; // Cor padrão caso não tenha
        });
    }


    if (tamanhoDropdown) { // Verifica se o elemento existe
        tamanhoDropdown.addEventListener("change", function () {
            const selectedOption = tamanhoDropdown.options[tamanhoDropdown.selectedIndex];
            const color = selectedOption.getAttribute("data-color");
            tamanhoDropdown.style.backgroundColor = color || "#ffffff"; // Cor padrão caso não tenha
        });
    }

    if (statusDropdown) { // Verifica se o elemento existe
       
        statusDropdown.addEventListener("change", function () {
            const selectedOption = statusDropdown.options[statusDropdown.selectedIndex];
            const color = selectedOption.getAttribute("data-color");
            statusDropdown.style.backgroundColor = color || "#ffffff"; // Cor padrão caso não tenha
        });
    }

    document.addEventListener("DOMContentLoaded", function () {
        debugger
        if ($("#IsVendedor").val()) {
            debugger
            $("#UsuarioId").prop("disabled", true);

        }

    });
    

});

