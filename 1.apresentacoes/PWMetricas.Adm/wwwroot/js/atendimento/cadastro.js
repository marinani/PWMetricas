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

    function validarModelo(atendimentoModel) {
        const errors = [];

        if (!atendimentoModel.LojaId) errors.push("O campo LojaId é obrigatório.");
        if (!atendimentoModel.UsuarioId) errors.push("O campo UsuarioId é obrigatório.");
        if (!atendimentoModel.CanalId) errors.push("O campo CanalId é obrigatório.");
        if (!atendimentoModel.Data) errors.push("O campo Data é obrigatório.");
        if (!atendimentoModel.OrigemId) errors.push("O campo OrigemId é obrigatório.");
        if (!atendimentoModel.ClienteId) errors.push("O campo ClienteId é obrigatório.");
        if (!atendimentoModel.ProdutoId) errors.push("O campo ProdutoId é obrigatório.");
        if (!atendimentoModel.Whatsapp) errors.push("O campo Whatsapp é obrigatório.");
        if (!atendimentoModel.TamanhoId) errors.push("O campo TamanhoId é obrigatório.");
        if (!atendimentoModel.Uf) errors.push("O campo Uf é obrigatório.");
        if (!atendimentoModel.ValorPedido) errors.push("O campo ValorPedido é obrigatório.");
        if (!atendimentoModel.Cidade) errors.push("O campo Cidade é obrigatório.");
        if (!atendimentoModel.DataRetorno) errors.push("O campo DataRetorno é obrigatório.");
        if (!atendimentoModel.StatusId) errors.push("O campo StatusId é obrigatório.");
        if (!atendimentoModel.Observacao) errors.push("O campo Observacao é obrigatório.");

        return errors;
    }

    document.addEventListener("DOMContentLoaded", function () {
        const form = document.querySelector("form[asp-action='NovoAtendimento']");
        const lojaIdSelect = document.getElementById("LojaId");

        document.querySelector("button[type='submit']").addEventListener("click", function (event) {
            event.preventDefault(); // Impede o envio padrão do formulário

            // Monta o modelo
            const atendimentoModel = {
                LojaId: lojaIdSelect.value, // Captura o valor do campo LojaId
                UsuarioId: document.getElementById("UsuarioId").value,
                CanalId: document.getElementById("CanalId").value,
                Data: document.getElementById("Data").value,
                OrigemId: document.getElementById("OrigemId").value,
                ClienteId: document.querySelector("[asp-for='ClienteId']").value,
                ProdutoId: document.getElementById("ProdutoId").value,
                Whatsapp: document.getElementById("Whatsapp").value,
                TamanhoId: document.getElementById("TamanhoId").value,
                Uf: document.getElementById("Uf").value,
                ValorPedido: document.getElementById("ValorPedido").value,
                Cidade: document.querySelector("[asp-for='Cidade']").value,
                DataRetorno: document.getElementById("DataRetorno").value,
                StatusId: document.getElementById("StatusId").value,
                Observacao: document.getElementById("Observacao").value,
                IsVendedor: document.querySelector("input[name='IsVendedor']").value // Campo hidden
            };

            const errors = validarModelo(atendimentoModel);

            if (errors.length > 0) {
                alert("Erros encontrados:\n" + errors.join("\n"));
                return;
            }

            // Envia o modelo via POST
            fetch(form.action, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(atendimentoModel)
            })
                .then(response => {
                    if (response.ok) {
                        showAlert("Atendimento gravado com sucesso!", "success");
                        //alert("Atendimento gravado com sucesso!");
                        // Redirecionar ou limpar o formulário, se necessário
                    } else {
                        showAlert("Erro ao gravar atendimento.", "error");
                        
                    }
                })
                .catch(error => {
                    console.error("Erro:", error);
                    //alert("Erro ao enviar os dados.");
                    showAlert("Erro ao enviar os dados.", "error");
                });
        });
    });
    

});

